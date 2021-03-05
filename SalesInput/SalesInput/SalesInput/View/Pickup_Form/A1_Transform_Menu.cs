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
using SalesInput.View.Pickup_Form;
using SalesInput.SqlString;
using SalesInput.Librarys;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using Novacode;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;

namespace SalesInput.View.Transform_Form
{
    public partial class A1_Transform_Menu : Form
    {

        string warehouse = "3997";//倉庫代號
        bool Select = true;
        string Order = "";
        DateTime Date = DateTime.Now.Date;
        public A1_Transform_Menu(string order)
        {
            InitializeComponent();
            textBox1.Text = warehouse;
            Order = order;
            PickupDBEntities1 PickDB = new PickupDBEntities1();
            if (PickDB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == Order).Any())
            {
                dataGridView1.DataSource = PickDB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == Order).ToList();
            }

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            bool OrderState = true;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                if (Item.Cells[6].Value.ToString() == "N")
                {
                    OrderState = false;
                }
            }

            if (OrderState)
            {

                PickupDBEntities1 PickDB = new PickupDBEntities1();
                PickupA PK_A = PickDB.PickupAs.Where(s => s.OrderID == Order).First();
                PK_A.OrderTrunState = "Y";
                PickDB.SaveChanges();
            }
            this.Close();
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                Item.Cells[0].Value = Select;
            }
            if (Select)
            {
                Select = false;
            }
            else
            {
                Select = true;
            }

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

        private void button_Start_Click(object sender, EventArgs e)
        {
            warehouse = textBox1.Text;
            IPAddress IP = IPAddress.Parse("192.168.0.219");
            if (ByPing(IP))
            {
                try
                {
                    FL00SSEntities Erp = new FL00SSEntities();
                    PickupDBEntities1 PickDB = new PickupDBEntities1();

                    //--------------單頭-------------------------------------------------------
                    foreach (DataGridViewRow Item in dataGridView1.Rows)
                    {
                        bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                        if (SelectCheck)
                        {

                            if (Item.Cells[4].Value == null)
                            {
                                string ID = Item.Cells["RA_ID"].Value.ToString();
                                int OrderID = int.Parse(ID);
                                ReadyOrderA Ready_A = PickDB.ReadyOrderAs.Where(s => s.RA_ID == OrderID).First();//Ready A
                                string Staff = Item.Cells["RA_Staff"].Value.ToString(); //開單人
                                string Store = Item.Cells["RA_Store"].Value.ToString(); //門市
                                string RA_Maping_PU_ID = Item.Cells["RA_Maping_PU_ID"].Value.ToString();//揀貨單號
                                string RA_Box_ID= Item.Cells["RA_BoxID"].Value.ToString();//箱號
                                int Amount = int.Parse(Item.Cells["RA_Amount"].Value.ToString());
                                string Temp_Date_SN = Date.ToString("yyMM");//日期-單號用                                   
                                string Temp_SN = "M" + Temp_Date_SN + Staff;         //M+日期+開單人
                                string ERP_OrderSN = "";                             //ERP新單號

                                if (Erp.RSIMAs.Where(s => s.RAREN.StartsWith(Temp_SN)).Any())
                                {
                                    ERP_OrderSN = Erp.RSIMAs.Where(s => s.RAREN.StartsWith(Temp_SN)).OrderByDescending(s => s.RAREN).First().RAREN;
                                    string sort = ERP_OrderSN.Substring(10, 4);
                                    int Sort = int.Parse(sort) + 1;
                                    sort = string.Format("{0:0000}", Sort);
                                    ERP_OrderSN = Temp_SN + sort;
                                }
                                else
                                {
                                    ERP_OrderSN = Temp_SN + "0001";
                                }

                                RSIMA A = new RSIMA();
                                A.RAREN = ERP_OrderSN;//單號
                                A.RACNS = warehouse; //調出單位
                                A.RACN2 = Store;//調入單位
                                A.RADAY = Date;//調撥日期
                                A.RACLS = "M";//進銷存類別
                                A.RARMA = RA_Maping_PU_ID+"-"+ RA_Box_ID;//備註
                                A.RAMEN = Staff;//開單人員
                                A.RATOL = 0;//合計金額
                                A.RAQTY = Amount;//合計數量
                                A.TRDAT = DateTime.Now;//修改日期
                                A.TRMOD = "U";//異動型態
                                A.TRUSR = Staff;//異動人員
                                A.RAKIN = "01";//調撥說明
                                A.RAXDT = Date;//開單時間
                                Erp.RSIMAs.Add(A);
                                //Erp.SaveChanges();
                                Ready_A.RA_Maping_ERP_ID = ERP_OrderSN;//Ready A單頭 erp對應序號

                                //PickDB.SaveChanges();//Ready A單頭儲存
                                //------------------單身----------------------------------------------------------
                                List<ReadyOrderB> ReadyB = PickDB.ReadyOrderBs.Where(s => s.RB_Maping_RA_ID == OrderID).ToList();

                                int itemSN = 0;
                                List<string> TempProduct = new List<string>();
                                List<RSIMB> TempRsimb = new List<RSIMB>();
                                foreach (var item in ReadyB)
                                {
                                    SalesInputSQLstring Sql = new SalesInputSQLstring();
                                    TomLibrary Tom = new TomLibrary();
                                    Tom.SQLConnectionString = Sql.SQLConnectionString;
                                    string SqlQueryString = Sql.SQL_ProductDetail;
                                    System.Data.DataTable SQL_Data = Tom.SQLGetData(SqlQueryString, item.RB_Barcode); //DataSource


                                    string Name = SQL_Data.Rows[0][0].ToString();
                                    string Color = SQL_Data.Rows[0][1].ToString();
                                    string NameColor = Name + Color;
                                    //判斷若型號顏色重覆
                                    if (!TempProduct.Where(s => s == NameColor).Any())
                                    {
                                        TempProduct.Add(NameColor);
                                        itemSN++;
                                        String itemSN0 = string.Format("{0:0000}", itemSN);
                                        RSIMB B = new RSIMB();
                                        B.RBREN = ERP_OrderSN;//單號
                                        B.RBITM = itemSN0;//項次
                                        B.RBCNS = warehouse;//分倉編號
                                        B.RBCN2 = Store;//對應分倉
                                        B.RBCLS = "M";//進銷存類別
                                        B.RBCOS = "3";//類別
                                        B.RBNCR = SQL_Data.Rows[0][0].ToString();//型號
                                        B.RBCLR = SQL_Data.Rows[0][1].ToString();//顏色
                                        B.RBDC1 = item.RB_Amount;//數量加減
                                        B.RBQTY = item.RB_Amount;//數量(調撥量)
                                        B.RBRQY = 0;//數量(驗收量)
                                        B.RBCQY = 0;//數量(取消量)
                                        B.RBFQY = item.RB_Amount;//數量(差異量)
                                        B.RBRMK = "";//備註
                                        B.RBCLZ = SQL_Data.Rows[0][3].ToString();//尺寸類別
                                        switch (SQL_Data.Rows[0][4].ToString())//尺寸數量
                                        {
                                            case "RBQTY01":
                                                B.RBQTY01 = item.RB_Amount;
                                                break;
                                            case "RBQTY02":
                                                B.RBQTY02 = item.RB_Amount; ;
                                                break;
                                            case "RBQTY03":
                                                B.RBQTY03 = item.RB_Amount;
                                                break;
                                            case "RBQTY04":
                                                B.RBQTY04 = item.RB_Amount;
                                                break;
                                            case "RBQTY05":
                                                B.RBQTY05 = item.RB_Amount;
                                                break;
                                            case "RBQTY06":
                                                B.RBQTY06 = item.RB_Amount;
                                                break;
                                            case "RBQTY07":
                                                B.RBQTY07 = item.RB_Amount;
                                                break;
                                            case "RBQTY08":
                                                B.RBQTY08 = item.RB_Amount;
                                                break;
                                            case "RBQTY09":
                                                B.RBQTY09 = item.RB_Amount;
                                                break;
                                            case "RBQTY10":
                                                B.RBQTY10 = item.RB_Amount;
                                                break;
                                            case "RBQTY11":
                                                B.RBQTY11 = item.RB_Amount;
                                                break;
                                            case "RBQTY12":
                                                B.RBQTY12 = item.RB_Amount;
                                                break;
                                            case "RBQTY13":
                                                B.RBQTY13 = item.RB_Amount;
                                                break;
                                            case "RBQTY14":
                                                B.RBQTY14 = item.RB_Amount;
                                                break;
                                            case "RBQTY15":
                                                B.RBQTY15 = item.RB_Amount;
                                                break;
                                            case "RBQTY16":
                                                B.RBQTY16 = item.RB_Amount;
                                                break;
                                            case "RBQTY17":
                                                B.RBQTY17 = item.RB_Amount;
                                                break;
                                            case "RBQTY18":
                                                B.RBQTY18 = item.RB_Amount;
                                                break;
                                            case "RBQTY19":
                                                B.RBQTY19 = item.RB_Amount;
                                                break;
                                            case "RBQTY20":
                                                B.RBQTY20 = item.RB_Amount;
                                                break;
                                        }
                                        //Erp.RSIMBs.Add(B);
                                        TempRsimb.Add(B);
                                    }
                                    else
                                    {
                                        //若型號顏色重覆
                                        RSIMB B = TempRsimb.Where(s => s.RBREN == ERP_OrderSN && s.RBNCR == Name && s.RBCLR == Color).First();
                                        B.RBDC1 += item.RB_Amount;//數量加減
                                        B.RBQTY += item.RB_Amount;//數量(調撥量)
                                        B.RBFQY += item.RB_Amount;//數量(差異量)
                                        switch (SQL_Data.Rows[0][4].ToString())//尺寸數量
                                        {
                                            case "RBQTY01":
                                                if (B.RBQTY01 != null)
                                                {
                                                    B.RBQTY01 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY01 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY02":
                                                if (B.RBQTY02 != null)
                                                {
                                                    B.RBQTY02 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY02 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY03":
                                                if (B.RBQTY03 != null)
                                                {
                                                    B.RBQTY03 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY03 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY04":
                                                if (B.RBQTY04 != null)
                                                {
                                                    B.RBQTY04 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY04 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY05":
                                                if (B.RBQTY05 != null)
                                                {
                                                    B.RBQTY05 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY05 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY06":
                                                if (B.RBQTY06 != null)
                                                {
                                                    B.RBQTY06 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY06 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY07":
                                                if (B.RBQTY07 != null)
                                                {
                                                    B.RBQTY07 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY07 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY08":
                                                if (B.RBQTY08 != null)
                                                {
                                                    B.RBQTY08 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY08 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY09":
                                                if (B.RBQTY09 != null)
                                                {
                                                    B.RBQTY09 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY09 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY10":
                                                if (B.RBQTY10 != null)
                                                {
                                                    B.RBQTY10 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY10 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY11":
                                                if (B.RBQTY11 != null)
                                                {
                                                    B.RBQTY11 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY11 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY12":
                                                if (B.RBQTY12 != null)
                                                {
                                                    B.RBQTY12 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY12 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY13":
                                                if (B.RBQTY13 != null)
                                                {
                                                    B.RBQTY13 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY13 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY14":
                                                if (B.RBQTY14 != null)
                                                {
                                                    B.RBQTY14 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY14 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY15":
                                                if (B.RBQTY15 != null)
                                                {
                                                    B.RBQTY15 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY15 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY16":
                                                if (B.RBQTY16 != null)
                                                {
                                                    B.RBQTY16 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY16 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY17":
                                                if (B.RBQTY17 != null)
                                                {
                                                    B.RBQTY17 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY17 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY18":
                                                if (B.RBQTY18 != null)
                                                {
                                                    B.RBQTY18 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY18 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY19":
                                                if (B.RBQTY19 != null)
                                                {
                                                    B.RBQTY19 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY19 = item.RB_Amount;
                                                }
                                                break;
                                            case "RBQTY20":
                                                if (B.RBQTY20 != null)
                                                {
                                                    B.RBQTY20 += item.RB_Amount;
                                                }
                                                else
                                                {
                                                    B.RBQTY20 = item.RB_Amount;
                                                }
                                                break;
                                        }

                                    }



                                }
                                Erp.RSIMBs.AddRange(TempRsimb);
                                Erp.SaveChanges();
                                PickDB.SaveChanges();//Ready A單頭儲存
                                MessageBox.Show("轉單完成。");
                            }
                            else
                            {
                                MessageBox.Show("此單已轉。");
                            }
                        }

                    }
                    if (PickDB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == Order).Any())
                    {
                        dataGridView1.DataSource = PickDB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == Order).ToList();
                    }
                }
                catch
                {
                    MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。");
                }
            }
            else { MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。"); }

        }

        private void button_Detail_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 PickDB = new PickupDBEntities1();
            string IndexString = "";
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    IndexString = Item.Cells["RA_ID"].Value.ToString();
                    A2_Detail_Form DF = new A2_Detail_Form(IndexString);
                    DF.Show();
                }
            }



        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            PickupDBEntities1 PickDB = new PickupDBEntities1();
            System.Windows.Forms.Application.DoEvents();
            this.CloseProcess("WINWORD");
            this.Cursor = Cursors.WaitCursor;
            FL00SSEntities erpDB = new FL00SSEntities();
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            TomLibrary Tom = new TomLibrary();
            List<string> OrderUrl = new List<string>();
            string ServerUrl = Environment.CurrentDirectory;
            try
            {
                this.DeleteTempFolder(ServerUrl + "\\tmp\\");
            }
            catch
            {
            }
            Tom.SQLConnectionString = Sql.SQLConnectionString;
            string SqlQueryString = Sql.TransferOrderHeader;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    if (Item.Cells[4].Value != null)
                    {
                        int ID = int.Parse(Item.Cells[1].Value.ToString());
                        ReadyOrderA A = PickDB.ReadyOrderAs.Where(s => s.RA_ID == ID).First();
                        A.RA_State = "Y";
                        PickDB.SaveChanges();
                        SqlQueryString += " or  RAREN='" + Item.Cells[4].Value.ToString() + "' ";
                    }
                    else
                    {
                        MessageBox.Show("尚未轉單。");
                    }

                }
            }

            System.Data.DataTable DataTableOrderHeader = Tom.SQLGetData(SqlQueryString);
            bool flag8 = DataTableOrderHeader.Rows.Count != 0;
            if (flag8)
            {

                foreach (DataRow HeaderItem in DataTableOrderHeader.Rows)
                {
                    System.Windows.Forms.Application.DoEvents();
                    string OrderNum = HeaderItem[0].ToString();
                    string OrderOutput = HeaderItem[1].ToString();
                    string OrderIntput = HeaderItem[2].ToString();
                    string OrderDate = string.Format("{0:yyyy/MM/dd}", HeaderItem[8]);
                    string OrderMark = HeaderItem[11].ToString();
                    string OrderAddress = HeaderItem[25].ToString();
                    string OrderDescribe = HeaderItem[33].ToString();
                    string OrderName = HeaderItem[12].ToString();
                    DocX wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report3.docx");
                    wordDocument.ReplaceText("[$單號$]", OrderNum, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$調出$]", OrderOutput, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$調入$]", OrderIntput, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$日期$]", OrderDate, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$備註$]", OrderMark, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$地址$]", OrderAddress, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$調撥$]", OrderDescribe, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$姓名$]", OrderName, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    System.Data.DataTable DataTableOrderItem = Tom.SQLGetData(Sql.TransferOrderDescribe, OrderNum);
                    int SumPage = (int)(Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(DataTableOrderItem.Rows.Count / 27))) + 1);
                    wordDocument.ReplaceText("[$總頁$]", SumPage.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    int OrderItemCount = 0;
                    int Page = 1;
                    int PageItemCount = 0;
                    foreach (DataRow Bodyitem in DataTableOrderItem.Rows)
                    {
                        bool flag9 = OrderItemCount == 27;
                        if (flag9)
                        {
                            wordDocument.ReplaceText("[$SumCount$]", HeaderItem[22].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$PageItemCount$]", PageItemCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            PageItemCount = 0;
                            string FileName = OrderNum + "_" + Page;
                            wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + ".docx");
                            OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + ".docx");
                            Page++;
                            OrderItemCount = 0;
                            wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report3.docx");
                            wordDocument.ReplaceText("[$單號$]", OrderNum, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$調出$]", OrderOutput, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$調入$]", OrderIntput, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$日期$]", OrderDate, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$備註$]", OrderMark, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$地址$]", OrderAddress, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$調撥$]", OrderDescribe, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$姓名$]", OrderName, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            wordDocument.ReplaceText("[$總頁$]", SumPage.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        }
                        OrderItemCount++;
                        string OrderCount = string.Format("{0:00}", OrderItemCount);
                        string ProductsNUM = "[$編號" + OrderCount + "$]";
                        string OrderPage = "[$當頁$]";
                        string ProductItemNUM = Bodyitem[5].ToString().PadRight(26, ' ');
                        string ProductItemNAM = Bodyitem[2].ToString().PadRight(20, ' ');
                        string ProductItemColor = Bodyitem[3].ToString().PadRight(18, ' ');
                        string ProductItemSize = Bodyitem[4].ToString().PadRight(17, ' ');
                        string ProductItemAmount = Bodyitem[6].ToString();
                        PageItemCount += int.Parse(ProductItemAmount);
                        string AllItem = string.Concat(new string[]
                        {
                            ProductItemNUM,
                            ProductItemNAM,
                            ProductItemColor,
                            ProductItemSize,
                            ProductItemAmount
                        });
                        wordDocument.ReplaceText(ProductsNUM, AllItem, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText(OrderPage, Page.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    }
                    bool flag10 = OrderItemCount != 27;
                    if (flag10)
                    {
                        int Last = 34 - OrderItemCount;
                        for (int i = 1; i <= Last; i++)
                        {
                            string OrderCount2 = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM2 = "[$編號" + OrderCount2 + "$]";
                            wordDocument.ReplaceText(ProductsNUM2, " ", false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            bool flag11 = OrderItemCount == 27;
                            if (flag11)
                            {
                                break;
                            }
                            OrderItemCount++;
                        }
                    }
                    wordDocument.ReplaceText("[$SumCount$]", HeaderItem[22].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$PageItemCount$]", PageItemCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                    string FileNameEnd = OrderNum + "_" + Page;
                    wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileNameEnd + ".docx");
                    OrderUrl.Add(ServerUrl + "\\tmp\\" + FileNameEnd + ".docx");
                }
                string FinalFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                MergeWord(OrderUrl.ToArray(), ServerUrl + "\\tmp\\" + FinalFileName + ".docx");

                this.Cursor = Cursors.Default;
                Process.Start(ServerUrl + "\\tmp\\" + FinalFileName + ".docx");

                dataGridView1.DataSource = PickDB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == Order).ToList();

            }
            else
            {
                this.Cursor = Cursors.Default;
            }


        }


        public static bool MergeWord(string[] InFilePath, string OutFilePath)
        {
            object missing = Missing.Value;
            object iLastWord = InFilePath.Last<string>();
            object oOutputDoc = OutFilePath;
            object oPageBreak = WdBreakType.wdColumnBreak;
            Microsoft.Office.Interop.Word.Application wordApp = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
            Document origDoc = wordApp.Documents.Open(ref iLastWord, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            origDoc.Activate();
            for (int i = 0; i < InFilePath.Count<string>(); i++)
            {
                bool flag = i == InFilePath.Count<string>() - 1 || InFilePath.Count<string>() == 0;
                if (flag)
                {
                    break;
                }
                wordApp.Selection.InsertFile(InFilePath[i], ref missing, ref missing, ref missing, ref missing);
            }
            wordApp.ActiveDocument.SaveAs(ref oOutputDoc, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            wordApp.ActiveDocument.Close(ref missing, ref missing, ref missing);
            wordApp.Quit(ref missing, ref missing, ref missing);
            return true;
        }

        public void DeleteTempFolder(string tempFolderPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(tempFolderPath);
            bool exists = dInfo.Exists;
            if (exists)
            {
                dInfo.Delete(true);
                dInfo.Create();
            }
            else
            {
                dInfo.Create();
            }
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Date = dateTimePicker1.Value.Date;
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

        private void buttonRSIC_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {

                    if (Item.Cells[4].Value == null)
                    {
                        int ID = int.Parse(Item.Cells["RA_ID"].Value.ToString());
                        string Staff = Item.Cells["RA_Staff"].Value.ToString(); //開單人
                        A4_TransformRSICA a4_TransformRSICA = new A4_TransformRSICA(ID, Staff,Order);
                        a4_TransformRSICA.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("此單據已轉單。");
                    }
                }
            }
        }
    }
}
