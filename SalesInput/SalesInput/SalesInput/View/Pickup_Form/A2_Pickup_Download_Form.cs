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
using SalesInput.Modle;
using SalesInput.Librarys;
using SalesInput.SqlString;
using SalesInput.ObjectFile;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace SalesInput.View.Pickup_Form
{
    public partial class A2_Pickup_Download_Form : Form
    {
        string Staff = "";//員工編號
        string warehouse = "3997";//倉庫代號
        public A2_Pickup_Download_Form(string staff)
        {

            InitializeComponent();
            Staff = staff;
            PickupDBEntities1 DB = new PickupDBEntities1();
            dataGridView1.DataSource = DB.PickupAs.Where(s => s.OrderSate == "N").ToList();
            textBoxOutput.Text = warehouse;
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    string type = ""; //單據類型
                    string output = "";//調出單位
                    string date = ""; //調撥日期
                    string orderID = Item.Cells[1].Value.ToString();//揀貨單號
                    output = textBoxOutput.Text;//調出單位
                    if (radioButton1.Checked)
                    {
                        type = "調撥單";
                    }//單據類型判定
                    else
                    {
                        type = "銷貨單";
                    }
                    date = dateTimePicker1.Text;//調撥日期          
                    OrderInfo orderInfo = new OrderInfo();
                    orderInfo.Order = orderID;
                    orderInfo.OrderType = type;
                    orderInfo.OrderOutput = output;
                    orderInfo.OrderDate = date;
                    A2_Pickup_Start_Form PS = new A2_Pickup_Start_Form(orderInfo);
                    //開啟揀貨視窗
                    PS.Show();
                }
            }
            this.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public class DownloadItem
        {
            public string 單號 { get; set; }
            public string 櫃號 { get; set; }
            public string 櫃名 { get; set; }
            public int 數量 { get; set; }
            public string 狀態 { get; set; }
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 DB = new PickupDBEntities1();
            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            TOM.SQLConnectionString = SQL.FILASQLConnectionString;


            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    string orderID = Item.Cells[1].Value.ToString();//單號
                    List<ReadyOrderB> RB = DB.ReadyOrderBs.Where(s => s.RB_OrderSN == orderID).ToList();
                    List<ReadyOrderA> RA = DB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == orderID).ToList();
                    List<PickupB> PB = DB.PickupBs.Where(s => s.OrderID == orderID).ToList();
                    PickupA A = DB.PickupAs.Find(orderID);
                    DB.ReadyOrderBs.RemoveRange(RB);
                    DB.ReadyOrderAs.RemoveRange(RA);
                    DB.PickupBs.RemoveRange(PB);
                    DB.PickupAs.Remove(A);
                    DB.SaveChanges();
                    TOM.SQLUpdateData(SQL.PickupRemove, Item.Cells[1].Value.ToString(), Staff);

                }
            }
            MessageBox.Show("刪除成功。");
            dataGridView1.DataSource = DB.PickupAs.Where(s => s.OrderSate == "N").ToList();
        }

        private void buttonTop10_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 DB = new PickupDBEntities1();

            dataGridView1.DataSource = DB.PickupAs.OrderByDescending(s => s.OrderID).Take(20).ToList();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 DB = new PickupDBEntities1();
            TomLibrary TOM = new TomLibrary();
            string OrderID = "";
            string tempOrderID = "";
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    tempOrderID = Item.Cells[1].Value.ToString();//單號
                    OrderID += " or [OrderID]='" + tempOrderID + "' ";

                }
            }

            TomLibrary tom = new TomLibrary();
            tom.SQLConnectionString = "";
            string sqlString = @"SELECT 
                                      [OrderID] AS 單號
                                      ,Order_Store  AS 櫃點
                                      ,Order_Location2  AS 備儲
,Order_Location  AS 儲位
,Order_Type  AS 型號
,Order_Color  AS 顏色
,Order_Size  AS 尺寸
,Order_Amount AS 應揀
,Order_PickAmount AS 實揀
,Order_Barcode  AS 條碼
                                  FROM [PickupDB].[dbo].[PickupB]  where [OrderID]=''  ";
            sqlString = sqlString + OrderID;
            SalesInputSQLstring salesInputSQLstring = new SalesInputSQLstring();
            tom.SQLConnectionString = salesInputSQLstring.PickupConnectionString;
            DataTable data = tom.SQLGetData(sqlString);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
            saveFileDialog.Title = "Save File";
            saveFileDialog.ShowDialog();
            string savepos = saveFileDialog.FileName;

            tom.DataTableToExcelFile(data, savepos);
            MessageBox.Show("存檔完成。");



        }
    }
}
