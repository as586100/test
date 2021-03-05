using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesInput.View.Pickup_Form;
using SalesInput.Librarys;
using SalesInput.SqlString;
using System.Net;
using System.Net.NetworkInformation;

namespace SalesInput.View.Pickup_Form
{
    public partial class A0_LoginForm : Form
    {
        public A0_LoginForm()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
         
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard k = new Keyboard();

            k.TB.Text = textBox1.Text;

            if (k.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = k.TextBoxMsg;//從form2取值設定到form1
            }
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard k = new Keyboard();

            k.TB.Text = textBox2.Text;

            if (k.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = k.TextBoxMsg;//從form2取值設定到form1
            }
        }

        private void A0_LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_button_Click(object sender, EventArgs e)
        {

            IPAddress IP = IPAddress.Parse("192.168.0.219");
            if (ByPing(IP))
            {
                try
                {
                    string account = textBox1.Text;
                    string password = textBox2.Text;
                    TomLibrary TOM = new TomLibrary();
                    SalesInputSQLstring SQL = new SalesInputSQLstring();
                    TOM.SQLConnectionString = SQL.FILASQLConnectionString;
                    DataTable data = TOM.SQLGetData(SQL.UserCheck, account, password);

                    if (data.Rows.Count == 0)
                    {
                        MessageBox.Show("帳號密碼有誤。");
                    }
                    else
                    {
                        A1_Pickup_Form PF = new A1_Pickup_Form(account);
                        PF.Show();
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。");
                }
            }
            else
            {
                MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。");
            }
        }

        private void A0_LoginForm_KeyDown(object sender, KeyEventArgs e)
        {

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
    }
}
