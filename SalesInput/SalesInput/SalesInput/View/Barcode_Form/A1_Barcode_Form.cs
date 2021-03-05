using SalesInput.Librarys;
using SalesInput.SqlString;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput
{
    public partial class A1_Barcode_Form : Form
    {
        public A1_Barcode_Form()
        {
            InitializeComponent();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            SalesInputSQLstring Sql = new SalesInputSQLstring();
            FL00SSEntities Erp = new FL00SSEntities();
            TomLibrary Tom = new TomLibrary();
            Tom.SQLConnectionString = Sql.SQLConnectionString;
            string SqlQueryString = Sql.SQL_BarcodeString;

            if (textBox_Type.Text == "" && textBox_Size.Text == "" && textBox_Color.Text == "" && textBox_Barcode.Text == "")
            {
                MessageBox.Show("條件都沒輸入，賣來亂!");
            }
            else
            {
                if (textBox_Type.Text != "")
                {
                    SqlQueryString += " AND  BSTO.BSNCR ='" + textBox_Type.Text + "' ";
                }
                if (textBox_Size.Text != "")
                {
                    SqlQueryString += " AND  BSTO.BSSIZ ='" + textBox_Size.Text + "' ";
                }
                if (textBox_Color.Text != "")
                {
                    SqlQueryString += " AND  BSTO.BSCLR ='" + textBox_Color.Text + "' ";
                }
                if (textBox_Barcode.Text != "")
                {
                    SqlQueryString += " AND  BSTO.BSONU ='" + textBox_Barcode.Text + "' ";

                }
                System.Data.DataTable SQL_Data = Tom.SQLGetData(SqlQueryString); //DataSource
                dataGridView1.DataSource = SQL_Data;
                if (SQL_Data.Rows.Count != 0)
                {
                    button_Print.Visible = true;
                    button2.Visible = true;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Encoding defultEncoding = Encoding.GetEncoding(950);
            //saveFileDialog1.Filter = "txt|*.txt";
            //saveFileDialog1.Title = "請選擇儲存路徑";
            //saveFileDialog1.ShowDialog();
            string FileName = Application.StartupPath.ToString() + @"\barcodeprint\UNMSTB03.TXT";
            try { System.IO.File.Delete(FileName); } catch { } //刪除原TXT檔案

            string nulltxt = "　";
            FileStream fsFile = new FileStream(FileName, FileMode.OpenOrCreate);
            StreamWriter swWriter = new StreamWriter(fsFile, defultEncoding);
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0].Value != null)
                {
                    //----寫入數據------------
                    //型號ProductType--- [ITEM1]



                    string ProductType = "型   號:";
                    ProductType += item.Cells["型號"].Value.ToString() + "-" + item.Cells["顏色"].Value.ToString();
                    ProductType += ";";

                    //尺寸   Size --- [ITEM2]


                    string Size = "尺   寸:";
                    Size += item.Cells["尺寸"].Value.ToString();
                    Size += ";";


                    //售價   Price  [ITEM3]
                    string Price = "售   價:";
                    Price += string.Format("{0:C0}", int.Parse(item.Cells["最初價"].Value.ToString(), NumberStyles.AllowDecimalPoint));
                    Price += ";";


                    //成分1  Ingredients1  [ITEM4]
                    string Ingredients1 = "成   份:";
                    Ingredients1 += item.Cells["材質一"].Value.ToString();
                    Ingredients1 += ";";


                    //成分2  Ingredients2  [ITEM5]
                    string Ingredients2 = nulltxt;
                    Ingredients2 += item.Cells["材質二"].Value.ToString();
                    Ingredients2 += ";";


                    //成分3  Ingredients3 [ITEM6]
                    string Ingredients3 = nulltxt;
                    Ingredients3 += item.Cells["材質三"].Value.ToString();
                    Ingredients3 += ";";


                    //成分4  Ingredients4  [ITEM7]
                    string Ingredients4 = nulltxt;
                    Ingredients4 += item.Cells["材質四"].Value.ToString();
                    Ingredients4 += ";";


                    //成分5  Ingredients5  [ITEM8]
                    string Ingredients5 = nulltxt;
                    Ingredients5 += item.Cells["材質五"].Value.ToString();
                    Ingredients5 += ";";


                    //產地   Place        [ITEM9]
                    string Place = "產   地:";
                    Place += item.Cells["產地"].Value.ToString();
                    Place += ";";


                    //品名   ProductName  [ITEM10]
                    string ProductNameItem = item.Cells["中文名稱"].Value.ToString();
                    string ProductNameItem1 = ProductNameItem;
                    string ProductNameItem2 = "";
                    string ProductNameItem3 = "";
                    if (ProductNameItem1.Length > 8)
                    {
                        ProductNameItem1 = ProductNameItem.Substring(0, 8);

                        if (ProductNameItem1.Length == 8)
                        {
                            ProductNameItem2 = ProductNameItem.Substring(8, (ProductNameItem.Length - ProductNameItem1.Length));
                            if (ProductNameItem2.Length > 8)
                            {                                 
                                 ProductNameItem2 = ProductNameItem.Substring(8, 8);
                                ProductNameItem3 = ProductNameItem.Substring(8+ProductNameItem2.Length, (ProductNameItem.Length - ProductNameItem1.Length - ProductNameItem2.Length));
                            }
                        }
                    }


                    string ProductName = "品   名:";
                    ProductName += ProductNameItem1;
                    ProductName += ";";


                    //品名(預留)-無值帶空白 ProductName_ [ITEM11]
                    string ProductName_ = nulltxt;
                    ProductName_ += ProductNameItem2;
                    ProductName_ += ";";


                    //品名(預留)-無值帶空白 ProductName__ [ITEM2]
                    string ProductName__ = nulltxt;
                    ProductName__ += ProductNameItem3;
                    ProductName__ += ";";


                    //條碼     Barcode   [ITEM13]
                    string Barcode = "";
                    Barcode += getEanCode13(item.Cells["條碼"].Value.ToString());
                    Barcode += ";";


                    ////製造年月 Date      [ITEM14]
                    //string Date = "製造年月:";
                    //Date += item.Cells["製造年月"].Value.ToString();
                    //Date += ";";


                    ////製造商   Manufacturer  [ITEM15]
                    //string Manufacturer = "製造商:";
                    //Manufacturer += item.Cells["製造商"].Value.ToString();
                    //Manufacturer += ";";


                    ////電話     TEL         [ITEM16]
                    //string TEL = "電話:";
                    //TEL += item.Cells["電話"].Value.ToString();
                    //TEL += ";";


                    ////地址     Address     [ITEM17]

                    //string AddressItem = item.Cells["地址"].Value.ToString();
                    //string AddressItem1 = AddressItem;
                    //string AddressItem2 = "";
                    //string AddressItem3 = "";
                    //if (AddressItem1.Length > 8)
                    //{
                    //    AddressItem1 = AddressItem.Substring(0, 8);

                    //    if (AddressItem1.Length == 8)
                    //    {
                    //        AddressItem2 = AddressItem.Substring(8, (AddressItem.Length-AddressItem1.Length));
                    //        if (AddressItem2.Length > 8)
                    //        {
                    //            AddressItem2 = AddressItem.Substring(8,8);
                    //            AddressItem3 = AddressItem.Substring(8+AddressItem2.Length, (AddressItem.Length - AddressItem1.Length - AddressItem2.Length));
                    //        }
                    //    }
                    //}
                    //string Address = "地址:";
                    //Address += AddressItem1;
                    //Address += ";";


                    ////地址(預留)-無值帶空白 Address_  [ITEM18]
                    //string Address_ = nulltxt;
                    //Address_ += AddressItem2;
                    //Address_ += ";";


                    ////地址(預留)-無值帶空白 Address__  [ITEM19]
                    //string Address__ = nulltxt;
                    //Address__ += AddressItem3;
                    //Address__ += ";";


                    //數量    Amount     [ITEM20]
                    string Amount = "";
                    Amount += item.Cells[0].Value.ToString();


                    if (!checkBox1.Checked)
                    {
                        Ingredients1 = "成 份:詳見洗標標示;";
                        Ingredients2 = nulltxt + ";";
                        Ingredients3 = nulltxt + ";";
                        Ingredients4 = nulltxt + ";";
                        Ingredients5 = nulltxt + ";";
                    }


                    string ProductItem = ProductType + Size + Price + Ingredients1 + Ingredients2 + Ingredients3 + Ingredients4 + Ingredients5 + Place + ProductName + ProductName_ + ProductName__ + Barcode + item.Cells["型號"].Value.ToString() + "-" + item.Cells["顏色"].Value.ToString()+";"+ item.Cells["尺寸"].Value.ToString()+ ";" + Amount;

                    swWriter.WriteLine(ProductItem);

                }

            }

            swWriter.Close();
            MessageBox.Show("開始列印");
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + @"\barcodeprint\UNMSTB03.CMD");

        }


        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button_FileInput_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader st = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default);
                List<BarcodeItem> List_BarcodeItem = new List<BarcodeItem>();
                while (!st.EndOfStream)
                {
                    BarcodeItem Barcode_Item = new BarcodeItem();
                    string line = st.ReadLine();
                    string[] s = line.Split(',');

                    Barcode_Item.ProuctBarcode = s[0];
                    Barcode_Item.ProuctType = s[1];
                    Barcode_Item.ProuctSize = s[2];
                    Barcode_Item.ProuctPrice = s[4];
                    Barcode_Item.Describe1 = s[5];
                    Barcode_Item.Describe2 = s[6];
                    Barcode_Item.Describe3 = s[7];
                    Barcode_Item.Describe4 = s[8];
                    Barcode_Item.Describe5 = s[8];
                    Barcode_Item.Amount = s[9];
                    Barcode_Item.ProuctName = s[10];
                    Barcode_Item.ProuctLocation = s[11];
                    List_BarcodeItem.Add(Barcode_Item);
                }


                dataGridView2.DataSource = List_BarcodeItem;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Encoding defultEncoding = Encoding.GetEncoding(950);

            string FileName = Application.StartupPath.ToString() + @"\barcodeprint\UNMSTB03.TXT";
            try { System.IO.File.Delete(FileName); } catch { } //刪除原TXT檔案

            string nulltxt = "　";
            FileStream fsFile = new FileStream(FileName, FileMode.OpenOrCreate);
            StreamWriter swWriter = new StreamWriter(fsFile, defultEncoding);
            foreach (DataGridViewRow item in dataGridView2.Rows)
            {

                //----寫入數據------------
                //型號ProductType--- [ITEM1]

                string ProductType = "型   號:";
                ProductType += item.Cells["ProuctType"].Value.ToString();
                ProductType += ";";

                //尺寸   Size --- [ITEM2]

                string Size = "尺   寸:";
                Size += item.Cells["ProuctSize"].Value.ToString();
                Size += ";";

                //售價   Price  [ITEM3]
                string Price = "售   價:";
                Price += string.Format("{0:C0}", item.Cells["ProuctPrice"].Value.ToString());
                Price += ";";

                //成分1  Ingredients1  [ITEM4]
                string Ingredients1 = "成   份:";
                Ingredients1 += item.Cells["Describe1"].Value.ToString();
                Ingredients1 += ";";

                //成分2  Ingredients2  [ITEM5]
                string Ingredients2 = nulltxt;
                Ingredients2 += item.Cells["Describe2"].Value.ToString();
                Ingredients2 += ";";

                //成分3  Ingredients3 [ITEM6]
                string Ingredients3 = nulltxt;
                Ingredients3 += item.Cells["Describe3"].Value.ToString();
                Ingredients3 += ";";

                //成分4  Ingredients4  [ITEM7]
                string Ingredients4 = nulltxt;
                Ingredients4 += item.Cells["Describe4"].Value.ToString();
                Ingredients4 += ";";

                //成分5  Ingredients5  [ITEM8]
                string Ingredients5 = nulltxt;
                Ingredients5 += item.Cells["Describe5"].Value.ToString();
                Ingredients5 += ";";

                //產地   Place        [ITEM9]
                string Place = "產   地:";
                Place += item.Cells["ProuctLocation"].Value.ToString();
                Place += ";";

                //品名   ProductName  [ITEM10]
                string ProductNameItem = item.Cells["ProuctName"].Value.ToString();
                string ProductNameItem1 = ProductNameItem;
                string ProductNameItem2 = "";
                string ProductNameItem3 = "";
                if (ProductNameItem1.Length > 8)
                {
                    ProductNameItem1 = ProductNameItem.Substring(0, 8);

                    if (ProductNameItem1.Length == 8)
                    {
                        ProductNameItem2 = ProductNameItem.Substring(8, (ProductNameItem.Length - ProductNameItem1.Length));
                        if (ProductNameItem2.Length > 8)
                        {
                            ProductNameItem2 = ProductNameItem.Substring(8, 8);
                            ProductNameItem3 = ProductNameItem.Substring(8 + ProductNameItem2.Length, (ProductNameItem.Length - ProductNameItem1.Length - ProductNameItem2.Length));
                        }
                    }
                }



                string ProductName = "品   名:";
                ProductName += ProductNameItem1;
                ProductName += ";";

                //品名(預留)-無值帶空白 ProductName_ [ITEM11]
                string ProductName_ = nulltxt;
                ProductName_ += ProductNameItem2;
                ProductName_ += ";";

                //品名(預留)-無值帶空白 ProductName__ [ITEM2]
                string ProductName__ = nulltxt;
                ProductName__ += ProductNameItem3;
                ProductName__ += ";";

                //條碼     Barcode   [ITEM13]
                string Barcode = "";
                Barcode += item.Cells["ProuctBarcode"].Value.ToString();
                Barcode += ";";

                ////製造年月 Date      [ITEM14]
                //string Date = "製造年月:";

                //Date += ";";

                ////製造商   Manufacturer  [ITEM15]
                //string Manufacturer = "製造商:";

                //Manufacturer += ";";

                ////電話     TEL         [ITEM16]
                //string TEL = "電話:";

                //TEL += ";";

                ////地址     Address     [ITEM17]
                //string Address = "地址:";

                //Address += ";";

                ////地址(預留)-無值帶空白 Address_  [ITEM18]
                //string Address_ = nulltxt;
                //Address_ += "";
                //Address_ += ";";

                ////地址(預留)-無值帶空白 Address__  [ITEM19]
                //string Address__ = nulltxt;
                //Address__ += "";
                //Address__ += ";";

                //數量    Amount     [ITEM20]
                string Amount = "";
                Amount += item.Cells["Amount"].Value.ToString();

                string ProductItem = ProductType + Size + Price + Ingredients1 + Ingredients2 + Ingredients3 + Ingredients4 + Ingredients5 + Place + ProductName + ProductName_ + ProductName__ + Barcode + item.Cells["ProuctType"].Value.ToString() + ";" + item.Cells["ProuctSize"].Value.ToString() + ";" + Amount;

                swWriter.WriteLine(ProductItem);

            }
            swWriter.Close();
            MessageBox.Show("開始列印");
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + @"\barcodeprint\UNMSTB03.CMD");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

     
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {

                if (item.Cells[0].Value != null)
                {
                    TSCLIB_DLL.openport("TSC TTP-244CE_BARCODE");                                           //Open specified printer driver
                    TSCLIB_DLL.setup("60", "25", "4", "10", "0", "3", "0");                           //Setup the media size and sensor type info
                    TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
                    TSCLIB_DLL.windowsfont(50, 10, 32, 0, 0, 0, "ARIAL", "型　號:" + item.Cells["型號"].Value.ToString()+"-"+ item.Cells["顏色"].Value.ToString());  //Draw windows font
                    TSCLIB_DLL.windowsfont(50, 40, 32, 0, 0, 0, "ARIAL", "尺　寸:" + item.Cells["尺寸"].Value.ToString());  //Draw windows font
                    TSCLIB_DLL.windowsfont(50, 70, 32, 0, 0, 0, "ARIAL", "售　價:" + string.Format("{0:C0}", int.Parse(item.Cells["最初價"].Value.ToString(), NumberStyles.AllowDecimalPoint)));  //Draw windows font                  
                    TSCLIB_DLL.barcode("100", "120", "128", "50", "1", "0", "3", "3", getEanCode13(item.Cells["條碼"].Value.ToString())); //列印條碼
                    TSCLIB_DLL.printlabel("1", item.Cells[0].Value.ToString());                                                    //Print labels
                    TSCLIB_DLL.closeport();
                }
            }
           
        }


        public static string getEanCode13(string barcode)
        {
            if (barcode.Length == 12)
            {
                try
                {
                    char[] even = barcode.ToArray();
                    int num = 1;
                    int sumA = 0;
                    int sumB = 0;
                    int sumC = 0;
                    int ena13 = 0;
                    for (int i = 0; i < even.Count(); i++)
                    {
                        if (num % 2 == 0)
                        {
                            sumA += int.Parse(even[i].ToString());
                        }
                        else
                        {
                            sumB += int.Parse(even[i].ToString());
                        }

                        num++;
                    }
                    sumC = ((sumA * 3) + sumB);
                    ena13 = (10 - (sumC % 10)) % 10;
                    return barcode + ena13.ToString();
                }
                catch
                {
                    return barcode;
                }
            }
            else
            {
                return barcode;
            }
        } //取得EAN13碼
    }

    public class BarcodeItem
    {
        public string ProuctBarcode { get; set; }//0
        public string ProuctType { get; set; }//1
        public string ProuctSize { get; set; }//2
        public string ProuctPrice { get; set; }//3
        public string Describe1 { get; set; }//4
        public string Describe2 { get; set; }//5
        public string Describe3 { get; set; }//6
        public string Describe4 { get; set; }//7
        public string Describe5 { get; set; }//8
        public string Amount { get; set; }//9
        public string ProuctName { get; set; }//10
        public string ProuctLocation { get; set; }//11
    }

}
