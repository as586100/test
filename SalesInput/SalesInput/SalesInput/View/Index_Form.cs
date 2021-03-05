using SalesInput.View.Pickup_Form;
using SalesInput.View.Location_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesInput.View;
using System.Net;
using System.Net.NetworkInformation;
using SalesInput.Librarys;
using SalesInput.SqlString;

namespace SalesInput
{
    public partial class Index_Form : Form
    {
        public Index_Form()
        {
            InitializeComponent();      
            this.Text += labelVersion.Text;
            try
            {
              
                IPAddress IP = IPAddress.Parse("192.168.0.219");
                if (ByPing(IP))
                {
                SalesInputSQLstring SQL = new SalesInputSQLstring();
                TomLibrary Tom = new TomLibrary();
                Tom.SQLConnectionString = SQL.FILASQLConnectionString;
                DataTable Versions = Tom.SQLGetData(SQL.CheckVersions);
                if(labelVersion.Text != Versions.Rows[0][0].ToString())
                {
                    MessageBox.Show("目前:"+ labelVersion.Text + " 非最新版本。","提示");
                    labelUpdateMsg.Visible = true;
                }
            }

            }
            catch
            {
               
            }
        }

        /// <path>
        /// SalesInput/Index_Form/button1_Click
        /// </path>
        /// <summary>
        /// 開啟連續套表Form
        /// </summary>
        /// <token>
        /// button1_Click
        /// </token>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 開啟 A1_Print_Form 視窗 
        /// </remarks>
        /// <return>
        /// 開啟 A1_Print_Form 視窗 
        /// </return>
        private void button1_Click(object sender, EventArgs e)
        {
            A1_Print_Form PF = new A1_Print_Form();

            PF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            A1_Barcode_Form BF = new A1_Barcode_Form();
            BF.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            A0_SettingLogin SL = new A0_SettingLogin();


            string Path= System.Windows.Forms.Application.StartupPath + @"\update.exe";
            Process.Start(Path);
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //A1_Pickup_Form PF = new A1_Pickup_Form();
            //PF.Show();
            A0_LoginForm LF = new A0_LoginForm();
            LF.Show();

        }

        private void buttonLocation_Click(object sender, EventArgs e)
        {
            A1_Location_Form LF = new A1_Location_Form();
            LF.Show();
        }

        private void buttonLocationSearch_Click(object sender, EventArgs e)
        {
            A1_Location_Search LS = new A1_Location_Search();

            LS.Show();
        }

        private void buttonErp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://211.22.134.8/mis/Main/Main.aspx");
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.0.38/fila/");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            UpdateMessage UM = new UpdateMessage();
            UM.Show();
        }
        public bool ByPing(IPAddress IP)
        {
            Ping tPingControl = new Ping();
            PingReply tReply = tPingControl.Send(IP);
            tPingControl.Dispose();
            if (tReply.Status != IPStatus.Success)
                return false;
            else
                return true;
        }

        private void labelUpdateMsg_Click(object sender, EventArgs e)
        {
            try
            {
                SalesInputSQLstring SQL = new SalesInputSQLstring();
                TomLibrary Tom = new TomLibrary();
                Tom.SQLConnectionString = SQL.FILASQLConnectionString;
                DataTable Versions = Tom.SQLGetData(SQL.CheckVersions);
                DialogResult dialogResult = MessageBox.Show("目前最新版本為 : " + Versions.Rows[0][0] + "\n發行日為 : " +DateTime.Parse(Versions.Rows[0][1].ToString()).ToString("yyyy/MM/dd") + "\n請問是否要執行版本更新呢?", "版本資訊", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    A0_SettingLogin SL = new A0_SettingLogin();
                    string Path = System.Windows.Forms.Application.StartupPath + @"\update.exe";
                    Process.Start(Path);
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            catch
            {
                MessageBox.Show("網路環境不穩定，請確認網路環境。");
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            View.DataConvert_Form.A1_DataConvert_Form form = new View.DataConvert_Form.A1_DataConvert_Form();
            form.Show();
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            View.Customer_Form.A0_noticeMenu menu = new View.Customer_Form.A0_noticeMenu();
            menu.Show();
        }
    }
}
