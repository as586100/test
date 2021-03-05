namespace SalesInput.View.DataConvert_Form
{
    partial class A1_DataConvert_Form
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_store = new System.Windows.Forms.TextBox();
            this.textBox_SheetName1 = new System.Windows.Forms.TextBox();
            this.buttonConverBarcode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox_SheetName2 = new System.Windows.Forms.TextBox();
            this.buttonConver1D = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(282, 175);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBox_store);
            this.tabPage1.Controls.Add(this.textBox_SheetName1);
            this.tabPage1.Controls.Add(this.buttonConverBarcode);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(274, 149);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "盤點機格式";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "手開單格式 ➤ 盤點機格式";
            // 
            // textBox_store
            // 
            this.textBox_store.Location = new System.Drawing.Point(82, 68);
            this.textBox_store.Name = "textBox_store";
            this.textBox_store.Size = new System.Drawing.Size(175, 22);
            this.textBox_store.TabIndex = 9;
            // 
            // textBox_SheetName1
            // 
            this.textBox_SheetName1.Location = new System.Drawing.Point(82, 40);
            this.textBox_SheetName1.Name = "textBox_SheetName1";
            this.textBox_SheetName1.Size = new System.Drawing.Size(175, 22);
            this.textBox_SheetName1.TabIndex = 8;
            // 
            // buttonConverBarcode
            // 
            this.buttonConverBarcode.Location = new System.Drawing.Point(13, 105);
            this.buttonConverBarcode.Name = "buttonConverBarcode";
            this.buttonConverBarcode.Size = new System.Drawing.Size(244, 23);
            this.buttonConverBarcode.TabIndex = 7;
            this.buttonConverBarcode.Text = "轉換盤點機格式";
            this.buttonConverBarcode.UseVisualStyleBackColor = true;
            this.buttonConverBarcode.Click += new System.EventHandler(this.buttonConverBarcode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "帶入分倉";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "工作表名稱";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.textBox_SheetName2);
            this.tabPage2.Controls.Add(this.buttonConver1D);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(274, 149);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "一維單格式";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "手開單格式 ➤ 一維單格式";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(82, 68);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(175, 22);
            this.textBox3.TabIndex = 15;
            // 
            // textBox_SheetName2
            // 
            this.textBox_SheetName2.Location = new System.Drawing.Point(82, 40);
            this.textBox_SheetName2.Name = "textBox_SheetName2";
            this.textBox_SheetName2.Size = new System.Drawing.Size(175, 22);
            this.textBox_SheetName2.TabIndex = 14;
            // 
            // buttonConver1D
            // 
            this.buttonConver1D.Location = new System.Drawing.Point(13, 105);
            this.buttonConver1D.Name = "buttonConver1D";
            this.buttonConver1D.Size = new System.Drawing.Size(244, 23);
            this.buttonConver1D.TabIndex = 13;
            this.buttonConver1D.Text = "轉換一維單格式";
            this.buttonConver1D.UseVisualStyleBackColor = true;
            this.buttonConver1D.Click += new System.EventHandler(this.buttonConver1D_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "帶入分倉";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "工作表名稱";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // A1_DataConvert_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 194);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "A1_DataConvert_Form";
            this.Text = "資料轉換";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_store;
        private System.Windows.Forms.TextBox textBox_SheetName1;
        private System.Windows.Forms.Button buttonConverBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox_SheetName2;
        private System.Windows.Forms.Button buttonConver1D;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}