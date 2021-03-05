using ImageMagick;
using Newtonsoft.Json;
using SalesInput.Modle;
using SalesInput.SqlString;
using SalesInput.View.Customer_Form;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesInput.Librarys
{
    /// <summary>
    /// 列印相關
    /// </summary>
    public class PrintLibrary
    {
        /// <summary>
        /// 列印標籤
        /// </summary>
        /// <param name="PrinterPath"></param>
        /// <param name="noticeDetails"></param>
        public void PrintLabel(string PrinterPath, List<NoticeDetail> noticeDetails)
        {
            try
            {
                BarCodeCtrl.openport(PrinterPath);
                //標籤的寬度(mm)、高度(mm)、列印速度、列印濃度、感應器類
                BarCodeCtrl.setup("100", "100", "6", "12", "0", "2", "0");
                BarCodeCtrl.clearbuffer();
                string DetailbmplFullPath = GetDetailFullPath(DateTime.Now.ToString("yyyyMMddHHmmss"), ImgFormat.BMP);
                string DetailpcxlFullPath = GetDetailFullPath(DateTime.Now.ToString("yyyyMMddHHmmss"), ImgFormat.PCX);
                string DetailFileName = GetFileName(DateTime.Now.ToString("yyyyMMddHHmmss"), ImgFormat.PCX);

                //檢查目錄是否存在
                if (!Directory.Exists("Detail\\"))
                    Directory.CreateDirectory("Detail\\");

                #region 列印揀貨總表
                int yTitle = 50;
                int iCount = noticeDetails.Count;//總共印幾筆
                int y = yTitle + 50;
                int i = 1;
                int intnextpage = 13;
                int intLableCount = 12; //一張標籤印幾筆商品
                int intTotalCount = 0; //共有幾件

                Bitmap newBitmap = new Bitmap(1000, 1000);
                Graphics g = Graphics.FromImage(newBitmap);
                g.Clear(Color.White);

                foreach (NoticeDetail item in noticeDetails.OrderBy(s => s.Location_1))
                {
                    if (i == 1 || ((i - 1) % intLableCount) == 0 && (iCount - i) > 0 || ((i - 1) % intLableCount) == 0 && iCount % intnextpage == 0)
                    {
                        if (((i - 1) % intLableCount) == 0)
                        {
                            intnextpage = intnextpage + intLableCount;
                        }

                        g.DrawString("儲位", new Font("標楷體", 30), Brushes.Black, 25, yTitle);
                        g.DrawString("型號", new Font("標楷體", 30), Brushes.Black, 250, yTitle);
                        g.DrawString("顏色", new Font("標楷體", 30), Brushes.Black, 475, yTitle);
                        g.DrawString("尺寸", new Font("標楷體", 30), Brushes.Black, 600, yTitle);
                        g.DrawString("數量", new Font("標楷體", 30), Brushes.Black, 750, yTitle);

                        y = yTitle + 50;
                    }
                    g.DrawString(item.Location_1.ToString(), new Font("標楷體", 30), Brushes.Black, 25, y);
                    g.DrawString(item.TypeName.ToString(), new Font("標楷體", 30), Brushes.Black, 250, y);
                    g.DrawString(item.Color.ToString(), new Font("標楷體", 30), Brushes.Black, 475, y);
                    g.DrawString(item.Size.ToString(), new Font("標楷體", 30), Brushes.Black, 600, y);
                    g.DrawString(item.Amount.ToString(), new Font("標楷體", 30), Brushes.Black, 750, y);
                    g.DrawString("----------------------------------------------------", new Font("標楷體", 25), Brushes.Black, 30, y + 50);

                    intTotalCount += Convert.ToInt32(item.Amount.ToString());
                    y += 70;

                    if (((i % intLableCount) == 0 && (iCount - i) > 0) || (iCount - i) == 0)
                    {
                        if ((iCount - i) == 0)
                        {
                            if (iCount % intLableCount == 0)
                            {
                                //套印
                                PrintDetailImage(newBitmap, DetailbmplFullPath, DetailpcxlFullPath, DetailFileName);

                                //換新頁面
                                newBitmap = new Bitmap(1000, 1000);
                                g = Graphics.FromImage(newBitmap);
                                g.Clear(Color.White);

                                yTitle = 50;
                                y = yTitle;
                            }

                            #region 繪製合計
                            g.DrawString("----------------------------------------------------", new Font("標楷體", 25), Brushes.Black, 30, y);
                            y += 70;
                            g.DrawString("總合計", new Font("標楷體", 30), Brushes.Black, 650, y);
                            g.DrawString(intTotalCount.ToString(), new Font("標楷體", 30), Brushes.Black, 820, y);
                            #endregion

                            //套印
                            PrintDetailImage(newBitmap, DetailbmplFullPath, DetailpcxlFullPath, DetailFileName);

                        }
                        else
                        {
                            //套印
                            PrintDetailImage(newBitmap, DetailbmplFullPath, DetailpcxlFullPath, DetailFileName);
                            //換新頁面
                            newBitmap = new Bitmap(1000, 1000);
                            g = Graphics.FromImage(newBitmap);
                            g.Clear(Color.White);
                        }


                        yTitle = 50;

                    }

                    i++;
                }
                #endregion

                BarCodeCtrl.closeport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 列印HCT標籤
        /// </summary>
        /// <param name="OrderID"></param>
        public void PrintHCTLabel(int OrderID)
        {
            PickupDBEntities1 PickDB = new PickupDBEntities1();
            NoticeA noticeA = PickDB.NoticeAs.Find(OrderID);
            if (noticeA.WorkState == "P" || noticeA.WorkState == "Y")
            {
                //設定狀態為出貨
                noticeA.WorkState = "Y";
                noticeA.OrderState = "Y";
                PickDB.SaveChanges();

                //列印圖片
                string imgString = noticeA.shipImg;
                if (imgString != "")
                {

                    string ServerUrl = Environment.CurrentDirectory;
                    image(GetBytes(imgString)).Save(ServerUrl + "\\tmp\\temp.jpg", ImageFormat.Jpeg);
                    PrintDocument PD = new PrintDocument();
                    PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                    PrintPreviewDialog PPD = new PrintPreviewDialog();
                    PPD.Document = PD;
                    PPD.ShowDialog();
                    List<NoticeA> noticeAList = PickDB.NoticeAs.Where(s => s.WorkState == "T" || s.WorkState == "P").ToList();
                    // dataGridView1.DataSource = noticeAList; //02-17
                }
                else
                {
                    string shipDate = DateTime.Now.ToString("yyMMdd");
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
                    htcJson.ercsig = noticeA.Name;
                    htcJson.ertel1 = noticeA.Tel;
                    htcJson.eraddr = noticeA.Address;
                    htcJson.ejamt = "1";
                    htcJson.eqamt = "1";
                    htcJson.EMARK = noticeA.Remark;
                    List<htcJson> htcList = new List<htcJson>();
                    htcList.Add(htcJson);
                    string json = JsonConvert.SerializeObject(htcList);
                    string response = service.TransData_Json(hctID, hctPassword, json);


                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow item in dataTable.Rows)
                    {
                        string ServerUrl = Environment.CurrentDirectory;
                        noticeA.shipImg = item["image"].ToString();
                        library.SQLUpdateData(salesInputSql.ShipInsert, DateTime.Now.ToString("yyyy-MM-dd"), shipID, item["edelno"].ToString(), noticeA.ERPMaping, OrderID.ToString(), noticeA.Name, noticeA.Tel, noticeA.Address, item["image"].ToString(), item["success"].ToString(), item["erstno"].ToString(), noticeA.Remark, "");
                    }
                    //轉單完成。

                    List<NoticeA> noticeAList = PickDB.NoticeAs.Where(s => s.WorkState == "T" || s.WorkState == "P").ToList();
                    // dataGridView1.DataSource = noticeAList; //02-17
                    PickDB.SaveChanges();//noticA 單頭儲存
                    Application.DoEvents();

                }
            }
            else
            {
                MessageBox.Show("尚未轉單。");
            }

        }
        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {

            Point location = new Point(0, 0);
            string ServerUrl = Environment.CurrentDirectory;
            Image img = Image.FromFile(ServerUrl + "\\tmp\\temp.jpg");
            e.Graphics.DrawImage(img, new Rectangle(0, 0, 400, 400));

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
        Image image(byte[] b)
        {
            MemoryStream ms = new MemoryStream(b);
            Image newImage = Image.FromStream(ms);
            return newImage;
        }

        /// <summary>
        /// 取得完整路徑(包含圖片名稱)
        /// </summary>
        /// <param name="No">The no.</param>
        /// <param name="p_ImgFormat">The p_ img format.</param>
        /// <returns></returns>
        private string GetDetailFullPath(string No, ImgFormat p_ImgFormat)
        {

            string DetailPath = "Detail\\";
            return string.Format("{0}{1}", DetailPath, GetFileName(No, p_ImgFormat));
        }
        /// <summary>
        /// 取得圖片名稱
        /// </summary>
        /// <param name="No">The p_ packet no.</param>
        /// <param name="p_ImgFormat">The p_ img format.</param>
        /// <returns></returns>
        private string GetFileName(string No, ImgFormat p_ImgFormat)
        {
            string fileName = string.Empty;
            if (p_ImgFormat == ImgFormat.PCX)
            {
                fileName = string.Format("{0}.PCX", No);//標籤機指定的格式

            }
            else if (p_ImgFormat == ImgFormat.BMP)
            {
                fileName = string.Format("{0}.BMP", No);//hct 傳來的圖片格式
            }
            return fileName;
        }
        #region 套印明細圖片
        private void PrintDetailImage(Bitmap newBitmap, string DetailbmplFullPath, string DetailpcxlFullPath, string DetailFileName)
        {

            newBitmap = ProcessImage(newBitmap, 800, 800);
            #endregion

            BitmapData bmpData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);

            newBitmap = new Bitmap(newBitmap.Width, newBitmap.Height, bmpData.Stride,
            PixelFormat.Format1bppIndexed, bmpData.Scan0);


            newBitmap.Save(DetailbmplFullPath);

            #region bmp 轉 pcx圖片(可能會有機率轉失敗)
            using (MagickImage imageTurn = new MagickImage(DetailbmplFullPath))
            {
                imageTurn.Write(DetailpcxlFullPath);
            }
            #endregion

            #region 印明細
            BarCodeCtrl.downloadpcx(DetailpcxlFullPath, DetailFileName);

            BarCodeCtrl.sendcommand(string.Format("PUTPCX 10,10,\"{0}\"", DetailFileName));
            #endregion

            newBitmap = new Bitmap(1000, 1000);

            BarCodeCtrl.printlabel("1", "1");
            BarCodeCtrl.clearbuffer();
        }

        #region 轉換圖片大小
        private static Bitmap ProcessImage(Bitmap originImage, int width, int height)
        {
            Bitmap resizedbitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(resizedbitmap);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(originImage, 0, 0, width, height);

            return resizedbitmap;
        }
        #endregion

        #region Enum

        /// <summary>
        /// 圖片格式
        /// </summary>
        enum ImgFormat
        {
            None,
            BMP,
            PCX
        }
        #endregion
    }
}
