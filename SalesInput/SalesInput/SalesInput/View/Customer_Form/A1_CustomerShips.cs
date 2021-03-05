using SalesInput.Librarys;
using SalesInput.SqlString;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SalesInput.Modle;
using System.Diagnostics;

namespace SalesInput.View.Customer_Form
{
    public partial class A1_CustomerShips_Form : Form
    {
        /// <summary>
        /// 初始化設定
        /// </summary>
        public A1_CustomerShips_Form()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 查詢單據
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Search_Click(object sender, EventArgs e)
        {
            try
            {
                TomLibrary TOM = new TomLibrary();
                SalesInputSQLstring SQL = new SalesInputSQLstring();
                TOM.SQLConnectionString = SQL.FILASQLConnectionString;
                //string SqlString = SQL.NoticeADetail;

                StringBuilder SqlStringBuilder = new StringBuilder();
                SqlStringBuilder.Append(SQL.NoticeADetail);

                if (dateTimePicker1.Text != " ")
                {
                    SqlStringBuilder.AppendFormat(" and ( [Date] between '{0}' and '{1}' ) ", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"));
                    //SqlString += " and ( [Date] between'" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '"+dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") +"')";
                }
                if (checkBox1.Checked == false)
                {
                    SqlStringBuilder.Append(" and [IsDownload] is null ");
                    //SqlString += " and [IsDownload]   is null ";
                }
                else
                {
                    SqlStringBuilder.Append(" and [IsDownload]='Y' ");
                    //SqlString += " and [IsDownload]='Y' ";
                }
                switch (comboBox1.SelectedItem)
                {
                    case "待處理":
                        SqlStringBuilder.Append(" and [Status]='W' ");
                        //SqlString += "and [Status]='W'";
                        break;
                    case "出貨":
                        SqlStringBuilder.Append(" and [Status]='Y' ");
                        //SqlString += "and [Status]='Y'";
                        break;
                    case "缺貨":
                        SqlStringBuilder.Append(" and [Status]='N' ");
                        //SqlString += "and [Status]='N'";
                        break;
                }



                DataTable Table = TOM.SQLGetData(SqlStringBuilder.ToString());
                dataGridView_noticeOnline.DataSource = Table;


            }
            catch { MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。"); }
        }

        private void dataGridView_noticeOnline_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                bool SelectCheck = (null != dataGridView_noticeOnline.Rows[e.RowIndex].Cells[0] && null != dataGridView_noticeOnline.Rows[e.RowIndex].Cells[0].Value && true == (bool)dataGridView_noticeOnline.Rows[e.RowIndex].Cells[0].Value);
                if (SelectCheck)
                {
                    SelectCheck = false;
                }
                else { SelectCheck = true; }

                dataGridView_noticeOnline.Rows[e.RowIndex].Cells[0].Value = SelectCheck;
            }
        }

