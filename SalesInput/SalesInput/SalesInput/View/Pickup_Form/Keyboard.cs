using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.Pickup_Form
{
    public partial class Keyboard : Form
    {
        public TextBox TB = new TextBox();
        public Keyboard()
        {
            InitializeComponent();
            textBox1.Focus();
            TB = textBox1;
        }

        private void textBoxNum_MouseDown(object sender, MouseEventArgs e)
        {
            TB = (TextBox)sender;
        }

        private void button1_KeyDown(object sender, EventArgs e)
        {
            TB.Focus();
            Button btn = (Button)sender;
            SendKeys.Send(btn.Text);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            TB = (TextBox)sender;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; //注意這一行

        }

        public Keyboard(string strTextMsg)
        {
            InitializeComponent();
            textBox1.Text = strTextMsg;
        }

        public string TextBoxMsg
        {
            set
            {
                TB.Text = value;
            }
            get
            {
                return TB.Text;
            }
        }

        private void button_Del_KeyDown(object sender, EventArgs e)
        {
            TB.Focus();

            SendKeys.Send("{BACKSPACE}");
        }
    }
}
