using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.Location_Form
{
    public partial class A1_Location_Form : Form
    {

        public A1_Location_Form()
        {
            InitializeComponent();
            Upload();
        

        }
        BindingList<LocationItem> Location_List = new BindingList<LocationItem>();
        int _SN = 0;
        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_Barcode_KeyDown(object sender, KeyEventArgs e)
        {

            bool state = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox_Barcode.Text.Length >= 12)
                {
                    FL00SSEntities DB = new FL00SSEntities();
                    string Barcode = textBox_Barcode.Text.Substring(0, 12);

                    BSTO Product_Item = new BSTO();

                    if (DB.BSTOes.Where(s => s.BSONU == Barcode).Any())
                    {
                        _SN++;
                        state = true;
                        LocationItem Location_Item = new LocationItem();
                        Product_Item = DB.BSTOes.Where(s => s.BSONU == Barcode).First();
                        Location_Item.Location = textBox_Location.Text;  //儲位
                        Location_Item.Location2 = textBox_Location2.Text;//備儲
                        Location_Item.ProductType = Product_Item.BSNCR; //型號
                        Location_Item.ProductColor = Product_Item.BSCLR;//顏色
                        Location_Item.SN = _SN;
                        Location_List.Add(Location_Item);


                    }

                }

                if (state)
                {
                    Upload();
            
                    textBox_Barcode.Text = "";
                    if (checkBoxHoad.Checked == true)
                    {
                        textBox_Barcode.Focus();
                    }
                    else
                    {
                        textBox_Location.Text = "";
                        textBox_Location2.Text = "";
                        textBox_Location.Focus();
                    }


                }
                else
                {
                    MessageBox.Show("查無此條碼。");
                    textBox_Barcode.Text = "";
                    textBox_Barcode.Focus();
                }
              
            }



        }



        private void button_Location_Empty_Click(object sender, EventArgs e)
        {
            textBox_Location.Text = "";
        }

        private void button_Location2_Empty_Click(object sender, EventArgs e)
        {
            textBox_Location2.Text = "";
        }

        private void button_Barcode_Empty_Click(object sender, EventArgs e)
        {
            textBox_Barcode.Text = "";
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

        private void button_Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);
                if (SelectCheck)
                {
                    string ID = Item.Cells["Column_SN"].Value.ToString();
                    int id = int.Parse(ID);
                    Location_List.Remove(Location_List.Where(s => s.SN == id).First());
                }
            }
            Update();
        }

        private void button_Empty_Click(object sender, EventArgs e)
        {
            Location_List.Clear();
            Update();

        }

        private void Upload()
        {
            dataGridView1.DataSource = Location_List;
        }

        private void button_Uplode_Click(object sender, EventArgs e)
        {
            IPAddress IP = IPAddress.Parse("192.168.0.219");
            if (ByPing(IP))
            {
                FL00SSEntities DB = new FL00SSEntities();
                foreach (DataGridViewRow Item in dataGridView1.Rows)
                {
                    string ProductType = Item.Cells["Column_ProductType"].Value.ToString();
                    string ProductColor = Item.Cells["Column_ProductColor"].Value.ToString();
                    BSTL ProductItem = DB.BSTLs.Where(s => s.BSNCR == ProductType && s.BSCLR == ProductColor).First();
                    ProductItem.BSPS3 = Item.Cells["Column_Location"].Value.ToString();
                    if (Item.Cells["Column_Location2"].Value.ToString() != "") //儲位不等於空白在儲存
                    {
                        ProductItem.BSPS4 = Item.Cells["Column_Location2"].Value.ToString();
                    }
                    DB.SaveChanges();
                }
                MessageBox.Show("上傳成功。");
                Location_List.Clear();
                Update();
            }
            else
            {
                MessageBox.Show("請確認網路環境。");

            }
        }

        public class LocationItem
        {
            public int SN { get; set; }
            public string Location { get; set; }
            public string Location2 { get; set; }
            public string ProductType { get; set; }
            public string ProductColor { get; set; }
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

        private void textBox_Location_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox_Barcode.Focus();
            }
        }
    }
}
