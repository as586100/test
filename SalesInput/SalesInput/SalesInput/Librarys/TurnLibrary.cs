using Newtonsoft.Json;
using SalesInput.Modle;
using SalesInput.SqlString;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.Librarys
{
    class TurnLibrary
    {
        string warehouse = "3998";

        public void Trun(int OrderID)
        {
            Application.DoEvents();
            IPAddress IP = IPAddress.Parse("192.168.0.219");
            string state = "Y"; //state 若其中一個品項=0 ;則回傳缺貨
            ///-----------------開立調撥單-----------------------------
            if (ByPing(IP))
            {
                PickupDBEntities1 PickDB = new PickupDBEntities1();
                FL00SSEntities Erp = new FL00SSEntities();
                DateTime Date = DateTime.Now;
                NoticeA noticeA = PickDB.NoticeAs.Find(OrderID);
                string pos_NoticA_ID = noticeA.NoticeMaping;
                string Staff = "3998"; //開單人
                string Store = noticeA.Require; //門市
                string receiverName = noticeA.Name;//收件人
                string receiverTel = noticeA.Tel;//收件人電話 
                string receiverAdd = noticeA.Address;//收件人地址
                string reciverMark = noticeA.Remark;//收件人備註
                string ERP_OrderSN = "";            //ERP新單號
                if (noticeA.ERPMaping == null)
                {
                    //取noticeID 轉數值
                    Staff = "3998"; //開單人
                    Store = noticeA.Require; //門市
                    string noticeA_Maping = noticeA.NoticeMaping;//揀貨單號
                    int Amount = PickDB.NoticeBs.Where(s => s.Maping == OrderID).Sum(s => s.Shipment).HasValue ? PickDB.NoticeBs.Where(s => s.Maping == OrderID).Sum(s => s.Shipment).Value : 0; //noticeb sum(出貨量)
                    string Temp_Date_SN = Date.ToString("yyMM");//日期-單號用                                   
                    string Temp_SN = "M" + Temp_Date_SN + Staff;         //M+日期+開單人        
                    if (Erp.RSIMAs.Where(s => s.RAREN.StartsWith(Temp_SN)).Any())
                    {
                        ERP_OrderSN = Erp.RSIMAs.Where(s => s.RAREN.StartsWith(Temp_SN)).OrderByDescending(s => s.RAREN).First().RAREN;
                        string sort = ERP_OrderSN.Substring(9, 4);
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
                    A.RADAY = Date.Date;//調撥日期
                    A.RACLS = "M";//進銷存類別
                    A.RARMA = String.Format("#{0},{1},{2}", noticeA_Maping, receiverName, receiverTel);//備註
                    A.RAMEN = Staff;//開單人員
                    A.RATOL = 0;//合計金額
                    A.RAQTY = Amount;//合計數量
                    A.TRDAT = DateTime.Now;//修改日期
                    A.TRMOD = "U";//異動型態
                    A.TRUSR = Staff;//異動人員
                    A.RAKIN = "01";//調撥說明
                    A.RAXDT = Date.Date;//開單時間
                    Erp.RSIMAs.Add(A);
                    //Erp.SaveChanges();
                    noticeA.WorkState = "P";
                    noticeA.ERPMaping = ERP_OrderSN;//notice A單頭 erp對應序號
                                                    //PickDB.SaveChanges();//Ready A單頭儲存
                                                    //------------------單身----------------------------------

                    List<NoticeB> noticeB_List = PickDB.NoticeBs.Where(s => s.Maping == OrderID).ToList();
                    int itemSN = 0;
                    List<string> TempProduct = new List<string>();
                    List<RSIMB> TempRsimb = new List<RSIMB>();
                    foreach (var item in noticeB_List)
                    {
                        SalesInputSQLstring Sql = new SalesInputSQLstring();
                        //FILA DB 更新noticeB
                        TomLibrary tomLibrary = new TomLibrary();
                        tomLibrary.SQLConnectionString = Sql.FILASQLConnectionString;
                        tomLibrary.SQLUpdateData(Sql.updateNoticeB, item.Shipment.Value.ToString(), pos_NoticA_ID, item.TypeName, item.Color, item.Size);

                        //ERP 調撥單
                        TomLibrary Tom = new TomLibrary();
                        Tom.SQLConnectionString = Sql.SQLConnectionString;
                        string SqlQueryString = Sql.SQL_ProductDetail;
                        System.Data.DataTable SQL_Data = Tom.SQLGetData(SqlQueryString, item.Barcode); //DataSource
                        string Name = SQL_Data.Rows[0][0].ToString();
                        string Color = SQL_Data.Rows[0][1].ToString();
                        string NameColor = Name + Color;
                        //判斷若型號顏色重覆

                        if (item.Shipment.Value != 0) //揀貨量不等於0，才紀錄到調撥單
                        {

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
                                B.RBDC1 = item.Shipment;//數量加減
                                B.RBQTY = item.Shipment;//數量(調撥量)
                                B.RBRQY = 0;//數量(驗收量)
                                B.RBCQY = 0;//數量(取消量)
                                B.RBFQY = item.Shipment;//數量(差異量)
                                B.RBRMK = "";//備註
                                B.RBCLZ = SQL_Data.Rows[0][3].ToString();//尺寸類別
                                switch (SQL_Data.Rows[0][4].ToString())//尺寸數量
                                {
                                    case "RBQTY01":
                                        B.RBQTY01 = item.Shipment;
                                        break;
                                    case "RBQTY02":
                                        B.RBQTY02 = item.Shipment;
                                        break;
                                    case "RBQTY03":
                                        B.RBQTY03 = item.Shipment;
                                        break;
                                    case "RBQTY04":
                                        B.RBQTY04 = item.Shipment;
                                        break;
                                    case "RBQTY05":
                                        B.RBQTY05 = item.Shipment;
                                        break;
                                    case "RBQTY06":
                                        B.RBQTY06 = item.Shipment;
                                        break;
                                    case "RBQTY07":
                                        B.RBQTY07 = item.Shipment;
                                        break;
                                    case "RBQTY08":
                                        B.RBQTY08 = item.Shipment;
                                        break;
                                    case "RBQTY09":
                                        B.RBQTY09 = item.Shipment;
                                        break;
                                    case "RBQTY10":
                                        B.RBQTY10 = item.Shipment;
                                        break;
                                    case "RBQTY11":
                                        B.RBQTY11 = item.Shipment;
                                        break;
                                    case "RBQTY12":
                                        B.RBQTY12 = item.Shipment;
                                        break;
                                    case "RBQTY13":
                                        B.RBQTY13 = item.Shipment;
                                        break;
                                    case "RBQTY14":
                                        B.RBQTY14 = item.Shipment;
                                        break;
                                    case "RBQTY15":
                                        B.RBQTY15 = item.Shipment;
                                        break;
                                    case "RBQTY16":
                                        B.RBQTY16 = item.Shipment;
                                        break;
                                    case "RBQTY17":
                                        B.RBQTY17 = item.Shipment;
                                        break;
                                    case "RBQTY18":
                                        B.RBQTY18 = item.Shipment;
                                        break;
                                    case "RBQTY19":
                                        B.RBQTY19 = item.Shipment;
                                        break;
                                    case "RBQTY20":
                                        B.RBQTY20 = item.Shipment;
                                        break;
                                }
                                //Erp.RSIMBs.Add(B);
                                TempRsimb.Add(B);
                            }
                            else
                            {
                                //若型號顏色重覆
                                RSIMB B = TempRsimb.Where(s => s.RBREN == ERP_OrderSN && s.RBNCR == Name && s.RBCLR == Color).First();
                                B.RBDC1 += item.Shipment;//數量加減
                                B.RBQTY += item.Shipment;//數量(調撥量)
                                B.RBFQY += item.Shipment;//數量(差異量)
                                switch (SQL_Data.Rows[0][4].ToString())//尺寸數量
                                {
                                    case "RBQTY01":
                                        if (B.RBQTY01 != null)
                                        {
                                            B.RBQTY01 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY01 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY02":
                                        if (B.RBQTY02 != null)
                                        {
                                            B.RBQTY02 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY02 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY03":
                                        if (B.RBQTY03 != null)
                                        {
                                            B.RBQTY03 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY03 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY04":
                                        if (B.RBQTY04 != null)
                                        {
                                            B.RBQTY04 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY04 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY05":
                                        if (B.RBQTY05 != null)
                                        {
                                            B.RBQTY05 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY05 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY06":
                                        if (B.RBQTY06 != null)
                                        {
                                            B.RBQTY06 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY06 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY07":
                                        if (B.RBQTY07 != null)
                                        {
                                            B.RBQTY07 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY07 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY08":
                                        if (B.RBQTY08 != null)
                                        {
                                            B.RBQTY08 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY08 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY09":
                                        if (B.RBQTY09 != null)
                                        {
                                            B.RBQTY09 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY09 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY10":
                                        if (B.RBQTY10 != null)
                                        {
                                            B.RBQTY10 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY10 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY11":
                                        if (B.RBQTY11 != null)
                                        {
                                            B.RBQTY11 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY11 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY12":
                                        if (B.RBQTY12 != null)
                                        {
                                            B.RBQTY12 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY12 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY13":
                                        if (B.RBQTY13 != null)
                                        {
                                            B.RBQTY13 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY13 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY14":
                                        if (B.RBQTY14 != null)
                                        {
                                            B.RBQTY14 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY14 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY15":
                                        if (B.RBQTY15 != null)
                                        {
                                            B.RBQTY15 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY15 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY16":
                                        if (B.RBQTY16 != null)
                                        {
                                            B.RBQTY16 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY16 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY17":
                                        if (B.RBQTY17 != null)
                                        {
                                            B.RBQTY17 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY17 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY18":
                                        if (B.RBQTY18 != null)
                                        {
                                            B.RBQTY18 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY18 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY19":
                                        if (B.RBQTY19 != null)
                                        {
                                            B.RBQTY19 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY19 = item.Shipment;
                                        }
                                        break;
                                    case "RBQTY20":
                                        if (B.RBQTY20 != null)
                                        {
                                            B.RBQTY20 += item.Shipment;
                                        }
                                        else
                                        {
                                            B.RBQTY20 = item.Shipment;
                                        }
                                        break;
                                }

                            }
                        }
                        else
                        {
                            state = "N";//state 若其中一個品項=0 ;則回傳缺貨
                        }
                    }
                    Erp.RSIMBs.AddRange(TempRsimb);
                    Erp.SaveChanges();
                    PickDB.SaveChanges();//noticB 單頭儲存
                    ///------------------貨運序號-----------------------------
                    string shipDate = Date.ToString("yyMMdd");
                    string shipID;
                    TomLibrary library = new TomLibrary();
                    SalesInputSQLstring salesInputSql = new SalesInputSQLstring();
                    library.SQLConnectionString = salesInputSql.FILASQLConnectionString;
                    DataTable oreder_DataTable = library.SQLGetData("select * from [FILA].[dbo].[POS_ShipOrder] where [ShipID] like @A order by shipID DESC ", shipDate + "%");
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
                    ///
                    string hctID = "04833400005";//hct帳號
                    string hctPassword = "0423693558";// hct密碼
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
                    htcJson.EMARK = reciverMark;
                    List<htcJson> htcList = new List<htcJson>();
                    htcList.Add(htcJson);
                    string json = JsonConvert.SerializeObject(htcList);
                    string response = service.TransData_Json(hctID, hctPassword, json);


                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow item in dataTable.Rows)
                    {
                        string ServerUrl = Environment.CurrentDirectory;
                        noticeA.shipImg = item["image"].ToString();
                        library.SQLUpdateData(salesInputSql.ShipInsert, DateTime.Now.ToString("yyyy-MM-dd"), shipID, item["edelno"].ToString(), ERP_OrderSN, OrderID.ToString(), receiverName, receiverTel, receiverAdd, item["image"].ToString(), item["success"].ToString(), item["erstno"].ToString(), reciverMark, "");
                        library.SQLUpdateData(salesInputSql.updateNoticeA, state, ERP_OrderSN, item["edelno"].ToString(), pos_NoticA_ID); //state 若其中一個品項=0 ;則回傳缺貨
                    }
                    //轉單完成。

                    List<NoticeA> noticeAList = PickDB.NoticeAs.Where(s => s.WorkState == "T" || s.WorkState == "P").ToList();
                    PickDB.SaveChanges();//noticA 單頭儲存
                    Application.DoEvents();
                    MessageBox.Show("轉單完成。");
                }
                else
                {
                    MessageBox.Show("此單已轉。");
                }
            }
            else
            {
                MessageBox.Show("請到網路良好暢通的地方再試一次。");
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
