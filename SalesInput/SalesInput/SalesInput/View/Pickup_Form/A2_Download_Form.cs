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
using SalesInput.Librarys;
using SalesInput.SqlString;


namespace SalesInput.View.Pickup_Form
{
    public partial class A2_Download_Form : Form
    {
        public TextBox TB = new TextBox();
        string Staff = ""; //員工編號
        public A2_Download_Form(string staff)
        {
            InitializeComponent();
            Staff = staff;
            //this.dateTimePicker1.CustomFormat = " ";
            //this.dateTimePicker1.Format = DateTimePickerFormat.Custom;



        }

        private void textBoxNum_MouseDown(object sender, MouseEventArgs e)
        {
            TB = (TextBox)sender;
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {

            TB.Focus();
            Button btn = (Button)sender;
            SendKeys.Send(btn.Text);
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            TB.Focus();

            SendKeys.Send("{BACKSPACE}");
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查詢
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
                StringBuilder SqlStringBuilder = new StringBuilder();
                SqlStringBuilder.Append(SQL.PickupADetail.ToString());
                //string SqlString = SQL.PickupADetail;

                if (textBoxOrder.Text != "")
                {
                    SqlStringBuilder.AppendFormat(" and [OrderID] ='{0}'", textBoxOrder.Text);
                    //SqlString += " and [OrderID] ='" + textBoxOrder.Text + "'";
                }

                if (textBoxStore.Text != "")
                {
                    SqlStringBuilder.AppendFormat(" and [OrderStore] ='{0}'", textBoxStore.Text);

                    //SqlString += " and [OrderStore] ='" + textBoxStore.Text + "'";
                }
                if (dateTimePicker1.Text != " ")
                {
                    SqlStringBuilder.AppendFormat(" and [OrderDate] ='{0}'", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
                    //SqlString += " and [OrderDate] ='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "'";

                }
                if (checkBox1.Checked)
                {
                    SqlStringBuilder.Append(" and [OrderUploade] ='Y'");
                    //SqlString += " and [OrderUploade] ='Y' ";
                }
                else
                {
                    SqlStringBuilder.Append(" and [OrderUploade] ='N'");
                    //SqlString += " and [OrderUploade] ='N' ";
                }
                SqlStringBuilder.Append("  and( [OrderStaff]!='轉久新' OR [OrderStaff] IS NULL) ORDER BY OrderLIST ");
                //SqlString += "  and( [OrderStaff]!='轉久新' OR [OrderStaff] IS NULL) ORDER BY OrderLIST ";

                DataTable Table = TOM.SQLGetData(SqlStringBuilder.ToString());
                //DataTable Table = TOM.SQLGetData(SqlString);
                dataGridView1.DataSource = Table;
            }
            catch { MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。"); }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        /// <summary>
        /// 下載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDownLoad_Click(object sender, EventArgs e)
        {

            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            TOM.SQLConnectionString = SQL.FILASQLConnectionString;
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                Application.DoEvents();
                bool SelectCheck = (null != Item.Cells[0] && null != Item.Cells[0].Value && true == (bool)Item.Cells[0].Value);

                if (SelectCheck)
                {
                    PickupDBEntities1 PickDB = new PickupDBEntities1();

                    PickupA A = new PickupA();
                    A.OrderID = Item.Cells[1].Value.ToString();
                    StringBuilder SqlStringBuilder = new StringBuilder();
                    SqlStringBuilder.Append(SQL.PickupADetail);//重覆下載判斷
                    //string SqlString = SQL.PickupADetail; //重覆下載判斷
                    SqlStringBuilder.AppendFormat("and [OrderID] ='{0}' ", Item.Cells[1].Value.ToString());
                    //SqlString += " and [OrderID] ='" + Item.Cells[1].Value.ToString() + "'";
                    DataTable Table = TOM.SQLGetData(SqlStringBuilder.ToString());
                    //DataTable Table = TOM.SQLGetData(SqlString);
                    string doubleChek = Table.Rows[0][5].ToString();//重覆下載判斷
                    bool pass = false; // 重覆下載判斷
                    if (doubleChek == "Y")
                    {
                        //pass = MessageBox.Show("單號:" + A.OrderID + " 已被下載，確定還要下載?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes;
                        pass = MessageBox.Show(string.Format("單號:{0} 已被下載，確定還要下載?", A.OrderID), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes;
                    }
                    else
                    {
                        pass = true;
                    }

                    if (pass) //判斷是否被下載過
                    {
                        TOM.SQLUpdateData(SQL.PickupUpdate, Item.Cells[1].Value.ToString(), Staff);
                        A.OrderStore = Item.Cells[2].Value.ToString();
                        A.OrderName = Item.Cells[3].Value.ToString();
                        A.OrderAmount = int.Parse(Item.Cells[4].Value.ToString());
                        A.OrderSate = "N";//單據狀況
                        A.OrderUploade = "Y";//下載狀況
                        A.OrderDate = DateTime.Parse(Item.Cells[7].Value.ToString());
                        A.OrderStaff = Staff;
                        PickDB.PickupAs.Add(A);
                        PickDB.SaveChanges();
                        DataTable PickTable = TOM.SQLGetData(SQL.PickupBDetail, Item.Cells[1].Value.ToString());
                        foreach (DataRow item in PickTable.Rows)
                        {
                            //---------------建立表身----------------------------------------------
                            PickupB B = new PickupB
                            {
                                OrderSN = int.Parse(item[0].ToString()), //序號
                                OrderID = item[1].ToString(),            //單頭key(外鍵)
                                Order_Location = item[2].ToString(),     //儲位
                                Order_Location2 = item[13].ToString()    //儲位2
                            };
                            try
                            {
                                B.Order_Stock = int.Parse(item[14].ToString()); //庫存
                            }
                            catch
                            {
                                B.Order_Stock = 0; //庫存
                            }
                            B.Order_Type = item[3].ToString();         //型號
                            B.Order_Size = item[5].ToString();         //尺寸
                            B.Order_Color = item[4].ToString();        //顏色
                            B.Order_Amount = int.Parse(item[6].ToString()); //應揀量
                            B.Order_PickAmount = 0;//實揀量
                            B.Order_Barcode = item[8].ToString();              //條碼
                                                                               //B.Order_BoxNum = int.Parse(item[9].ToString());//箱序
                            B.Order_BoxState = item[10].ToString();       //封箱狀態
                            B.Order_Store = item[11].ToString();//櫃點
                            B.Order_State = item[12].ToString();//單據狀態
                            PickDB.PickupBs.Add(B);
                            PickDB.SaveChanges();
                        }
                    }

                }

            }

            MessageBox.Show("下載完成。");
            dataGridView1.DataSource = null;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class DownloadItem
    {
        public string 單號 { get; set; }
        public string 櫃號 { get; set; }
        public string 櫃名 { get; set; }
        public int 數量 { get; set; }
        public string 狀態 { get; set; }
    }
}
