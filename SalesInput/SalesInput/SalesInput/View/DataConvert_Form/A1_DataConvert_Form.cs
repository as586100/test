using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.DataConvert_Form
{
    public partial class A1_DataConvert_Form : Form
    {
        public A1_DataConvert_Form()
        {
            InitializeComponent();
        }

        private void buttonConverBarcode_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string tableName = textBox_SheetName1.Text;
                DataTable excelData = GetExcelDataTable(openFileDialog1.FileName, "select * from [" + tableName + "$]", tableName, Path.GetExtension(openFileDialog1.FileName));
                string txtData = "";
                foreach (DataRow item in excelData.Rows)
                {
                    Application.DoEvents();
                    try
                    {
                        txtData += textBox_store.Text + "," + GetProductBarcode(item[0].ToString(), item[1].ToString(), item[2].ToString()) + "," + item[3].ToString() + "\r\n";
                    }
                    catch
                    {

                    }                  
                    saveFileDialog1.Filter = "Text File 活頁簿(*.txt)|*.txt";
                    saveFileDialog1.Title = "Save an Text File";
                }
                MessageBox.Show("選擇要轉存txt位置");

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                    streamWriter.WriteLine(txtData);
                    streamWriter.Close();
                    MessageBox.Show("轉換完成", "完成");
                    this.Cursor = Cursors.Default;
                }


            }
           
        }
        private DataTable GetExcelDataTable(string filePath, string sql, string tableName, string type)
        {
            OleDbConnection conn = new OleDbConnection();
            if (type == ".xls")
            {
                ////Office 2003
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;Readonly=0'");
            }
            else
            {
                //Office 2007
                conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'");
            }

            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.TableName = tableName;
            conn.Close();
            return dt;
        }
        public string GetProductBarcode(string typeName, string color, string size)//取商品條碼---參數型號,顏色,尺寸
        {
            FL00SSEntities erpDB = new FL00SSEntities();
            string barcode = erpDB.BSTOes.Where(s => s.BSNCR == typeName && s.BSCLR == color && s.BSSIZ == size).Any() ? erpDB.BSTOes.Where(s => s.BSNCR == typeName && s.BSCLR == color && s.BSSIZ == size).First().BSONU : "";
            return barcode;
        }

        public static Product GetProductForBarcode_public(string barcode)//取商品基本檔---參數條碼
        {
            FL00SSEntities erpDB = new FL00SSEntities();
            Product product = new Product();
            BSTO productItem = erpDB.BSTOes.Where(s => s.BSONU == barcode).Any() ? erpDB.BSTOes.Where(s => s.BSONU == barcode).First() : null;
            if (productItem != null)
            {
                product.Barcode = productItem.BSONU;//條碼
                product.TypeName = productItem.BSNCR;//型號
                product.Color = productItem.BSCLR;//顏色
                product.Size = productItem.BSSIZ;//尺寸
            }
            return product;
        }
        public class Product                            //商品基本檔類別
        {
            public string Barcode { get; set; }
            public string TypeName { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public int Qty { get; set; }
        }

        private void buttonConver1D_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string tableName = textBox_SheetName2.Text;
                DataTable excelData = GetExcelDataTable(openFileDialog1.FileName, "select * from [" + tableName + "$]", tableName, Path.GetExtension(openFileDialog1.FileName));
                IWorkbook wb = new HSSFWorkbook();
                //建立Excel 2003檔案
                wb = new HSSFWorkbook();
                ISheet ws;
                ws = wb.CreateSheet("Sheet1");
                ws.CreateRow(0);//第一行為欄位名稱
                Application.DoEvents();
                ws.GetRow(0).CreateCell(0).SetCellValue("型號");
                ws.GetRow(0).CreateCell(1).SetCellValue("顏色");
                ws.GetRow(0).CreateCell(2).SetCellValue("尺寸");
                ws.GetRow(0).CreateCell(3).SetCellValue(textBox3.Text);
                ws.AutoSizeColumn(0);
                ws.AutoSizeColumn(1);
                ws.AutoSizeColumn(2);
                ws.AutoSizeColumn(3);
                int Row = 0;
                foreach (DataRow item in excelData.Rows)
                {
                    Application.DoEvents();
                    try
                    {

                        for (int i = 2; i < excelData.Columns.Count; i++)
                        {
                            if (item[i].ToString() != "")
                            {
                                Row++;
                                ws.CreateRow(Row);
                                Application.DoEvents();
                                ws.GetRow(Row).CreateCell(0).SetCellValue(item[0].ToString());
                                ws.GetRow(Row).CreateCell(1).SetCellValue(item[1].ToString());
                                ws.GetRow(Row).CreateCell(2).SetCellValue(excelData.Columns[i].ToString());
                                ws.GetRow(Row).CreateCell(3).SetCellValue(item[i].ToString());
                            }
                        }
                    }
                    catch
                    {

                    }
                    saveFileDialog1.Filter = "Excel File 活頁簿(*.xls)|*.xls";
                    saveFileDialog1.Title = "Save an Excel File";
                }
                MessageBox.Show("選擇要轉存Excel位置");

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Create);//產生檔案
                    wb.Write(file);
                    file.Close();

                    MessageBox.Show("轉換完成", "完成");
                    this.Cursor = Cursors.Default;
                }


            }
        }
    }
}
