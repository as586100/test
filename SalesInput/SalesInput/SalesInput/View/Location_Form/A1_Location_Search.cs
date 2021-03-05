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
    public partial class A1_Location_Search : Form
    {
        public A1_Location_Search()
        {
            InitializeComponent();
        }
        int _SN = 0;
        BindingList<LocationItem> Location_List = new BindingList<LocationItem>();
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            bool state = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length >= 12)
                {
                    FL00SSEntities DB = new FL00SSEntities();
                    string Barcode = textBox1.Text.Substring(0, 12);

                    BSTO Product_Item = new BSTO();

                    if (DB.BSTOes.Where(s => s.BSONU == Barcode).Any())
                    {
                        _SN++;
                        state = true;
                    
                        Product_Item = DB.BSTOes.Where(s => s.BSONU == Barcode).First();
                       if(DB.BSTLs.Where(s => s.BSNCR == Product_Item.BSNCR && s.BSCLR == Product_Item.BSCLR).Any())
                        {

                           BSTL BSTL_Item =  DB.BSTLs.Where(s => s.BSNCR == Product_Item.BSNCR && s.BSCLR == Product_Item.BSCLR).First();
                            LocationItem Litem = new LocationItem();
                            Litem.SN = _SN;//序號
                            Litem.ProductType = BSTL_Item.BSNCR;//型號
                            Litem.ProductColor = BSTL_Item.BSCLR;//顏色
                            Litem.Location = BSTL_Item.BSPS3;//儲位1
                            Litem.Location2 = BSTL_Item.BSPS4;//儲位2
                            Location_List.Add(Litem);
                        }
                    }
                }
                if (state)
                {
                    Upload();
                }
                else
                {
                    MessageBox.Show("查無此條碼。");
                }
                textBox1.Text = "";
                textBox1.Focus();
            }
        }


        private void Upload()
        {
            dataGridView1.DataSource = Location_List;
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


        private void buttonClearText_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void buttonClearView_Click(object sender, EventArgs e)
        {
            Location_List.Clear();
            Update();
        }

        private void button_Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
