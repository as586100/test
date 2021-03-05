using SalesInput.Librarys;
using SalesInput.Modle;
using SalesInput.SqlString;
using SalesInput.View.Transform_Form;
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
    public partial class A1_Transform : Form
    {
        PickupDBEntities1 DB = new PickupDBEntities1();
        public TextBox TB = new TextBox();
        public A1_Transform()
        {
            InitializeComponent();
            System.Windows.Forms.Application.DoEvents();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool SelectCheck = (null != dataGridView1.Rows[e.RowIndex].Cells[0] && null != dataGridView1.Rows[e.RowIndex].Cells[0].Value && true == (bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                if (SelectCheck)
                {
                    SelectCheck = false;
                }
                else { SelectCheck = true; }

                dataGridView1.Rows[e.RowIndex].Cells[0].Value = SelectCheck;
            }
        }

        private void button_Unlock_Click(object sender, EventArgs e)
        {
            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            TOM.SQLConnectionString = SQL.FILASQLConnectionString;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    string Order = Item.Cells[1].Value.ToString();
                    List<PickupB> SumList = DB.PickupBs.Where(S => S.OrderID == Order).ToList();
                    PickupA A = DB.PickupAs.Find(Order);
                    A.OrderDifference = null;
                    A.OrderSate = "N";
                    A.OrderTrunState = "N";
                    DB.SaveChanges();
                    TOM.SQLUpdateData(SQL.PickupFinish, Order, "N", "0",""); //更新單據狀態及差異量
                    MessageBox.Show("此張單據已解除。");
                }
            }
            dataGridView1.DataSource = DB.PickupAs.Where(s => s.OrderSate == "Y").ToList();
        }

        private void button_Turn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    A1_Transform_Menu TM = new A1_Transform_Menu(Item.Cells[1].Value.ToString());
                    TM.Show();

                }
            }
        }

        private void button_difference_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    A2_DifferenceForm DF = new A2_DifferenceForm(Item.Cells[1].Value.ToString());
                    DF.Show();

                }
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
           List<PickupA> A = DB.PickupAs.Where(s => s.OrderSate == "Y").ToList();
            if (textBoxOrder.Text != "")
            {
                A = A.Where(s => s.OrderID == textBoxOrder.Text).ToList();
            }
            if (textBoxStore.Text != "")
            {
                A = A.Where(s => s.OrderStore == textBoxStore.Text).ToList();
            }
            if(dateTimePicker1.Text != " ")
            {
                
                DateTime Date = dateTimePicker1.Value.Date;//日期
                A = A.Where(s => s.OrderDate == Date).ToList();             
            }
            if (checkBox1.Checked)
            {
                A = A.Where(s => s.OrderTrunState != "Y").ToList();
            }

           
            dataGridView1.DataSource = A;
        }

        private void textBoxNum_MouseDown(object sender, MouseEventArgs e)
        {
            TB = (TextBox)sender;
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {

            TB.Focus();
            Button btn = (Button)sender;
            SendKeys.Send(btn.Text);
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            TB.Focus();

            SendKeys.Send("{BACKSPACE}");
        }
    }
}
