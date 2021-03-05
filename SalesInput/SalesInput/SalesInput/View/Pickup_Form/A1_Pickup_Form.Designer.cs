namespace SalesInput
{
    partial class A1_Pickup_Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button__Exit = new System.Windows.Forms.Button();
            this.button_DownLoad = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button__Exit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_DownLoad, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(50);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 426);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // button__Exit
            // 
            this.button__Exit.BackgroundImage = global::SalesInput.Properties.Resources.pictogram_din_e010_exit;
            this.button__Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button__Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button__Exit.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button__Exit.Location = new System.Drawing.Point(469, 216);
            this.button__Exit.Name = "button__Exit";
            this.button__Exit.Size = new System.Drawing.Size(411, 157);
            this.button__Exit.TabIndex = 2;
            this.button__Exit.Text = "離開";
            this.button__Exit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button__Exit.UseVisualStyleBackColor = true;
            this.button__Exit.Click += new System.EventHandler(this.button__Exit_Click_1);
            // 
            // button_DownLoad
            // 
            this.button_DownLoad.BackgroundImage = global::SalesInput.Properties.Resources.File_Download_512;
            this.button_DownLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_DownLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DownLoad.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_DownLoad.ForeColor = System.Drawing.Color.Black;
            this.button_DownLoad.Location = new System.Drawing.Point(53, 53);
            this.button_DownLoad.Name = "button_DownLoad";
            this.button_DownLoad.Size = new System.Drawing.Size(410, 157);
            this.button_DownLoad.TabIndex = 3;
            this.button_DownLoad.Text = "單據下載";
            this.button_DownLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_DownLoad.UseVisualStyleBackColor = true;
            this.button_DownLoad.Click += new System.EventHandler(this.button_DownLoad_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::SalesInput.Properties.Resources.icon31;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(469, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(411, 157);
            this.button2.TabIndex = 4;
            this.button2.Text = "揀貨";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::SalesInput.Properties.Resources._1456991079743;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(53, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(410, 157);
            this.button3.TabIndex = 5;
            this.button3.Text = "揀貨轉調撥";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // A1_Pickup_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "A1_Pickup_Form";
            this.Text = "A1_Pickup_Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button__Exit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_DownLoad;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}