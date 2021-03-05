using SalesInput.Librarys;
using SalesInput.SqlString;
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
using System.Net;
using System.Net.NetworkInformation;

namespace SalesInput.View.Customer_Form
{
    public partial class A2_NoticePickup : Form
    {
        int tempID;
        public A2_NoticePickup(string ID)
        {
            InitializeComponent();
            textBoxBarcode.Focus();
            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            tempID = int.Parse(ID);
            List<NoticeB> noticeBList = pickupDBEntities1.NoticeBs.Where(s => s.Maping == tempID).ToList();
            dataGridView1.DataSource = noticeBList;
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            bool state = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxBarcode.Text.Length >= 12)
                {
                    string Barcode = textBoxBarcode.Text.Substring(0, 12);
                    PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();

                    List<NoticeB> noticeBList = pickupDBEntities1.NoticeBs.Where(s => s.Maping == tempID).ToList();
                    NoticeB noticeB = noticeBList.Where(s => s.Barcode == Barcode).First();
                    if (noticeB.Demand - noticeB.Shipment > 0)
                    {
                        noticeB.Shipment++;
                        pickupDBEntities1.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("此型號已經滿足。");
                    }
                    dataGridView1.DataSource = noticeBList;
                }
                textBoxBarcode.Clear();
                textBoxBarcode.Focus();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                int Demand = int.Parse(Item.Cells["Column6"].Value.ToString());
                int Shipment = int.Parse(Item.Cells["Column7"].Value.ToString());

                if (Demand - Shipment <= 0)
                {
                    dataGridView1.Rows[Item.Index].DefaultCellStyle.BackColor = Color.Pink;
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_Done_Click(object sender, EventArgs e)
        {
            button_Done.Enabled = false;

            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            NoticeA noticeA = pickupDBEntities1.NoticeAs.Find(tempID);
            bool turn = true;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                int aAmount = int.Parse(item.Cells["Column6"].Value.ToString()); //需求量
                int bAmount = int.Parse(item.Cells["Column7"].Value.ToString()); //出貨量

                if (aAmount != bAmount)
                {
                    turn = false;
                }
            }

            if (!turn)
            {
                DialogResult dr = MessageBox.Show("需求量不等於出貨量!確定要執行出貨?", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    noticeA.WorkState = "T";
                    pickupDBEntities1.SaveChanges();
                    this.Close();
                }
            }
            else
            {
                noticeA.WorkState = "T";
                pickupDBEntities1.SaveChanges();

                TurnLibrary turnLibrary = new TurnLibrary();
                turnLibrary.Trun(tempID);
                PrintLibrary printLibrary = new PrintLibrary();
                printLibrary.PrintHCTLabel(tempID);
                //MessageBox.Show("出貨完成，請轉單");
                //this.Close();
                button_Done.Enabled = true;
            }

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            TomLibrary library = new TomLibrary();
            SalesInputSQLstring salesInputSql = new SalesInputSQLstring();
            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            library.SQLConnectionString = salesInputSql.FILASQLConnectionString;
            NoticeA noticeA = pickupDBEntities1.NoticeAs.Find(tempID);
            string pos_NoticA_ID = noticeA.NoticeMaping;
            noticeA.OrderState = "N";
            noticeA.WorkState = "N";
            library.SQLUpdateData(salesInputSql.updateNoticeA, "N", "", "", pos_NoticA_ID);
            MessageBox.Show("已通知缺貨。");
            pickupDBEntities1.SaveChanges();
            this.Close();

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
