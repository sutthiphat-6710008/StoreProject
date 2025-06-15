namespace StoreProject
{
    partial class FrmProductShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductShow));
            this.label1 = new System.Windows.Forms.Label();
            this.lvAllProduct = new System.Windows.Forms.ListView();
            this.btnProductCreaate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(55, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1115, 92);
            this.label1.TabIndex = 0;
            this.label1.Text = "บริหารจัดการข้อมูลสินค้า";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvAllProduct
            // 
            this.lvAllProduct.HideSelection = false;
            this.lvAllProduct.Location = new System.Drawing.Point(61, 155);
            this.lvAllProduct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvAllProduct.Name = "lvAllProduct";
            this.lvAllProduct.Size = new System.Drawing.Size(1107, 278);
            this.lvAllProduct.TabIndex = 1;
            this.lvAllProduct.UseCompatibleStateImageBehavior = false;
            // 
            // btnProductCreaate
            // 
            this.btnProductCreaate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductCreaate.Image = global::StoreProject.Properties.Resources.basket;
            this.btnProductCreaate.Location = new System.Drawing.Point(952, 464);
            this.btnProductCreaate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProductCreaate.Name = "btnProductCreaate";
            this.btnProductCreaate.Size = new System.Drawing.Size(217, 63);
            this.btnProductCreaate.TabIndex = 2;
            this.btnProductCreaate.Text = "เพิ่มข้อมูลสินค้า";
            this.btnProductCreaate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProductCreaate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProductCreaate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(669, 548);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(500, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "* เมื่อต้องการแก้ไขหรือลบข้อมูลสินค้า ให้ดังเบิ้ลคลิกที่รายการสินค้า";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProductShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 604);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnProductCreaate);
            this.Controls.Add(this.lvAllProduct);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimizeBox = false;
            this.Name = "FrmProductShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "บริการจัดการข้อมูลสินค้า";
            this.Load += new System.EventHandler(this.FrmProductShow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvAllProduct;
        private System.Windows.Forms.Button btnProductCreaate;
        private System.Windows.Forms.Label label2;
    }
}