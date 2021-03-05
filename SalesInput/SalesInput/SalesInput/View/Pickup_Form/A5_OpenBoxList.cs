using SalesInput.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesInput.Librarys;
using SalesInput.SqlString;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using SalesInput.ObjectFile;

namespace SalesInput.View.Pickup_Form
{
    public partial class A5_OpenBoxList : Form
    {
    
        OrderInfo TempOrderInfo = new OrderInfo();

        public A5_OpenBoxList(OrderInfo orderInfo)
        {
            InitializeComponent();
            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            TempOrderInfo = orderInfo;
            List<ReadyOrderA> readyOrderA = pickupDBEntities1.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == TempOrderInfo.Order).ToList();
            textBoxStore.Text = "3997";
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            if (readyOrderA != null)
            {
                dataGridView1.DataSource = readyOrderA;
            }
                
        }

        private void btnPrint_Click(object sender, EventArgs e) //列印封箱明細
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                bool select = item.Selected;
                if (select)
                {
                    int readyAID = int.Parse(item.Cells[0].Value.ToString());
                    printMethod(readyAID.ToString());

                }
            }
               
        }

        private static void printMethod(string ID)
        {
            PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
            int id = int.Parse(ID);
            ReadyOrderA readyOrderA = pickupDBEntities1.ReadyOrderAs.Find(id);
            long readyAID = readyOrderA.RA_ID;
            string orderID = readyOrderA.RA_Maping_PU_ID;//單號
            string boxID = readyOrderA.RA_BoxID.Value.ToString();//箱號
            string store = readyOrderA.RA_Store;//門市
            List<ReadyOrderB> orderB = pickupDBEntities1.ReadyOrderBs.Where(s => s.RB_Maping_RA_ID == readyAID).ToList();
            long pageCount = 0;//頁小計
            long pageSum = orderB.Sum(s => s.RB_Amount).Value;//總合計
            int fontHigh = 200;
            if (orderB != null)
            {
                TSCLIB_DLL.openport("TSC TTP-244CE");                                           //Open specified printer driver
                TSCLIB_DLL.setup("100", "100", "4", "10", "0", "3", "0");                           //Setup the media size and sensor type info
                TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
                TSCLIB_DLL.barcode("100", "50", "128", "50", "1", "0", "2", "2", readyOrderA.RA_Maping_ERP_ID!=null? readyOrderA.RA_Maping_ERP_ID:""); //列印條碼
                TSCLIB_DLL.windowsfont(100, 150, 36, 0, 0, 0, "ARIAL", "型號                 顏色      尺寸       數量 ||櫃點:" + store);  //Draw windows font
                TSCLIB_DLL.windowsfont(100, 170, 36, 0, 0, 0, "ARIAL", "--------------------------------------------------");  //Draw windows font
            
            }
            int count = 0;
            foreach (var orderBItem in orderB)
            {
                count++;
                if (fontHigh > 618)
                {
                    fontHigh = 200;
                    TSCLIB_DLL.openport("TSC TTP-244CE");                                           //Open specified printer driver
                    TSCLIB_DLL.setup("100", "100", "4", "10", "0", "3", "0");                           //Setup the media size and sensor type info
                    TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
                    TSCLIB_DLL.barcode("100", "50", "128", "50", "1", "0", "2", "2", readyOrderA.RA_Maping_ERP_ID != null ? readyOrderA.RA_Maping_ERP_ID : ""); //列印條碼
                    TSCLIB_DLL.windowsfont(100, 150, 36, 0, 0, 0, "ARIAL", "型號                 顏色      尺寸       數量 ||櫃點:" + store);  //Draw windows font
                    TSCLIB_DLL.windowsfont(100, 170, 36, 0, 0, 0, "ARIAL", "--------------------------------------------------");  //Draw windows font
                }
                TSCLIB_DLL.windowsfont(100, fontHigh, 36, 0, 0, 0, "細明體", String.Format("{0}    {1}    {2}    {3}  ", orderBItem.RB_Type.PadRight(9, ' '), orderBItem.RB_Color.PadRight(3, ' '), orderBItem.RB_Size.PadRight(3, ' '), orderBItem.RB_Amount.Value.ToString().PadRight(3, ' ')));  //Draw windows font
                pageCount += orderBItem.RB_Amount.Value; //頁小計小計累加
                if (fontHigh == 618)
                {
                    TSCLIB_DLL.windowsfont(100, 700, 36, 0, 0, 0, "ARIAL", "--------------------------------------------------");  //合計底線
                    TSCLIB_DLL.windowsfont(100, 750, 36, 0, 0, 0, "ARIAL", string.Format("                      頁小計:{0}      總和計:{1}", pageCount, pageSum));  //Draw windows font
                    TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
                    TSCLIB_DLL.closeport();                                                             //Close specified printer driver 
                    pageCount = 0;//頁小計歸0
                }
                fontHigh += 38;
            }
            if (orderB != null&&fontHigh < 656)
            {
                TSCLIB_DLL.windowsfont(100, 700, 36, 0, 0, 0, "ARIAL", "--------------------------------------------------");  //合計底線
                TSCLIB_DLL.windowsfont(100, 750, 36, 0, 0, 0, "ARIAL", string.Format("                      頁小計:{0}      總和計:{1}", pageCount, pageSum));  //Draw windows font
                TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
                TSCLIB_DLL.closeport();                                                             //Close specified printer driver 
                pageCount = 0;//頁小計歸0
            }          
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                bool select = item.Selected;
                if (select)
                {
                    int readyAID = int.Parse(item.Cells[0].Value.ToString());
                    shipMethod(readyAID.ToString());

                }
            }
        }

        private void shipMethod(string ID) //缺收件人
        {

            try
            {
                int readyAID = int.Parse(ID);
                PickupDBEntities1 pickupDB = new PickupDBEntities1();
                ReadyOrderA orderA = pickupDB.ReadyOrderAs.Find(readyAID);
                //if (orderA.RA_Img == null)
                //{
                    string payMent = "21";

                    if (comboBoxPayment.Text == "元付")
                    {
                        payMent = "11";
                    }
                    DateTime Date = DateTime.Now;
                    ///------------------貨運序號-----------------------------
                    string shipDate = Date.ToString("yyMMdd");
                    string shipID;
                    TomLibrary library = new TomLibrary(); //FILA資料表使用
                    TomLibrary libraryERP = new TomLibrary(); //ERP資料表使用
                    SalesInputSQLstring salesInputSql = new SalesInputSQLstring();
                    library.SQLConnectionString = salesInputSql.FILASQLConnectionString; //FILA 資料表連接字串
                    libraryERP.SQLConnectionString = salesInputSql.SQLConnectionString; //ERP資料表連接字串

                    //DataTable oreder_DataTable = library.SQLGetData("select * from [FILA].[dbo].[POS_ShipOrdertest] where [ShipID] like @A order by shipID DESC ", shipDate + "%");
                    //測試資料庫
                    DataTable oreder_DataTable = library.SQLGetData("select * from [FILA].[dbo].[POS_ShipOrder] where [ShipID] like @A order by shipID DESC ", shipDate + "%");
                    //正式資料庫
                    DataTable store_InfoTable = libraryERP.SQLGetData(salesInputSql.StoreContact + "where BCNUM='" + orderA.RA_Store + "'");
                    if (oreder_DataTable.Rows.Count == 0)
                    {
                        shipID = shipDate + "0001";
                    }
                    else
                    {
                        int tempShipID = int.Parse(oreder_DataTable.Rows[0]["ShipID"].ToString());
                        tempShipID++;
                        shipID = tempShipID.ToString();
                    }
                    ///-----------------轉託運單-----------------------------
                    string receiverName = store_InfoTable.Rows[0]["櫃點"].ToString() + store_InfoTable.Rows[0]["分倉名稱"].ToString();//收件人
                    string receiverTel = store_InfoTable.Rows[0]["電話"].ToString().Length != 0 ? store_InfoTable.Rows[0]["電話"].ToString() : "04-23693558";// 收件人電話
                    string receiverAdd = store_InfoTable.Rows[0]["地址"].ToString();// 收件人地址
                  
                    string reciverMark = orderA.RA_Maping_PU_ID + "-" + orderA.RA_BoxID + "、" + TempOrderInfo.OrderNote;//備註欄
                    if (orderA.RA_Maping_ERP_ID != null)
                        {
                             reciverMark = orderA.RA_Maping_ERP_ID; //若erp調撥單號不為空值，則顯示調撥單號
                        }

                    string hctID = "04833400005";//hct帳號-測試
                    string hctPassword = "0423693558";// hct密碼-測試
                                                      //string hctID = "test";//hct帳號-測試
                                                      //string hctPassword = "test";// hct密碼-測試
                    HCTWebService.Service1SoapClient service = new HCTWebService.Service1SoapClient();
                    htcJson htcJson = new htcJson();
                    htcJson.epino = shipID;
                    htcJson.ercsig = receiverName;
                    htcJson.ertel1 = receiverTel;
                    htcJson.eraddr = receiverAdd;
                    htcJson.ejamt = "1";
                    htcJson.eqamt = "1";
                    //htcJson.eddate = TempOrderInfo.OrderShipDate;
                    htcJson.eddate = dateTimePicker1.Value.ToString("yyyyMMdd"); //指配日期
                    htcJson.eprdct = payMent;                                  //元到付
                    //if (TempOrderInfo.OrderShipDate != " ")       //若指配日不等於空值 則出貨日減一天
                    //{
                    //    htcJson.esdate = DateTime.Parse(TempOrderInfo.OrderShipDate).AddDays(-1).ToString("yyyyMMdd");
                    //}
                    htcJson.EMARK = reciverMark;
                    List<htcJson> htcList = new List<htcJson>();
                    htcList.Add(htcJson);
                    string json = JsonConvert.SerializeObject(htcList);
                    string response = service.TransData_Json(hctID, hctPassword, json);
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(response);
                    foreach (DataRow dataTableItem in dataTable.Rows)
                    {
                        string ServerUrl = Environment.CurrentDirectory;
                        orderA.RA_Img = dataTableItem["image"].ToString(); //存到ReadyA
                                                                           //library.SQLUpdateData(salesInputSql.ShipInsertTest, DateTime.Now.ToString("yyyy-MM-dd"), shipID, dataTableItem["edelno"].ToString(), shipID, shipID.ToString(), receiverName, receiverTel, receiverAdd, dataTableItem["image"].ToString(), dataTableItem["success"].ToString(), dataTableItem["erstno"].ToString(), reciverMark, "");
                                                                           //測試區資料庫
                        library.SQLUpdateData(salesInputSql.ShipInsert,dateTimePicker1.Value.AddDays(-1).ToString("yyyy-MM-dd"), shipID, dataTableItem["edelno"].ToString(), shipID, shipID.ToString(), receiverName, receiverTel, receiverAdd, dataTableItem["image"].ToString(), dataTableItem["success"].ToString(), dataTableItem["erstno"].ToString(), reciverMark, "");
                        //正是資料庫
                    }
                    //轉單完成。
                    string imgString = orderA.RA_Img;
                    string ServerUrls = Environment.CurrentDirectory;
                    image(GetBytes(imgString)).Save(ServerUrls + "\\tmp\\temp.jpg", ImageFormat.Jpeg);
                    PrintDocument PD = new PrintDocument();
                    PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                    PrintPreviewDialog PPD = new PrintPreviewDialog();
                    PPD.Document = PD;
                    PPD.ShowDialog();
                    pickupDB.SaveChanges();
            //}
            //else
            //{
            //    string imgString = orderA.RA_Img;
            //    string ServerUrls = Environment.CurrentDirectory;
            //    image(GetBytes(imgString)).Save(ServerUrls + "\\tmp\\temp.jpg", ImageFormat.Jpeg);
            //    PrintDocument PD = new PrintDocument();
            //    PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
            //    PrintPreviewDialog PPD = new PrintPreviewDialog();
            //    PPD.Document = PD;
            //    PPD.ShowDialog();
            //}
            }
            catch
            {
                MessageBox.Show("請到網路訊號通暢的地方，在試一次。");
            }
        }

        Image image(byte[] b)
        {
            MemoryStream ms = new MemoryStream(b);
            Image newImage = Image.FromStream(ms);
            return newImage;
        }
        byte[] GetBytes(string HexString)
        {
            int byteLength = HexString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { HexString[j], HexString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }
        byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }
        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {

            Point location = new Point(0, 0);
            string ServerUrl = Environment.CurrentDirectory;
            Image img = Image.FromFile(ServerUrl + "\\tmp\\temp.jpg");
            e.Graphics.DrawImage(img, new Rectangle(0, 0, 400, 400));

        }

        private void btnErp_Click(object sender, EventArgs e)
        {
            DateTime Date = dateTimePicker2.Value;
            string warehouse = textBoxStore.Text;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {

                bool select = Item.Selected;
                if (select)
                {
                    {
                        int readyAID = int.Parse(Item.Cells[0].Value.ToString());
                        turnErpMethod( Date, warehouse, readyAID.ToString());
                        MessageBox.Show("轉單完成。");
                    }
                }
            }
        }

        private static void turnErpMethod( DateTime Date, string warehouse, string ID)
        {
            FL00SSEntities Erp = new FL00SSEntities();
            PickupDBEntities1 PickDB = new PickupDBEntities1();
  
            int OrderID = int.Parse(ID);                         //ReadyOrderAID 序號//轉int數值
            ReadyOrderA Ready_A = PickDB.ReadyOrderAs.Where(s => s.RA_ID == OrderID).First();//Ready A
            string Staff = Ready_A.RA_Staff;
            string Store = Ready_A.RA_Store; //門市
            string RA_Maping_PU_ID = Ready_A.RA_Maping_PU_ID;//揀貨單號
            string RA_BoxID = ID;//箱號
            long Amount = Ready_A.RA_Amount.Value;
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
            A.RARMA = RA_Maping_PU_ID + "-" + RA_BoxID;//備註
            A.RAMEN = Staff;//開單人員
            A.RATOL = 0;//合計金額
            A.RAQTY = Amount;//合計數量
            A.TRDAT = DateTime.Now;//修改日期
            A.TRMOD = "U";//異動型態
            A.TRUSR = Staff;//異動人員
            A.RAKIN = "01";//調撥說明
            A.RAXDT = Date;//開單時間
            Erp.RSIMAs.Add(A);
            Erp.SaveChanges();
            Ready_A.RA_Maping_ERP_ID = ERP_OrderSN;//Ready A單頭 erp對應序號

            PickDB.SaveChanges();//Ready A單頭儲存
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
                    Erp.RSIMBs.Add(B);
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
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
           
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {

                bool select = Item.Selected;
                if (select)
                {
                    {
                        PickupDBEntities1 PickDB = new PickupDBEntities1();
                        int readyAID = int.Parse(Item.Cells[0].Value.ToString());
                        PickDB.ReadyOrderBs.RemoveRange(PickDB.ReadyOrderBs.Where(s => s.RB_Maping_RA_ID == readyAID).ToList());
                        PickDB.ReadyOrderAs.Remove(PickDB.ReadyOrderAs.Find(readyAID));
                        PickDB.SaveChanges();
                        MessageBox.Show("刪除完成。");

                        PickupDBEntities1 pickupDBEntities1 = new PickupDBEntities1();
                        List<ReadyOrderA> readyOrderA = pickupDBEntities1.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == TempOrderInfo.Order).ToList();
                        dataGridView1.DataSource = readyOrderA;
                    }
                }
            }
        }

     
        private void A5_OpenBoxList_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            A2_Pickup_Start_Form PS = new A2_Pickup_Start_Form(TempOrderInfo);
            this.Close();
            PS.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {          
            A2_Pickup_Start_Form PS = new A2_Pickup_Start_Form(TempOrderInfo);
            this.Close();
            PS.Show();
        }
    }
    public class htcJson
    {
        public string epino { get; set; }
        public string ercsig { get; set; }
        public string ertel1 { get; set; }
        public string eraddr { get; set; }
        public string ejamt { get; set; }
        public string eqamt { get; set; }
        public string EMARK { get; set; }
        public string eddate { get; set; }
        public string esdate { get; set; }
        public string eprdct { get; set; }
    }
    public class htcJsonRespones
    {
        public string Num { get; set; }
        public string success { get; set; }
        public string edelno { get; set; }
        public string epino { get; set; }
        public string erstno { get; set; }
        public string eqamt { get; set; }
        public string image { get; set; }
        public string ErrMsg { get; set; }
    }
}
