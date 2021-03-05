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
    public partial class A4_TransformRSICA : Form
    {

        string staff;
        int id;
        string order;

        public A4_TransformRSICA(int _id ,string _staff,string _order)
        {
            InitializeComponent();

            textBoxPurchase.Text = DateTime.Now.ToString("yyyyMM");
            textBoxStore.Text = "3998";
            id = _id;
            staff = _staff;
            order = _order;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (textBoxCustomer.Text != "")
            {
                FL00SSEntities erp = new FL00SSEntities();
                PickupDBEntities1 PickDB = new PickupDBEntities1();
                RSICA rsicaItem = new RSICA();
                DateTime date = dateTimePicker.Value;               //銷貨日期
                                                                    //銷貨單頭
                string racns = textBoxStore.Text;            //出貨櫃點
                string ranum = textBoxCustomer.Text;         //銷貨客戶
                string racls = "3";                          //進銷存模式
                string racts = "D";                          //價格類別
                DateTime raday = date;                      //銷貨日期
                string rarma = textBoxMark.Text;            //抬頭備註
                string ramen = staff;                     //開單人員
                string rasal = textBoxSales.Text;            //業務員
                string raedt = textBoxPurchase.Text;                          //帳款年月
                string racs1 = "2";                          //計稅方式
                string racs2 = "0";                          //稅率類別
                int ratax = 0;                              //稅額
                int ratol = 0;                              //合計金額
                int raqty = 0;                              //銷貨數量(合計數量)
                string raadr = textBoxAddress.Text;         //地址
                string trmod = "";                          //異動型態
                string rasn2 = "";                          //表尾簽核
                string racs3 = "";                          //憑證類別
                DateTime raxdt = DateTime.Now;              //開單時間
                string Temp_Date_SN = date.ToString("yyMM");//日期-單號用                                   
                string Temp_SN = "C" + Temp_Date_SN + ramen;//M+日期+開單人
                string ERP_OrderSN = "";                    //銷貨單號
                if (erp.RSICAs.Where(s => s.RAREN.StartsWith(Temp_SN)).Any())
                {
                    ERP_OrderSN = erp.RSICAs.Where(s => s.RAREN.StartsWith(Temp_SN)).OrderByDescending(s => s.RAREN).First().RAREN;
                    string sort = ERP_OrderSN.Substring(10, 4);
                    int Sort = int.Parse(sort) + 1;
                    sort = string.Format("{0:0000}", Sort);
                    ERP_OrderSN = Temp_SN + sort;
                }
                else
                {
                    ERP_OrderSN = Temp_SN + "0001";
                }

                //------------------銷貨單頭 RSICA----------------------------
                rsicaItem.RAREN = ERP_OrderSN;  //銷貨單號
                rsicaItem.RACNS = racns;        //出貨櫃點
                rsicaItem.RANUM = ranum;        //銷貨客戶
                rsicaItem.RACLS = racls;        //進銷存模式
                rsicaItem.RACTS = racts;        //價格類別
                rsicaItem.RADAY = raday;        //銷貨日期
                rsicaItem.RARMA = rarma;        //抬頭備註
                rsicaItem.RAMEN = ramen;        //開單人員
                rsicaItem.RASAL = rasal;        //業務員
                rsicaItem.RAEDT = raedt;        //帳款年月
                rsicaItem.RACS1 = racs1;        //計稅方式
                rsicaItem.RACS2 = racs2;        //稅率類別
                rsicaItem.RATAX = ratax;        //稅額
                rsicaItem.RATOL = ratol;        //合計金額
                rsicaItem.RAQTY = raqty;        //銷貨數量(合計數量)
                rsicaItem.RAADR = raadr;        //地址
                rsicaItem.TRMOD = trmod;        //異動型態
                rsicaItem.RASN2 = rasn2;        //表尾簽核
                rsicaItem.RACS3 = racs3;        //憑證類別
                rsicaItem.RAXDT = raxdt;        //開單時間

                erp.RSICAs.Add(rsicaItem);

                int OrderID = id;

                //------------------銷貨單頭 RSICA----------------------------
                List<ReadyOrderB> ReadyB = PickDB.ReadyOrderBs.Where(s => s.RB_Maping_RA_ID == OrderID).ToList();
                List<string> TempProduct = new List<string>();
                List<RSICB> TempRsicb = new List<RSICB>();
                int itemSN = 0;                                 //序號
                decimal sumPrice = 0;                           //合計金額
                int amount = 0;                                 //數量
                foreach (var item in ReadyB)
                {
                    SalesInputSQLstring Sql = new SalesInputSQLstring();
                    TomLibrary Tom = new TomLibrary();
                    Tom.SQLConnectionString = Sql.SQLConnectionString;
                    string SqlQueryString = Sql.SQL_ProductDetail;
                    System.Data.DataTable SQL_Data = Tom.SQLGetData(SqlQueryString, item.RB_Barcode); //取尺寸類別欄位

                    string Name = SQL_Data.Rows[0][0].ToString();
                    string Color = SQL_Data.Rows[0][1].ToString();
                    string NameColor = Name + Color;
                    if (!TempProduct.Where(s => s == NameColor).Any())
                    {
                        TempProduct.Add(NameColor);
                        itemSN++;
                        String itemSN0 = string.Format("{0:0000}", itemSN);
                        RSICB B = new RSICB();
                        B.RBREN = ERP_OrderSN;//單號
                        B.RBITM = itemSN0;//項次
                        B.RBCNS = racns;//分倉編號
                        B.RBCN2 = null;//對應分倉
                        B.RBCLS = "3";//進銷存類別
                        B.RBCOS = null;//類別
                        B.RBNCR = SQL_Data.Rows[0][0].ToString();//型號
                        B.RBCLR = SQL_Data.Rows[0][1].ToString();//顏色
                        B.RBDC1 =  -1;//數量加減
                        B.RBQTY = int.Parse(item.RB_Amount.Value.ToString());//數量(調撥量)


                        decimal price = erp.BSTLs.Where(s => s.BSNCR == Name && s.BSCLR == Color).First().BSEEP.Value;
                        B.RBDPC = price;//定價
                        B.RBUP1 = price;//實售價
                        B.RBUPC = Math.Round(price / (decimal)1.05, 0);//未稅單價
                        B.RBDCX = 100;//折扣
                        B.RBAM1 = item.RB_Amount * price;//金額
                        B.RBAMT = item.RB_Amount * Math.Round(price / (decimal)1.05, 0);//金額
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

                        TempRsicb.Add(B);
                    }
                    else
                    {
                        //若型號顏色重覆
                        RSICB B = TempRsicb.Where(s => s.RBREN == ERP_OrderSN && s.RBNCR == Name && s.RBCLR == Color).First();
              
                        B.RBQTY += int.Parse(item.RB_Amount.ToString());//單品加總量
                        
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
                    RSICB bItem = TempRsicb.Where(s => s.RBREN == ERP_OrderSN && s.RBNCR == Name && s.RBCLR == Color).First();
                    decimal bPrice = erp.BSTLs.Where(s => s.BSNCR == Name && s.BSCLR == Color).First().BSEEP.Value;
                    bItem.RBAM1 = bItem.RBQTY * bPrice;//金額
                    bItem.RBAMT = bItem.RBQTY * Math.Round(bPrice / (decimal)1.05, 0);//金額
                }
                sumPrice = TempRsicb.Where(s => s.RBREN == ERP_OrderSN).Sum(s => s.RBAM1).Value;//金額加總
                amount = TempRsicb.Where(s => s.RBREN == ERP_OrderSN).Sum(s => s.RBQTY).Value;//數量加總
                rsicaItem.RATAX = sumPrice - Math.Round(sumPrice / (decimal)1.05);  //稅額
                rsicaItem.RATOL = sumPrice;      //合計金額
                rsicaItem.RAQTY = amount;        //銷貨數量(合計數量)
                ReadyOrderA readyOrderA = PickDB.ReadyOrderAs.Find(id);
                readyOrderA.RA_Maping_ERP_ID = ERP_OrderSN;
                PickDB.SaveChanges();
                erp.RSICBs.AddRange(TempRsicb);
                erp.SaveChanges();
                MessageBox.Show(ERP_OrderSN);
                A1_Transform_Menu transform_Menu = new A1_Transform_Menu(order);
                transform_Menu.Show();
            }
            else
            {
                MessageBox.Show("請輸入銷貨客戶!!");
            }
        }

        private void textBoxCustomer_KeyDown(object sender, KeyEventArgs e)
        {
           


        }

        private void textBoxCustomer_KeyUp(object sender, KeyEventArgs e)
        {
            string customer = textBoxCustomer.Text;

            FL00SSEntities erp = new FL00SSEntities();

            if (erp.BCUS.Where(s => s.BCNUM == customer).Any())
            {
                textBoxSales.Text = erp.BCUS.Where(s => s.BCNUM == customer).First().BCSAL;
                textBoxAddress.Text = erp.BCUS.Where(s => s.BCNUM == customer).First().BCADR;


            }
            else
            {
                textBoxSales.Text = "";
                textBoxAddress.Text = "";
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
