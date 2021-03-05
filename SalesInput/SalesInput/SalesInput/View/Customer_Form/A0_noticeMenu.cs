using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.Customer_Form
{
    public partial class A0_noticeMenu : Form
    {
        public A0_noticeMenu()
        {
            InitializeComponent();
        }

        private void button_NoticeDownload_Click(object sender, EventArgs e)
        {
            A1_CustomerShips_Form ships_Form = new A1_CustomerShips_Form();
            ships_Form.Show();
        }

        private void button_Pickup_Click(object sender, EventArgs e)
        {
            A1_ShipsMenu shipsMenu_Form = new A1_ShipsMenu();
            shipsMenu_Form.Show();
        }

        private void buttonTrun_Click(object sender, EventArgs e)
        {
            A1_NoticeTrun noticeTrun_Form = new A1_NoticeTrun();
            noticeTrun_Form.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
