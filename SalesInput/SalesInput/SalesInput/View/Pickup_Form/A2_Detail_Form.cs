using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesInput.Modle;

namespace SalesInput.View.Pickup_Form
{
    public partial class A2_Detail_Form : Form
    {
        public A2_Detail_Form( string orderID)
        {
            InitializeComponent();
           PickupDBEntities1 PickDB = new PickupDBEntities1();
            int OrderID = int.Parse(orderID);

           dataGridView1.DataSource= PickDB.ReadyOrderBs.Where(s => s.RB_Maping_RA_ID == OrderID).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int sum = 0;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {


                sum += int.Parse(Item.Cells["RB_Amount"].Value.ToString());

            }
            label1.Text = "合計:" + sum.ToString();
        }
    }
}
