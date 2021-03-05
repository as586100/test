namespace SalesInput.View.Customer_Form
{
    partial class A0_noticeMenu
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
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonTrun = new System.Windows.Forms.Button();
            this.button_Pickup = new System.Windows.Forms.Button();
            this.button_NoticeDownload = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonExit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonTrun, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_Pickup, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NoticeDownload, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 425);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExit.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonExit.Location = new System.Drawing.Point(295, 215);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(286, 207);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "離開";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonTrun
            // 
            this.buttonTrun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTrun.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonTrun.Location = new System.Drawing.Point(3, 215);
            this.buttonTrun.Name = "buttonTrun";
            this.buttonTrun.Size = new System.Drawing.Size(286, 207);
            this.buttonTrun.TabIndex = 2;
            this.buttonTrun.Text = "寄客轉單";
            this.buttonTrun.UseVisualStyleBackColor = true;
            this.buttonTrun.Click += new System.EventHandler(this.buttonTrun_Click);
            // 
            // button_Pickup
            // 
            this.button_Pickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Pickup.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Pickup.Location = new System.Drawing.Point(295, 3);
            this.button_Pickup.Name = "button_Pickup";
            this.button_Pickup.Size = new System.Drawing.Size(286, 206);
            this.button_Pickup.TabIndex = 1;
            this.button_Pickup.Text = "寄客揀貨";
            this.button_Pickup.UseVisualStyleBackColor = true;
            this.button_Pickup.Click += new System.EventHandler(this.button_Pickup_Click);
            // 
            // button_NoticeDownload
            // 
            this.button_NoticeDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NoticeDownload.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_NoticeDownload.Location = new System.Drawing.Point(3, 3);
            this.button_NoticeDownload.Name = "button_NoticeDownload";
            this.button_NoticeDownload.Size = new System.Drawing.Size(286, 206);
            this.button_NoticeDownload.TabIndex = 0;
            this.button_NoticeDownload.Text = "寄客通知單下載";
            this.button_NoticeDownload.UseVisualStyleBackColor = true;
            this.button_NoticeDownload.Click += new System.EventHandler(this.button_NoticeDownload_Click);
            // 
            // A0_noticeMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 425);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "A0_noticeMenu";
            this.Text = "A0_noticeMenu";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonTrun;
        private System.Windows.Forms.Button button_Pickup;
        private System.Windows.Forms.Button button_NoticeDownload;
    }
}