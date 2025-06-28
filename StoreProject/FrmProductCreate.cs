using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreProject
{
    public partial class btProDelete : Form
    {
        byte[] proImage;

        public btProDelete()
        {
            InitializeComponent();
        }

        private byte[] convertImageToByteArray(Image image, ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }
        private void btProImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pcbProImage.Image = Image.FromFile(openFileDialog.FileName);
                if (pcbProImage.Image.RawFormat == ImageFormat.Jpeg)
                {
                    proImage = convertImageToByteArray(pcbProImage.Image, ImageFormat.Jpeg);
                }
                else
                {
                    proImage = convertImageToByteArray(pcbProImage.Image, ImageFormat.Png);
                }

            }
        }

        private void tbProPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox tb = sender as TextBox;

            // อนุญาตให้กด Backspace ได้
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // อนุญาตให้พิมพ์ตัวเลข
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }

            // อนุญาตให้พิมพ์จุดทศนิยมได้แค่ 1 จุด
            if (e.KeyChar == '.')
            {
                if (!tb.Text.Contains("."))
                {
                    return;
                }
            }

            // ถ้าไม่เข้าเงื่อนไขใด ๆ ให้ยกเลิกการพิมพ์
            e.Handled = true;
        }

        private void showWarningMSG(string msg)
        {
            MessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            if (proImage == null)
            {
                showWarningMSG("กรุณาเลือกรูปด้วย");
            }
            else if (tbProName.Text.Length == 0)
            {
                showWarningMSG("กรุณากรอกชื่อสินค้า");
            }
            else if (tbProPrice.Text.Length == 0)
            {
                showWarningMSG("กรุณากรอกราคาสินค้า");
            }
            else if (tbProUnit.Text.Length == 0)
            {
                showWarningMSG("กรุณากรอกรายละเอียดสินค้า");
            }
            else if (int.Parse(nudProQuan.Value.ToString()) <= 0)
            {
                showWarningMSG("กรุณากรอกรายละเอียดสินค้า");
            }
            else
            {
                string connectionString = @"Server=MSI\SQLEXPRESS;Database=store_db;Trusted_Connection=True;";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                        string strSQL = "INSERT INTO product_tb (proName, proPrice, proQuan, proUnit, proStatus, proImage,createAt,updateAt ) " +
                                       "VALUES (@proName, @proPrice, @proQuan, @proUnit, @proStatus, @proImage,@createAt,@updateAt)";

                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@proName", SqlDbType.NVarChar, 300).Value = tbProName.Text;
                            sqlCommand.Parameters.Add("@proPrice", SqlDbType.Float).Value = float.Parse(tbProPrice.Text);
                            sqlCommand.Parameters.Add("@proQuan", SqlDbType.Int).Value = int.Parse(nudProQuan.Value.ToString());
                            sqlCommand.Parameters.Add("@proUnit", SqlDbType.NVarChar, 50).Value = tbProUnit.Text;
                            if (rdoProStatusOn.Checked == true)
                            {
                                sqlCommand.Parameters.Add("@proStatus", SqlDbType.NVarChar, 50).Value = "พร้อมขาย";
                            }
                            else
                            {
                                sqlCommand.Parameters.Add("@proStatus", SqlDbType.NVarChar, 50).Value = "ไม่พร้อมขาย";
                            }
                            sqlCommand.Parameters.Add("@proImage", SqlDbType.Image).Value = proImage;
                            sqlCommand.Parameters.Add("@createAt", SqlDbType.Date).Value = DateTime.Now.Date;
                            sqlCommand.Parameters.Add("@updateAt", SqlDbType.Date).Value = DateTime.Now.Date;

                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();
                            MessageBox.Show("บันทึกข้อมูลสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            proImage = null;
            tbProName.Clear();
            tbProPrice.Clear();
            tbProUnit.Clear();
            nudProQuan.Value = 0;
            pcbProImage.Image = null;
        }
    }
}