        private void button_describe_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView_noticeOnline.Rows)
            {
                Application.DoEvents();
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);
                if (SelectCheck)
                {
                    A2_ItemList itemList = new A2_ItemList(Item.Cells["ID"].Value.ToString());
                    itemList.Show();
                }
            }
        }

        /// <summary>
        /// 下載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Download_Click(object sender, EventArgs e)
        {
            try
            {
                FL00SSEntities fL00SS = new FL00SSEntities();
                PickupDBEntities1 pickupDB = new PickupDBEntities1();
                SalesInputSQLstring SQL = new SalesInputSQLstring();
                TomLibrary TOM = new TomLibrary();
                foreach (DataGridViewRow Item in dataGridView_noticeOnline.Rows)
                {
                    Application.DoEvents();
                    bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);
                    if (SelectCheck)
                    {
                        string ID = Item.Cells["col_ID"].Value.ToString(); //取得ID


                        TOM.SQLConnectionString = SQL.FILASQLConnectionString; //SQL連線字串

                        StringBuilder noticeA_SqlStringBuilder = new StringBuilder();
                        noticeA_SqlStringBuilder.Append(SQL.NoticeA);//取noticeA SQL 字串

                        //string noticeA_SqlString = SQL.NoticeA;//取noticeA SQL 字串

                        noticeA_SqlStringBuilder.AppendFormat("'{0}'", ID);
                        //noticeA_SqlString += "'" + ID + "'";//取noticeA SQL 字串

                        DataTable tableNoticeA = TOM.SQLGetData(noticeA_SqlStringBuilder.ToString()); //取得noticeA明細
                        NoticeA noticeA = new NoticeA
                        {
                            Date = DateTime.Parse(tableNoticeA.Rows[0][1].ToString()), //noticeA 日期
                            Require = tableNoticeA.Rows[0][2].ToString(),//noticeA 需求單位
                            Response = tableNoticeA.Rows[0][3].ToString(),//noticeA 回覆單位
                            Name = tableNoticeA.Rows[0][4].ToString(),//noticeA 姓名
                            Tel = tableNoticeA.Rows[0][5].ToString(),//noticeA 電話
                            Address = tableNoticeA.Rows[0][6].ToString(),//noticeA 地址
                            Remark = tableNoticeA.Rows[0][7].ToString(),//noticeA 備註
                            NoticeMaping = tableNoticeA.Rows[0][0].ToString(),//noticeA 對應單號
                            WorkState = "W",//noticeA 作業狀態
                            OrderState = tableNoticeA.Rows[0][8].ToString()//noticeA 單據狀態
                        };//建立一個noticeA物件     
                        pickupDB.NoticeAs.Add(noticeA);

                        pickupDB.SaveChanges();//儲存

                        StringBuilder noticeB_SqlStringBuilder = new StringBuilder();
                        noticeB_SqlStringBuilder.Append(SQL.NoticeBDetail);//取noticeB SQL 字串

                        //string noticeB_SqlString = SQL.NoticeBDetail;//取noticeB SQL 字串

                        noticeB_SqlStringBuilder.AppendFormat("'{0}'", ID);

                        //noticeB_SqlString += "'" + ID + "'";//取noticeB SQL 字串

                        DataTable tableNoticeB = TOM.SQLGetData(noticeB_SqlStringBuilder.ToString()); //取得noticeB明細
                        foreach (DataRow item in tableNoticeB.Rows)
                        {
                            string typeName = item[0].ToString();  //型號
                            string color = item[1].ToString();     //顏色 
                            string size = item[2].ToString();     //尺寸
                            int amount = int.Parse(item[3].ToString());     //數量
                            string barcode = fL00SS.BSTOes.Where(s => s.BSNCR == typeName && s.BSCLR == color && s.BSSIZ == size).Any() ? fL00SS.BSTOes.Where(s => s.BSNCR == typeName && s.BSCLR == color && s.BSSIZ == size).First().BSONU : "";//條碼
                            string location1 = fL00SS.BSTLs.Where(s => s.BSNCR == typeName && s.BSCLR == color).First().BSPS3 ?? "";//儲位1
                            string location2 = fL00SS.BSTLs.Where(s => s.BSNCR == typeName && s.BSCLR == color).First().BSPS4 ?? "";//儲位2
                            NoticeB noticeB = new NoticeB
                            {
                                Barcode = barcode, //條碼
                                TypeName = typeName,//型號
                                Color = color,//顏色
                                Size = size,//尺寸
                                Demand = amount,//需求量
                                Shipment = 0,//出貨量
                                Location1 = location1,//儲位1
                                Location2 = location2,//儲位2
                                Maping = noticeA.ID//對應ID
                            };//noticeB物件
                            pickupDB.NoticeBs.Add(noticeB);//新增至DB
                        }
                        pickupDB.SaveChanges();//儲存


                        string updateDownloadSqlString = "  update [FILA].[dbo].[POS_NoticeA] set [IsDownload]='Y' WHERE [ID]=@a";
                        TOM.SQLUpdateData(updateDownloadSqlString, ID, ""); //更新狀態下載狀態Y





                    }
                }
                //重新載入Gridview
                TOM.SQLConnectionString = SQL.FILASQLConnectionString;

                StringBuilder SqlStringBuilder = new StringBuilder();
                SqlStringBuilder.Append(SQL.NoticeADetail);
                //string SqlString = SQL.NoticeADetail;
                if (dateTimePicker1.Text != " ")
                {
                    SqlStringBuilder.AppendFormat("and [Date] ='{0}'", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
                    //SqlString += " and [Date] ='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "'";

                }
                if (checkBox1.Checked == false)
                {
                    SqlStringBuilder.Append(" and [IsDownload] is null ");
                    //SqlString += " and [IsDownload] is null ";
                }
                else
                {
                    SqlStringBuilder.Append(" and [IsDownload]='Y' ");
                    //SqlString += " and [IsDownload]='Y' ";
                }
                DataTable Table = TOM.SQLGetData(SqlStringBuilder.ToString());
                dataGridView_noticeOnline.DataSource = Table;
                switch (comboBox1.SelectedItem)
                {
                    case "待處理":
                        SqlStringBuilder.Append(" and [Status]='W' ");
                        //SqlString += "and [Status]='W'";
                        break;
                    case "出貨":
                        SqlStringBuilder.Append(" and [Status]='Y' ");
                        //SqlString += "and [Status]='Y'";
                        break;
                    case "缺貨":
                        SqlStringBuilder.Append(" and [Status]='N' ");
                        //SqlString += "and [Status]='N'";
                        break;
                }
                MessageBox.Show("下載完成。"); //訊息提示
            }
            catch(Exception ex)
            {
                MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。");
            }
        }

        private void dataGridView_noticeOnline_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView_noticeOnline.Rows)
            {

                string orderState = Item.Cells["Column5"].Value.ToString();



                switch (orderState)
                {
                    case "Y":
                        Item.Cells["Column5"].Value = "出貨";
                        break;
                    case "N":
                        Item.Cells["Column5"].Value = "缺貨";
                        break;
                    case "O":
                        Item.Cells["Column5"].Value = "部分缺";
                        break;
                    case "W":
                        Item.Cells["Column5"].Value = "待處理";
                        break;

                }



            }
        }


        /// <summary>
        /// 列印揀貨總表(原為下載Excel)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Print_Click(object sender, EventArgs e)
        {

            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            TOM.SQLConnectionString = SQL.FILASQLConnectionString;
            StringBuilder SqlStringBuilder = new StringBuilder();
            SqlStringBuilder.Append(SQL.NoticeDetail_Print);
            //string SqlString = SQL.NoticeDetail_Excel;


            if (dateTimePicker1.Text != " ")
            {
                SqlStringBuilder.AppendFormat(" and ( [Date] between'{0}' and '{1}')", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"));
                //SqlString += " and ( [Date] between'" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "')";

            }
            if (checkBox1.Checked == false)
            {
                SqlStringBuilder.Append(" and [IsDownload]   is null ");
                //SqlString += " and [IsDownload]   is null ";
            }
            else
            {
                SqlStringBuilder.Append("and [IsDownload]='Y' ");
                //SqlString += " and [IsDownload]='Y' ";
            }
            switch (comboBox1.SelectedItem)
            {
                case "待處理":
                    SqlStringBuilder.Append(" and [POS_NoticeA].[Status]='W' ");
                    //SqlString += "and [POS_NoticeA].[Status]='W'";
                    break;
                case "出貨":
                    SqlStringBuilder.Append(" and [POS_NoticeA].[Status]='Y'  ");
                    //SqlString += "and [POS_NoticeA].[Status]='Y'";
                    break;
                case "缺貨":
                    SqlStringBuilder.Append(" and [POS_NoticeA].[Status]='N' ");
                    //SqlString += "and [POS_NoticeA].[Status]='N'";
                    break;
            }

            SqlStringBuilder.Append(" group by TypeName, Color, Size ");

            DataTable Table = TOM.SQLGetData(SqlStringBuilder.ToString());
            TomLibrary tomLibrary = new TomLibrary();
            FL00SSEntities fL00SS = new FL00SSEntities();
            List<NoticeDetail> noticeDetailList = new List<NoticeDetail>();
            foreach (DataRow item in Table.Rows)
            {
                NoticeDetail noticeDetail = new NoticeDetail();
                noticeDetail.TypeName = item[0].ToString();//型號
                noticeDetail.Color = item[1].ToString();//顏色
                noticeDetail.Size = item[2].ToString();//尺寸
                noticeDetail.Amount = int.Parse(item[3].ToString());//數量
                //noticeDetail.Customer = item[4].ToString();//代寄客
                noticeDetail.Location_1 = fL00SS.BSTLs.Where(s => s.BSNCR == noticeDetail.TypeName && s.BSCLR == noticeDetail.Color).First().BSPS3 ?? "";//儲位
                //noticeDetail.Location_2 = fL00SS.BSTLs.Where(s => s.BSNCR == noticeDetail.TypeName && s.BSCLR == noticeDetail.Color).First().BSPS4 ?? "";//儲位2
                noticeDetailList.Add(noticeDetail);//增加到List              
            }

            PrintLibrary printLibrary = new PrintLibrary();
            printLibrary.PrintLabel("TSC TTP-244CE", noticeDetailList);

            #region 下載Excel(原功能)
            //this.CloseProcess("EXCEL"); //結束Excel 程序
            //IWorkbook wb = new HSSFWorkbook();
            //ISheet ws = wb.CreateSheet("Sheet1");
            //ws.CreateRow(0);
            //ws.GetRow(0).CreateCell(0).SetCellValue("儲位1");
            //ws.GetRow(0).CreateCell(1).SetCellValue("型號");
            //ws.GetRow(0).CreateCell(2).SetCellValue("顏色");
            //ws.GetRow(0).CreateCell(3).SetCellValue("尺寸");
            //ws.GetRow(0).CreateCell(4).SetCellValue("數量");
            //ws.GetRow(0).CreateCell(5).SetCellValue("客人姓名");
            //ws.GetRow(0).CreateCell(6).SetCellValue("儲位2");

            //int rowCount = 1;
            //foreach (var item in noticeExcelDetailList.OrderBy(s => s.Location_1)) //儲位排序
            //{
            //    ws.CreateRow(rowCount);//建立Row
            //    ws.GetRow(rowCount).CreateCell(0).SetCellValue(item.Location_1);//儲位1
            //    ws.GetRow(rowCount).CreateCell(1).SetCellValue(item.TypeName);//型號
            //    ws.GetRow(rowCount).CreateCell(2).SetCellValue(item.Color);//顏色
            //    ws.GetRow(rowCount).CreateCell(3).SetCellValue(item.Size);//尺寸
            //    ws.GetRow(rowCount).CreateCell(4).SetCellValue(item.Amount);//數量
            //    ws.GetRow(rowCount).CreateCell(5).SetCellValue(item.Customer);//客人姓名
            //    ws.GetRow(rowCount).CreateCell(6).SetCellValue(item.Location_2); //儲位2
            //    rowCount++;//增加一筆
            //}

            //string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            //FileStream file = new FileStream(ServerUrl + "\\tmp\\" + "PickupDetail" + ".xls", FileMode.Create);//建立檔案
            //wb.Write(file);//寫入檔案
            //file.Close();//檔案關閉
            //Process.Start(ServerUrl + "\\tmp\\" + "PickupDetail.xls");//開啟excel
            #endregion
        }
        private void CloseProcess(string ProcessName)//關閉程序
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

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView_noticeOnline.Rows)
            {
                Item.Cells[0].Value = true;
            }
        }

        private void buttonUnselect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView_noticeOnline.Rows)
            {
                Item.Cells[0].Value = false;
            }
        }
    }
    /// <summary>
    /// 單據明細
    /// </summary>
    public class NoticeDetail
    {
        /// <summary>
        /// 儲位
        /// </summary>
        public string Location_1 { get; set; }
        ///// <summary>
        ///// 備用儲位
        ///// </summary>
        //public string Location_2 { get; set; }
        /// <summary>
        /// 型號
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 顏色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        public int Amount { get; set; }
        ///// <summary>
        ///// 客人
        ///// </summary>
        //public string Customer { get; set; }
    }
}
