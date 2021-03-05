using Microsoft.Office.Interop.Word;
using Novacode;
using SalesInput.Librarys;
using SalesInput.SqlString;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput
{
    public partial class A1_Print_Form : Form
    {
        public A1_Print_Form()
        {
            InitializeComponent();

            this.dateTimePicker_Date1.CustomFormat = " ";
            this.dateTimePicker_Date1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker_Date2.CustomFormat = " ";
            this.dateTimePicker_Date2.Format = DateTimePickerFormat.Custom;

            this.dateTimePicker2.CustomFormat = " ";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = " ";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;


            dateTimePicker_IDate1.CustomFormat = " ";
            this.dateTimePicker_IDate1.Format = DateTimePickerFormat.Custom;
            dateTimePicker_IDate2.CustomFormat = " ";
            this.dateTimePicker_IDate2.Format = DateTimePickerFormat.Custom;

            this.dateTimePickerT1.CustomFormat = " ";
            this.dateTimePickerT1.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerT2.CustomFormat = " ";
            this.dateTimePickerT2.Format = DateTimePickerFormat.Custom;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            pictureBox1.Visible = true;
            button1.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.CloseProcess("WINWORD");
            try
            {
                this.DeleteTempFolder(ServerUrl + "\\tmp\\");
            }
            catch
            { }
            List<string> OrderUrl = new List<string>();
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            FL00SSEntities Erp = new FL00SSEntities();
            TomLibrary Tom = new TomLibrary();
            Tom.SQLConnectionString = Sql.SQLConnectionString;


            string SqlQueryString = Sql.SalesOrderHeader;
            if (textBox_Number1.Text == "" && dateTimePicker_Date1.Text == " " && textBox_Customer.Text == "" && textBox_Store.Text == "")
            {
                MessageBox.Show("條件都沒輸入，賣來亂!");
            }
            else
            {
                if (textBox_Number1.Text != "")
                {
                    string OrderNumber1 = textBox_Number1.Text;
                    string OrderNumber2 = textBox_Number1.Text;
                    if (textBox_Number2.Text != "")
                    {
                        OrderNumber2 = textBox_Number2.Text;
                    }
                    SqlQueryString += " and raren between ";
                    SqlQueryString += "'" + OrderNumber1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderNumber2 + "'";
                }
                if (dateTimePicker_Date1.Text != " ")
                {
                    string OrderDate1 = dateTimePicker_Date1.Text;
                    string OrderDate2 = dateTimePicker_Date1.Text;

                    if (dateTimePicker_Date2.Text != " ")
                    {
                        OrderDate2 = dateTimePicker_Date2.Text;
                    }

                    SqlQueryString += " and raday between ";
                    SqlQueryString += "'" + OrderDate1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderDate2 + "'";
                }

                if (textBox_Customer.Text != "")
                {
                    SqlQueryString += " and ranum ='" + textBox_Customer.Text + "' ";
                }
                if (textBox_Store.Text != "")
                {
                    SqlQueryString += " and RACNS ='" + textBox_Store.Text + "' ";

                }


                System.Data.DataTable SalesHeaderItem = Tom.SQLGetData(SqlQueryString); //DataSource
                if (SalesHeaderItem.Rows.Count != 0)
                {
                    dataGridView1.DataSource = SalesHeaderItem;
                    foreach (DataRow HeaderItem in SalesHeaderItem.Rows) //DataSource loop
                    {
                        System.Windows.Forms.Application.DoEvents();
                        DocX wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report.docx");//參考樣板
                                                                                           //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$銷貨單號$]", HeaderItem["單號"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$出貨櫃點$]", HeaderItem["櫃點"].ToString() + HeaderItem["櫃點名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$銷貨客戶$]", HeaderItem["客戶"].ToString() + HeaderItem["客戶名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$銷貨日期$]", HeaderItem["日期"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$抬頭備註$]", HeaderItem["備註"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$調入地址$]", HeaderItem["地址"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$出貨類別$]", HeaderItem["出貨類別"].ToString() + HeaderItem["類別名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$姓名$]", HeaderItem["員編"].ToString() + HeaderItem["姓名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);

                        //--------------------------表身-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        System.Data.DataTable SalesOrderItem = Tom.SQLGetData(Sql.SalesOrderDescribe, HeaderItem["單號"].ToString());//表身Source
                        int OrderItemCount = 0;//當頁Item
                        decimal PageSum = 0;//當頁加總
                        int OrderSum = (int)(Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(SalesOrderItem.Rows.Count / 27))) + 1); ;//單據加總
                        int PageCount = 1;//設定第頁數為:1
                        string FileName = HeaderItem["單號"].ToString() + "_";
                        foreach (DataRow OrderItem in SalesOrderItem.Rows)
                        {
                            if (OrderItemCount == 27)//品項=27時
                            {
                                wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$OrderSum$]", HeaderItem["合計金額"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$OrderQty$]", HeaderItem["合計數量"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderItemCount = 0;
                                PageSum = 0;
                                PageCount++;
                                wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report.docx");//參考樣板
                                                                                              //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                                wordDocument.ReplaceText("[$銷貨單號$]", HeaderItem["單號"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$出貨櫃點$]", HeaderItem["櫃點"].ToString() + HeaderItem["櫃點名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$銷貨客戶$]", HeaderItem["客戶"].ToString() + HeaderItem["客戶名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$銷貨日期$]", HeaderItem["日期"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$抬頭備註$]", HeaderItem["備註"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$調入地址$]", HeaderItem["地址"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$出貨類別$]", HeaderItem["出貨類別"].ToString() + HeaderItem["類別名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$姓名$]", HeaderItem["員編"].ToString() + HeaderItem["姓名"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            }
                            OrderItemCount++; //當頁Item +1
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            string OrderPage = "[$當頁$]";
                            string ProductItemNUM = OrderItem["條碼"].ToString().PadRight(14, ' ');
                            string ProductItemNAM = OrderItem["貨號"].ToString().PadRight(10, ' ');
                            string ProductItemColor = OrderItem["顏色"].ToString().PadRight(7, ' ');
                            string ProductItemSize = OrderItem["尺寸"].ToString().PadRight(7, ' ');
                            string ProductItemAmount = OrderItem["數量"].ToString().PadRight(7, ' ');
                            string ProductItemPrice = OrderItem["定價"].ToString().PadRight(10, ' ');
                            string ProductItemDiscCunt = OrderItem["折數"].ToString().PadRight(7, ' ');
                            string ProductItemSolidPrice = OrderItem["實售價"].ToString().PadRight(10, ' ');
                            decimal Price = decimal.Parse(ProductItemSolidPrice);
                            //int Price = 1;
                            int Amount = int.Parse(ProductItemAmount);
                            decimal SumPrice = Price * Amount;
                            PageSum += SumPrice;
                            //string.Format("{0:C0}", SumPrice.ToString())
                            string AllItem = string.Concat(new string[]
                                    {
                            ProductItemNUM,
                            ProductItemNAM,
                            ProductItemColor,
                            ProductItemSize,
                            ProductItemAmount,ProductItemPrice,
                            ProductItemDiscCunt,
                            ProductItemSolidPrice,  SumPrice.ToString("#,0")
                                    });
                            wordDocument.ReplaceText(ProductsNUM, AllItem, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);

                        }

                        //---------------當頁Item 不到27品項，帶空白----------------------------------------------------------------------------------------
                        while (OrderItemCount != 27)
                        {
                            OrderItemCount++;
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            wordDocument.ReplaceText(ProductsNUM, " ", false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        }
                        //-------------存檔--------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$OrderSum$]", HeaderItem["合計金額"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$OrderQty$]", HeaderItem["合計數量"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                        OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");




                    }
                    string FinalFileName = DateTime.Now.ToString("yyyyMMddHHmmss");//最後匯出檔
                    MergeWord(OrderUrl.ToArray(), ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                    MessageBox.Show(String.Format("共{0}頁 銷貨單。", OrderUrl.Count));
                    Process.Start(ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                }
                else
                {
                    MessageBox.Show("查無資料。");

                }
            }
            pictureBox1.Visible = false;
            button1.Enabled = true;
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
        private void dateTimePicker_Date1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_Date1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker_Date2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_Date2.CustomFormat = "yyyy-MM-dd";
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

        private void button_ClearN1_Click(object sender, EventArgs e)
        {
            textBox_Number1.Text = "";
        }

        private void button_ClearN2_Click(object sender, EventArgs e)
        {
            textBox_Number2.Text = "";
        }

        private void button_ClearD1_Click(object sender, EventArgs e)
        {
            this.dateTimePicker_Date1.CustomFormat = " ";
            this.dateTimePicker_Date1.Format = DateTimePickerFormat.Custom;
        }

        private void button_ClearD2_Click(object sender, EventArgs e)
        {
            this.dateTimePicker_Date2.CustomFormat = " ";
            this.dateTimePicker_Date2.Format = DateTimePickerFormat.Custom;
        }

        private void button_inventory_Click(object sender, EventArgs e)
        {
            string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            pictureBox2.Visible = true;  //顯示Loding..
            button_inventory.Enabled = false;     //鎖住button
            System.Windows.Forms.Application.DoEvents(); //多執行序
            this.CloseProcess("WINWORD"); //關閉 word
            try
            {
                this.DeleteTempFolder(ServerUrl + "\\tmp\\");  //清空tmp裏頭資料
            }
            catch
            { }
            List<string> OrderUrl = new List<string>();  //新增一個表單url陣列
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            FL00SSEntities Erp = new FL00SSEntities();
            TomLibrary Tom = new TomLibrary();
            Tom.SQLConnectionString = Sql.SQLConnectionString; //連接字串


            string SqlQueryString = Sql.InventoryOrderHeader; //設定 表頭sql語法
            if (textBox_IStore.Text == "" && dateTimePicker_IDate1.Text == " ") //完全沒輸入
            {
                MessageBox.Show("條件都沒輸入，賣來亂!");
            }
            else
            {
                if (textBox_IStore.Text != "")
                {
                    SqlQueryString += " and racns ='" + textBox_IStore.Text + "' ";
                }
                if (dateTimePicker_IDate1.Text != " ")
                {
                    string OrderDate1 = dateTimePicker_IDate1.Text;
                    string OrderDate2 = dateTimePicker_IDate1.Text;

                    if (dateTimePicker_IDate2.Text != " ")
                    {
                        OrderDate2 = dateTimePicker_IDate2.Text;
                    }

                    SqlQueryString += " and raday between ";
                    SqlQueryString += "'" + OrderDate1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderDate2 + "'";
                }
                if (textBox_ISN1.Text != "")
                {
                    string OrderSN1 = textBox_ISN1.Text;
                    string OrderSN2 = textBox_ISN1.Text;
                    if (textBox_ISN2.Text != "")
                    {
                        OrderSN2 = textBox_ISN2.Text;
                    }
                    SqlQueryString += " and raser between ";
                    SqlQueryString += "'" + OrderSN1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderSN2 + "'";
                }
                MessageBox.Show(SqlQueryString);

                System.Data.DataTable SalesHeaderItem = Tom.SQLGetData(SqlQueryString); //DataSource

                if (SalesHeaderItem.Rows.Count != 0)
                {

                    dataGridView2.DataSource = SalesHeaderItem;
                    foreach (DataRow HeaderItem in SalesHeaderItem.Rows) //DataSource loop
                    {
                        System.Windows.Forms.Application.DoEvents();
                        DocX wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report2.docx");//參考樣板
                        //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$盤點櫃點$]", HeaderItem["racns"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$盤點日期$]", HeaderItem["raday"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$序號$]", HeaderItem["RASER"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$姓名$]", HeaderItem["RAMEN"].ToString() + HeaderItem["BPNME"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$備註$]", HeaderItem["RARMK"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        //--------------------------表身-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        DateTime DateOrder = DateTime.Parse(HeaderItem["raday"].ToString());
                        System.Data.DataTable SalesOrderItem = Tom.SQLGetData(Sql.InventoryOrderDescribe, HeaderItem["racns"].ToString(), DateOrder.ToString("yyyy-MM-dd"), HeaderItem["RASER"].ToString());//表身Source
                        int OrderItemCount = 0;//當頁Item
                        int PageSum = 0;//當頁加總
                        int OrderSum = (int)(Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(SalesOrderItem.Rows.Count / 27))) + 1); ;//單據加總
                        int PageCount = 1;//設定第頁數為:1
                        string FileName = HeaderItem["RANUM"].ToString() + DateOrder.ToString("yyyyMMdd") + HeaderItem["RASER"].ToString() + "_";
                        foreach (DataRow OrderItem in SalesOrderItem.Rows)
                        {
                            if (OrderItemCount == 36)//品項=36時
                            {
                                wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);

                                wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderItemCount = 0;
                                PageSum = 0;
                                PageCount++;
                                wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report2.docx");//參考樣板
                                                                                               //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                wordDocument.ReplaceText("[$盤點櫃點$]", HeaderItem["racns"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$盤點日期$]", HeaderItem["raday"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$序號$]", HeaderItem["RASER"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$姓名$]", HeaderItem["RAMEN"].ToString() + HeaderItem["BPNME"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$備註$]", HeaderItem["RARMK"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            }
                            OrderItemCount++; //當頁Item +1
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            string OrderPage = "[$當頁$]";
                            string ProductItemNUM = OrderItem["條碼"].ToString().PadRight(14, ' ');
                            string ProductItemNAM = OrderItem["貨號"].ToString().PadRight(15, ' ');
                            string ProductItemColor = OrderItem["顏色"].ToString().PadRight(12, ' ');
                            string ProductItemSize = OrderItem["尺寸"].ToString().PadRight(10, ' ');
                            string ProductItemAmount = OrderItem["數量"].ToString().PadRight(8, ' ');

                            int Amount = int.Parse(ProductItemAmount);

                            PageSum += Amount;
                            string AllItem = string.Concat(new string[]
                                    {
                            ProductItemNUM,
                            ProductItemNAM,
                            ProductItemColor,
                            ProductItemSize,
                            ProductItemAmount


                                    });
                            wordDocument.ReplaceText(ProductsNUM, AllItem, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);

                        }

                        //---------------當頁Item 不到27品項，帶空白----------------------------------------------------------------------------------------
                        while (OrderItemCount != 36)
                        {
                            OrderItemCount++;
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            wordDocument.ReplaceText(ProductsNUM, " ", false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        }
                        //-------------存檔--------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                        OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");




                    }
                    string FinalFileName = DateTime.Now.ToString("yyyyMMddHHmmss");//最後匯出檔
                    MergeWord(OrderUrl.ToArray(), ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                    MessageBox.Show(String.Format("共{0}頁 銷貨單。", OrderUrl.Count));
                    Process.Start(ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                }
                else
                {
                    MessageBox.Show("查無資料。");

                }
            }
            pictureBox2.Visible = false;
            button_inventory.Enabled = true;
        }

        private void dateTimePicker_IDate1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_IDate1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker_IDate2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker_IDate2.CustomFormat = "yyyy-MM-dd";
        }

        private void button_ClearID1_Click(object sender, EventArgs e)
        {
            this.dateTimePicker_IDate1.CustomFormat = " ";
            this.dateTimePicker_IDate1.Format = DateTimePickerFormat.Custom;
        }

        private void button_ClearID2_Click(object sender, EventArgs e)
        {
            this.dateTimePicker_IDate2.CustomFormat = " ";
            this.dateTimePicker_IDate2.Format = DateTimePickerFormat.Custom;
        }

        private void button_ClearISN1_Click(object sender, EventArgs e)
        {
            textBox_ISN1.Text = "";
        }

        private void button_ClearISN2_Click(object sender, EventArgs e)
        {
            textBox_ISN2.Text = "";
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Visible = true;
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
            string SqlQueryString = "SELECT * FROM RSIMA  WHERE RAREN LIKE '%' ";
            bool flag = this.textBox_OrderNum1.Text != "";
            if (flag)
            {
                string OrderNum_ = this.textBox_OrderNum1.Text;
                string OrderNum_2 = this.textBox_OrderNum1.Text;
                bool flag2 = this.textBox_OrderNum2.Text != "";
                if (flag2)
                {
                    OrderNum_2 = this.textBox_OrderNum2.Text;
                }
                SqlQueryString = string.Concat(new string[]
                {
                    SqlQueryString,
                    "  AND RAREN BETWEEN '",
                    OrderNum_,
                    "' AND  '",
                    OrderNum_2,
                    "'"
                });
            }
            bool flag3 = this.dateTimePickerT1.Text != " ";
            if (flag3)
            {
                string OrderDate_ = this.dateTimePickerT1.Text;
                string OrderDate_2 = this.dateTimePickerT1.Text;
                bool flag4 = this.dateTimePickerT2.Text != " ";
                if (flag4)
                {
                    OrderDate_2 = this.dateTimePickerT2.Text;
                }
                SqlQueryString = string.Concat(new string[]
                {
                    SqlQueryString,
                    "  AND RADAY BETWEEN '",
                    OrderDate_,
                    "' AND  '",
                    OrderDate_2,
                    "'"
                });
            }
            bool flag5 = this.textBox_OpenOrder.Text != "";
            if (flag5)
            {
                SqlQueryString = SqlQueryString + "  AND RAMEN = '" + this.textBox_OpenOrder.Text + "'";
            }
            bool flag6 = this.textBox_OutPut.Text != "";
            if (flag6)
            {
                SqlQueryString = SqlQueryString + "  AND RACNS = '" + this.textBox_OutPut.Text + "'";
            }
            bool flag7 = this.textBox_InPut.Text != "";
            if (flag7)
            {
                SqlQueryString = SqlQueryString + "  AND RACN2 = '" + this.textBox_InPut.Text + "'";
            }
            System.Data.DataTable DataTableOrderHeader = Tom.SQLGetData(SqlQueryString);
            bool flag8 = DataTableOrderHeader.Rows.Count != 0;
            if (flag8)
            {
                this.dataGridView3.DataSource = DataTableOrderHeader;
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
                MessageBox.Show(string.Format("共{0}筆。", this.dataGridView3.RowCount.ToString()));
                this.Cursor = Cursors.Default;
                Process.Start(ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                this.pictureBox1.Visible = false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("無資料");
                this.pictureBox1.Visible = false;
            }
        }

        private void dateTimePickerT1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerT1.CustomFormat = "yyyy-MM-dd";

        }

        private void dateTimePickerT2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerT2.CustomFormat = "yyyy-MM-dd";
        }

        private void button_ImportExcel_Click(object sender, EventArgs e)
        {
            string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            pictureBox2.Visible = true;  //顯示Loding..
            button_inventory.Enabled = false;     //鎖住button
            System.Windows.Forms.Application.DoEvents(); //多執行序
            this.CloseProcess("WINWORD"); //關閉 word
            try
            {
                this.DeleteTempFolder(ServerUrl + "\\tmp\\");  //清空tmp裏頭資料
            }
            catch
            { }
            List<string> OrderUrl = new List<string>();  //新增一個表單url陣列
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            FL00SSEntities Erp = new FL00SSEntities();
            TomLibrary Tom = new TomLibrary();
            Tom.SQLConnectionString = Sql.SQLConnectionString; //連接字串


            string SqlQueryString = Sql.InventoryOrderHeader; //設定 表頭sql語法
            if (textBox_IStore.Text == "" && dateTimePicker_IDate1.Text == " ") //完全沒輸入
            {
                MessageBox.Show("條件都沒輸入，賣來亂!");
            }
            else
            {
                if (textBox_IStore.Text != "")
                {
                    SqlQueryString += " and racns ='" + textBox_IStore.Text + "' ";
                }
                if (dateTimePicker_IDate1.Text != " ")
                {
                    string OrderDate1 = dateTimePicker_IDate1.Text;
                    string OrderDate2 = dateTimePicker_IDate1.Text;

                    if (dateTimePicker_IDate2.Text != " ")
                    {
                        OrderDate2 = dateTimePicker_IDate2.Text;
                    }

                    SqlQueryString += " and raday between ";
                    SqlQueryString += "'" + OrderDate1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderDate2 + "'";
                }
                if (textBox_ISN1.Text != "")
                {
                    string OrderSN1 = textBox_ISN1.Text;
                    string OrderSN2 = textBox_ISN1.Text;
                    if (textBox_ISN2.Text != "")
                    {
                        OrderSN2 = textBox_ISN2.Text;
                    }
                    SqlQueryString += " and raser between ";
                    SqlQueryString += "'" + OrderSN1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderSN2 + "'";
                }
                MessageBox.Show(SqlQueryString);

                System.Data.DataTable SalesHeaderItem = Tom.SQLGetData(SqlQueryString); //DataSource

                if (SalesHeaderItem.Rows.Count != 0)
                {
                    TomLibrary tomLibrary = new TomLibrary();
                    foreach (DataRow HeaderItem in SalesHeaderItem.Rows) //DataSource loop

                    {
                        DateTime DateOrder = DateTime.Parse(HeaderItem["raday"].ToString());
                        System.Data.DataTable SalesOrderItem = Tom.SQLGetData(Sql.InventoryOrderDescribe, HeaderItem["racns"].ToString(), DateOrder.ToString("yyyy-MM-dd"), HeaderItem["RASER"].ToString());//表身Source
                        tomLibrary.DataTableToExcelFile(SalesOrderItem, "C:\\", HeaderItem["racns"].ToString()+ DateOrder.ToString("yyyyMMdd") + HeaderItem["raser"].ToString()+"xls");
                    }
            
                 

                }

                pictureBox2.Visible = false;
                button_inventory.Enabled = true;
            }
        }

        private void button_Input_Click(object sender, EventArgs e)
        {
            string ServerUrl = Environment.CurrentDirectory; //帶出軟體所在目錄
            pictureBox1.Visible = true;
            button1.Enabled = false;
            System.Windows.Forms.Application.DoEvents();
            this.CloseProcess("WINWORD");
            try
            {
                this.DeleteTempFolder(ServerUrl + "\\tmp\\");
            }
            catch
            { }
            List<string> OrderUrl = new List<string>();
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            FL00SSEntities Erp = new FL00SSEntities();
            TomLibrary Tom = new TomLibrary();
            Tom.SQLConnectionString = Sql.SQLConnectionString;


            string SqlQueryString = Sql.InputOrderHeader;
            if (textBoxInputID1.Text == "" && dateTimePicker1.Text == " " && textBoxInputCustomer.Text == "" )
            {
                MessageBox.Show("條件都沒輸入，賣來亂!");
            }
            else
            {
                if (textBoxInputID1.Text != "")
                {
                    string OrderNumber1 = textBoxInputID1.Text;
                    string OrderNumber2 = textBoxInputID1.Text;
                    if (textBoxInputID2.Text != "")
                    {
                        OrderNumber2 = textBoxInputID2.Text;
                    }
                    SqlQueryString += " and raren between ";
                    SqlQueryString += "'" + OrderNumber1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderNumber2 + "'";
                }
                if (dateTimePicker1.Text != " ")
                {
                    string OrderDate1 = dateTimePicker1.Text;
                    string OrderDate2 = dateTimePicker1.Text;

                    if (dateTimePicker2.Text != " ")
                    {
                        OrderDate2 = dateTimePicker2.Text;
                    }

                    SqlQueryString += " and raday between ";
                    SqlQueryString += "'" + OrderDate1 + "'";
                    SqlQueryString += " and ";
                    SqlQueryString += "'" + OrderDate2 + "'";
                }

                if (textBoxInputCustomer.Text != "")
                {
                    SqlQueryString += " and ranum ='" + textBoxInputCustomer.Text + "' ";
                }
              

                System.Data.DataTable SalesHeaderItem = Tom.SQLGetData(SqlQueryString); //DataSource
                if (SalesHeaderItem.Rows.Count != 0)
                {
                    dataGridView1.DataSource = SalesHeaderItem;
                    foreach (DataRow HeaderItem in SalesHeaderItem.Rows) //DataSource loop
                    {
                        System.Windows.Forms.Application.DoEvents();
                        DocX wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report4.docx");//參考樣板
                                                                                           //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$單號$]", HeaderItem["單號"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$櫃點$]", HeaderItem["入庫櫃點"].ToString()+ HeaderItem["櫃點名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$客戶$]", HeaderItem["客戶編號"].ToString()+ HeaderItem["客戶名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$開單人員$]", HeaderItem["人員編號"].ToString() + HeaderItem["開單人員"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$日期$]", DateTime.Parse(HeaderItem["日期"].ToString()).ToShortDateString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$抬頭備註$]", HeaderItem["備註"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$計稅方式$]", HeaderItem["計稅方式"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$稅率類別$]", HeaderItem["稅率類別"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        //wordDocument.ReplaceText("[$調入地址$]", HeaderItem["地址"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);


                        //--------------------------表身-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        System.Data.DataTable SalesOrderItem = Tom.SQLGetData(Sql.InputOrderDescribe, HeaderItem["單號"].ToString());//表身Source
                        int OrderItemCount = 0;//當頁Item
                        int PageSum = 0;//當頁加總
                        int OrderSum = (int)(Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(SalesOrderItem.Rows.Count / 27))) + 1); ;//單據加總
                        int PageCount = 1;//設定第頁數為:1
                        string FileName = HeaderItem["單號"].ToString() + "_";
                        foreach (DataRow OrderItem in SalesOrderItem.Rows)
                        {
                            if (OrderItemCount == 27)//品項=27時
                            {
                                wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$OrderSum$]", HeaderItem["合計金額"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$OrderQty$]", HeaderItem["數量"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                                OrderItemCount = 0;
                                PageSum = 0;
                                PageCount++;
                                wordDocument = DocX.Load(ServerUrl + "\\Sample\\Report4.docx");//參考樣板
                                                                                               //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                                //----------------------------表頭-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                wordDocument.ReplaceText("[$單號$]", HeaderItem["單號"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$櫃點$]", HeaderItem["入庫櫃點"].ToString() + HeaderItem["櫃點名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$客戶$]", HeaderItem["客戶編號"].ToString() + HeaderItem["客戶名稱"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$日期$]", HeaderItem["日期"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$抬頭備註$]", HeaderItem["備註"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$計稅方式$]", HeaderItem["計稅方式"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                                wordDocument.ReplaceText("[$稅率類別$]", HeaderItem["稅率類別"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                            }
                            OrderItemCount++; //當頁Item +1
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            string OrderPage = "[$當頁$]";
                            string ProductItemNUM = OrderItem["條碼"].ToString().PadRight(14, ' ');
                            string ProductItemNAM = OrderItem["貨號"].ToString().PadRight(17, ' ');
                            string ProductItemColor = OrderItem["顏色"].ToString().PadRight(7, ' ');
                            string ProductItemSize = OrderItem["尺寸"].ToString().PadRight(7, ' ');
                            string ProductItemAmount = OrderItem["數量"].ToString().PadRight(7, ' ');
                          // string ProductItemPrice = "6";//OrderItem["定價"].ToString().PadRight(10, ' ');
                           //string ProductItemDiscCunt = "6";// OrderItem["折數"].ToString().PadRight(7, ' ');
                            //string ProductItemSolidPrice = "6";// OrderItem["實售價"].ToString().PadRight(10, ' ');
                           //int Price = (int)decimal.Parse(ProductItemSolidPrice);
                            int Amount = int.Parse(ProductItemAmount);
                          //  int SumPrice = Price * Amount;
                           // PageSum += SumPrice;
                            string AllItem = string.Concat(new string[]
                                    {
                            ProductItemNUM,
                            ProductItemNAM,
                            ProductItemColor,
                            ProductItemSize,
                            ProductItemAmount
                                    });
                            wordDocument.ReplaceText(ProductsNUM, AllItem, false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);

                        }

                        //---------------當頁Item 不到27品項，帶空白----------------------------------------------------------------------------------------
                        while (OrderItemCount != 27)
                        {
                            OrderItemCount++;
                            string OrderCount = string.Format("{0:00}", OrderItemCount);
                            string ProductsNUM = "[$編號" + OrderCount + "$]";
                            wordDocument.ReplaceText(ProductsNUM, " ", false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        }
                        //-------------存檔--------------------------------------------------------------------------------------------------------------------
                        wordDocument.ReplaceText("[$總頁$]", OrderSum.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$當頁$]", PageCount.ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$OrderSum$]", HeaderItem["合計金額"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$OrderQty$]", HeaderItem["數量"].ToString(), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$PageSum$]", string.Format("{0:N0}", PageSum), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.ReplaceText("[$DateTimeNOW$]", DateTime.Now.ToString("yyyy/MM/dd hh:mm"), false, RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch, true, false);
                        wordDocument.SaveAs(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");
                        OrderUrl.Add(ServerUrl + "\\tmp\\" + FileName + PageCount + ".docx");




                    }
                    string FinalFileName = DateTime.Now.ToString("yyyyMMddHHmmss");//最後匯出檔
                    MergeWord(OrderUrl.ToArray(), ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                    MessageBox.Show(String.Format("共{0}頁 銷貨單。", OrderUrl.Count));
                    Process.Start(ServerUrl + "\\tmp\\" + FinalFileName + ".docx");
                }
                else
                {
                    MessageBox.Show("查無資料。");

                }
            }
            pictureBox1.Visible = false;
            button1.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
        }
    }
}
