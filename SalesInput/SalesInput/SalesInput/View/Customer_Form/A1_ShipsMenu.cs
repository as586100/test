using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SalesInput.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.Customer_Form
{
    public partial class A1_ShipsMenu : Form
    {
        public A1_ShipsMenu()
        {
            InitializeComponent();

            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();

            List<NoticeA> noticeAList = pickupDBEntities1.NoticeAs.Where(s => s.WorkState=="W").ToList();
            dataGridView1.DataSource = noticeAList;
          

        }

        private void A1_ShipsMenu_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'pickupDBDataSet.NoticeA' 資料表。您可以視需要進行移動或移除。
            this.noticeATableAdapter.Fill(this.pickupDBDataSet.NoticeA);
            textBox1.Focus();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.noticeATableAdapter.FillBy(this.pickupDBDataSet.NoticeA);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.noticeATableAdapter.FillBy1(this.pickupDBDataSet.NoticeA);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void buttonPickup_Click(object sender, EventArgs e)
        {
            A2_NoticePickup pickup = new A2_NoticePickup(dataGridView1.SelectedRows[0].Cells["Column1"].Value.ToString());
            pickup.Show();
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PickupDBEntities1 DB = new PickupDBEntities1();
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                string workState = Item.Cells["Column11"].Value.ToString();
                string orderState = Item.Cells["Column12"].Value.ToString();
                int orderID   = int.Parse(Item.Cells["Column1"].Value.ToString());
                Item.Cells["Column14"].Value = DB.NoticeBs.Where(s => s.Maping == orderID).Count(); //件數

                switch (workState)
                {
                    case "Y":
                        Item.Cells["Column11"].Value = "已出貨";
                        break;
                    case "T":
                        Item.Cells["Column11"].Value = "已轉單";
                        break;
                    case "W":
                        Item.Cells["Column11"].Value = "待處理";
                        break;

                }
                switch (orderState)
                {
                    case "Y":
                        Item.Cells["Column12"].Value = "出貨";
                        break;
                    case "N":
                        Item.Cells["Column12"].Value = "缺貨";
                        break;
                    case "O":
                        Item.Cells["Column12"].Value = "部分缺";
                        break;
                    case "W":
                        Item.Cells["Column12"].Value = "待處理";
                        break;

                }



            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            string ID=  dataGridView1.SelectedRows[0].Cells["Column1"].Value.ToString();
            int orderID = int.Parse(ID);
            List<NoticeB> listNoticeB=  pickupDBEntities1.NoticeBs.Where(s => s.Maping == orderID).ToList();
            pickupDBEntities1.NoticeBs.RemoveRange(listNoticeB);
            NoticeA noticeA = pickupDBEntities1.NoticeAs.Find(orderID);
            pickupDBEntities1.NoticeAs.Remove(noticeA);
            pickupDBEntities1.SaveChanges();
            MessageBox.Show("刪除完成。");
            List<NoticeA> noticeAList = pickupDBEntities1.NoticeAs.Where(s => s.WorkState == "W").ToList();
            dataGridView1.DataSource = noticeAList;
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            IWorkbook wb = new HSSFWorkbook();//建立一份Excel
            ISheet ws = wb.CreateSheet("pickupList");
            ws.CreateRow(0);
            ws.CreateRow(1);
            ws.GetRow(0).CreateCell(0).SetCellValue("儲位");
            ws.GetRow(0).CreateCell(1).SetCellValue("庫存");
            ws.GetRow(0).CreateCell(2).SetCellValue("型號");
            ws.GetRow(0).CreateCell(3).SetCellValue("顏色");
            ws.GetRow(0).CreateCell(4).SetCellValue("尺寸");
            ws.GetRow(0).CreateCell(5).SetCellValue("條碼");
            ws.GetRow(0).CreateCell(6).SetCellValue("收件人");
           
            string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            FileStream file = new FileStream(ServerUrl + "\\tmp\\pickupList.xls",FileMode.Create);
            wb.Write(file);
            file.Close();
            Process.Start(ServerUrl + "\\tmp\\pickupList.xls");//打開檔案

        }


        private void CloseProcess(string ProcessName)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                Process[] array = ps;
                for (int i = 0; i < array.Length; i++)
                {
                    Process p = array[i];
                    bool flag = p.ProcessName == ProcessName;
                    if (flag)
                    {
                        p.CloseMainWindow();
                        p.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length >= 12)
                {
                    string barcode = textBox1.Text.Substring(0, 12);
                    PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
                    List<NoticeA> noticeAList = pickupDBEntities1.NoticeAs.Where(s => s.WorkState == "W").ToList();
                    List<NoticeA> noticeTempAList = new List<NoticeA>();
                    foreach (var item in noticeAList)
                    {
                        if (item.NoticeBs.Where(s => s.Barcode == barcode).Any())
                        {
                            noticeTempAList.Add(item);
                        }
                    }
                    dataGridView1.DataSource = noticeTempAList;
                    textBox1.Text = "";
                }

            }
        }
    }
}
