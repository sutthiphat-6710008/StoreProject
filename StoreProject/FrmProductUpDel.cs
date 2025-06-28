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

    public partial class FrmProductUpDel : Form
    {
        byte[] proImage;
        private void showWarningMSG(string msg)
        {
            MessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private byte[] convertImageToByteArray(Image image, ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }
        private Image convertByteArrayToImage(byte[] byteArrayIn)
        {
            byte[] proImage;
            if (byteArrayIn == null || byteArrayIn.Length == 0)
            {
                return null;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (ArgumentException ex)
            {
                // อาจเกิดขึ้นถ้า byte array ไม่ใช่ข้อมูลรูปภาพที่ถูกต้อง
                Console.WriteLine("Error converting byte array to image: " + ex.Message);
                return null;
            }
        }


        int proId;
        public FrmProductUpDel(int proID)
        {
            InitializeComponent();
            this.proId = proID;
        }

        private void FrmProductUpDel_Load(object sender, EventArgs e)
        {
            string connectionString = @"Server=MSI\SQLEXPRESS;Database=store_db;Trusted_Connection=True;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string strSQL = "SELECT proId, proName, proPrice, proQuan, proUnit, proStatus, proImage FROM product_tb " +
                 "WHERE proId = @proId";
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, sqlConnection))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@proId", proId);

                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        DataRow row = dataTable.Rows[0];
                        tbProId.Text = row["proId"].ToString();
                        tbProName.Text = row["proName"].ToString();
                        tbProPrice.Text = row["proPrice"].ToString();
                        tbProUnit.Text = row["proUnit"].ToString();
                        nudProQuan.Value = int.Parse(row["proQuan"].ToString());
                        if (row["proStatus"].ToString() == "พร้อมขาย")
                        {
                            rdoProStatusOn.Checked = true;
                        }
                        else
                        {
                            rdoProStatusOff.Checked = true;
                        }
                        if (row["proImage"] == DBNull.Value)
                        {
                            pcbProImage.Image = null;
                        }
                        else
                        { 
                            pcbProImage.Image = convertByteArrayToImage((byte[])row["proImage"]);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btProDelete_Click(object sender, EventArgs e)
        {
            string connectionString = @"Server=MSI\SQLEXPRESS;Database=store_db;Trusted_Connection=True;";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                    string strSQL = "DELETE FROM product_tb WHERE proId=@proId";

                    using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                    {
                        sqlCommand.Parameters.Add("@proId", SqlDbType.Int).Value = tbProId.Text;
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        MessageBox.Show("ลบเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void btProUpdate_Click(object sender, EventArgs e)
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
                       string strSQL = "UPDATE product_tb SET proName=@proName, proPrice=@proPrice, proQuan=@proQuan, proUnit=@proUnit, " +
                            "proStatus=@proStatus, proImage=@proImage, updateAt=@updateAt WHERE proId=@proId";


                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@proId", SqlDbType.Int).Value = int.Parse(tbProId.Text);
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
                            sqlCommand.Parameters.Add("@updateAt", SqlDbType.Date).Value = DateTime.Now.Date;

                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();
                            MessageBox.Show("บันทึกแก้ไขสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}
