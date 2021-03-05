using SalesInput.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesInput.Librarys;
using System.Diagnostics;

namespace SalesInput.View.Pickup_Form
{
    public partial class A2_DifferenceForm : Form
    {
        string Order = "";
        PickupDBEntities1 DB = new PickupDBEntities1();
        public A2_DifferenceForm(string order)
        {
            InitializeComponent();
            Order = order;
            MessageBox.Show(Order);
            dataGridView1.DataSource = DB.PickupBs.Where(s => s.OrderID == Order && s.Order_Amount != s.Order_PickAmount).ToList();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int SUM = 0;  
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                if ( Item.Cells["應揀"].Value != Item.Cells["實揀"].Value)
                {
                    int Must = int.Parse(Item.Cells["應揀"].Value.ToString());
                    int Real= int.Parse(Item.Cells["實揀"].Value.ToString());
                    int sum = Must - Real;
                    SUM += sum;
                    Item.Cells["差異"].Value = sum.ToString();
                }

            }
            label1.Text = "差異量:" + SUM.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ServerUrl = Environment.CurrentDirectory + "\\tmp\\";
            TomLibrary TOM = new TomLibrary();
            TOM.ListToExcelFile(DB.PickupBs.Where(s => s.OrderID == Order && s.Order_Amount != s.Order_PickAmount).ToList(), ServerUrl, "差異表");
            Process.Start(ServerUrl + "差異表.xls");
        }
    }
}
