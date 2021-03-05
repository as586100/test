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
using SalesInput.View.Transform_Form;

namespace SalesInput
{
    public partial class A1_Pickup_Form : Form
    {
        string Staff = "";//員工編號
        public A1_Pickup_Form(string staff)
        {
            InitializeComponent();
            Staff = staff;
        }

        private void button__Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_DownLoad_Click(object sender, EventArgs e)
        {
            A2_Download_Form DF = new A2_Download_Form(Staff);
            DF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            A2_Pickup_Download_Form PD = new A2_Pickup_Download_Form(Staff);
            PD.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            A1_Transform T = new A1_Transform();
            System.Windows.Forms.Application.DoEvents();
            T.Show();

           

        }
    }
}
