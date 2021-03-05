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
using System.Media;
using System.Threading;
using SalesInput.ObjectFile;

namespace SalesInput.View.Pickup_Form
{
    public partial class A2_Pickup_Start_Form : Form
    {
        int second = 0;
        PickupDBEntities1 DB = new PickupDBEntities1();
        OrderInfo TempOrderInfo = new OrderInfo();
  
        public A2_Pickup_Start_Form(OrderInfo orderInfo)
        {
            InitializeComponent();
            TempOrderInfo.Order = orderInfo.Order;//取單號
            TempOrderInfo.OrderType = orderInfo.OrderType; //調撥類型
            TempOrderInfo.OrderOutput = orderInfo.OrderOutput;//調出單位
            TempOrderInfo.OrderDate = orderInfo.OrderDate;//調撥日期
            TempOrderInfo.OrderNote = orderInfo.OrderNote;
            TempOrderInfo.OrderShipDate = orderInfo.OrderShipDate;//出貨單日期
            TempOrderInfo.OrderPayment = orderInfo.OrderPayment;


            labelOrderType.Text = TempOrderInfo.OrderType; //調撥類型
            labelOrderOutput.Text = TempOrderInfo.OrderOutput;//調出單位
            labelOrderDate.Text = TempOrderInfo.OrderDate;//調撥日期
            textBoxNote.Text = TempOrderInfo.OrderNote;
        
            if (TempOrderInfo.OrderShipDate == null|| TempOrderInfo.OrderShipDate == " ")
            {
                this.dateTimePicker1.CustomFormat = " ";
                this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.Text = TempOrderInfo.OrderShipDate;

            }

            if (TempOrderInfo.OrderPayment!=null)
            {
                if (TempOrderInfo.OrderPayment=="11")
                {
                    comboBoxPayment.SelectedIndex = 1;
                }
            }


            List <PickupB> PickupSource = DB.PickupBs.Where(S => S.OrderID == TempOrderInfo.Order&& S.Order_State == "N" && S.Order_BoxState == "N").OrderBy(s => s.Order_Location).ThenBy(s => s.Order_Type).ThenBy(s => s.Order_Color).ToList();
         
            int BoxID = 1;
            if (DB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == TempOrderInfo.Order).Any())
            {
                int _BoxID = int.Parse(DB.ReadyOrderAs.Where(s => s.RA_Maping_PU_ID == TempOrderInfo.Order).OrderByDescending(s => s.RA_BoxID).First().RA_BoxID.ToString());
                BoxID = _BoxID + 1;
            }
            button_OpenBox.Text = string.Format("第{0}箱", BoxID);
            //if (BoxID > 1)
            //{
            //    button_OpenBox.Text += string.Format("\n\n解開第『{0}』箱", BoxID - 1);
            //    button_OpenBox.Enabled = true;
            //}
           LableUpdate();//更新LABLE 總數
            timerRecord.Enabled = true;
            if (PickupSource.Count != 0)
            {
                dataGridView1.DataSource = PickupSource;
                this.ActiveControl = textBox1;
            }
            else
            {
                MessageBox.Show("此單已揀貨完畢。");

            }
            //*20170928 新增判斷揀貨時間
           if(DB.PickupAs.Where(s=>s.OrderID == TempOrderInfo.Order).First().OrderSpendTime!=null)
            {
                string[] arrayTime = DB.PickupAs.Where(s => s.OrderID == TempOrderInfo.Order).First().OrderSpendTime.Split(':');
                second += (int.Parse(arrayTime[0]) * 3600) + (int.Parse(arrayTime[1]) * 60) + int.Parse(arrayTime[2]);
            }

        }
       



        private void LableUpdate()
        {
            List<PickupB> SumList = DB.PickupBs.Where(S => S.OrderID == TempOrderInfo.Order).ToList();
            long Must = SumList.Sum(s => s.Order_Amount).Value;
            long Real = SumList.Sum(s => s.Order_PickAmount).Value;
            long Difference = Must - Real;
            long shoes = SumList.Where(s => s.Order_Type.Substring(1, 1) == "-").Sum(s => s.Order_Amount).Value;

            label_Must.Text = "應揀合計:" + Must.ToString();
            label_Real.Text = "實揀合計:" + Real.ToString();
            label_Difference.Text = "差異合計:" + Difference.ToString();
            Label_shoes.Text = "鞋子總數:" + shoes.ToString();

           



        }
        private void button_back_Click(object sender, EventArgs e)
        {
            //*20170928 返回時揀貨時間回存
            TimeSpan ts = new TimeSpan(0, 0, second);
            string SpendTime = (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds;
            PickupA A = DB.PickupAs.Find(TempOrderInfo.Order);
            A.OrderSpendTime = SpendTime;
            DB.SaveChanges();

            this.Close();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
               
            bool state = false;
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length >= 12)
                {
                    string Barcode = textBox1.Text.Substring(0, 12);
                    foreach (DataGridViewRow Item in dataGridView1.Rows)
                    {
                        if (Item.Cells["條碼"].Value.ToString() == Barcode)
                        {
                            state = true;
                            int SelectIndex = Item.Index;
                            PickupB ITEM = DB.PickupBs.Where(s => s.Order_Barcode == Barcode&&s.OrderID == TempOrderInfo.Order && s.Order_BoxState=="N").First();
                            int pickAmount = int.Parse(defaultAmountTextBox.Text);
                            if(ITEM.Order_PickAmount< ITEM.Order_Amount)
                            {
                                //ITEM.Order_PickAmount++;
                                ITEM.Order_PickAmount = ITEM.Order_PickAmount.Value + pickAmount;  //加預設數量
                            }
                          

                            if (ITEM.Order_PickAmount == ITEM.Order_Amount)
                            {
                                ITEM.Order_State = "Y";

                                if (SelectIndex >= dataGridView1.Rows.Count - 1)
                                {
                                    SelectIndex = dataGridView1.Rows.Count - 2;
                                }
                            }

                            if (SelectIndex< 0)
                            {
                                SelectIndex = 0;
                            }

                            DB.SaveChanges();
                            dataGridView1.DataSource = DB.PickupBs.Where(S => S.OrderID == TempOrderInfo.Order && S.Order_State == "N" && S.Order_BoxState == "N").OrderBy(s => s.Order_Location).ThenBy(s=>s.Order_Type).ThenBy(s=>s.Order_Color).ToList();         
                            if (dataGridView1.RowCount!=0)
                            {
                                dataGridView1.Rows[SelectIndex].Selected = true;

                                this.dataGridView1.FirstDisplayedScrollingRowIndex = SelectIndex;
                            }

                            LableUpdate();
                        }               
                    }
                }
                if (!state)
                {
                    string ServerUrl = Environment.CurrentDirectory + "\\media\\Beep.wav";
                    SoundPlayer sp = new SoundPlayer(ServerUrl);
                    sp.Play();
                    Thread.Sleep(250);
                    sp.Play();
                    Thread.Sleep(250);
                    sp.Play();
                    //MessageBox_Form.Even_MessageBox barcodeMsg = new MessageBox_Form.Even_MessageBox();
                    //barcodeMsg.Show();
                    MessageBox.Show("條碼錯誤。");
                    
                }
                textBox1.Clear();
                textBox1.Focus();
                defaultAmountTextBox.Text = "1";
            }
        }       
        private void buttonBox_Click(object sender, EventArgs e)
        {
            TempOrderInfo.OrderShipDate = dateTimePicker1.Text;
            TempOrderInfo.OrderNote = textBoxNote.Text;
            if (comboBoxPayment.Text == "到付")
            {
                TempOrderInfo.OrderPayment = "21";
            }else if(comboBoxPayment.Text == "元付")
            {
                TempOrderInfo.OrderPayment = "11";
            }

           A3_Box_Confirm_Form BC = new A3_Box_Confirm_Form(TempOrderInfo);
            try
            {
                //*20170928 封箱時揀貨時間回存
                TimeSpan ts = new TimeSpan(0, 0, second);
                string SpendTime = (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds;
                PickupA A = DB.PickupAs.Find(TempOrderInfo.Order);
                A.OrderSpendTime = SpendTime;
                DB.SaveChanges();
                this.Close();
                BC.Show();
            }
            catch { };

        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow Item in dataGridView1.Rows)
            {
                if (Item.Cells["實揀"].Value.ToString() != "0" && Item.Cells["應揀"].Value != Item.Cells["實揀"].Value)
                {
                    dataGridView1.Rows[Item.Index].DefaultCellStyle.BackColor = Color.Pink;
                }

            }
        }
        private void buttonFinish_Click(object sender, EventArgs e)
        {
            TomLibrary TOM = new TomLibrary();
            SalesInputSQLstring SQL = new SalesInputSQLstring();
            TOM.SQLConnectionString = SQL.FILASQLConnectionString;
            A3_Box_Confirm_Form BC = new A3_Box_Confirm_Form(TempOrderInfo);
            this.Close();
            try
            {
                BC.Show();
            }
            catch { }
            DialogResult dialogResult = MessageBox.Show("確認是否揀貨完畢，最後也請記得封箱。", "完成確認", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<PickupB> SumList = DB.PickupBs.Where(S => S.OrderID == TempOrderInfo.Order).ToList();
                long Must = SumList.Sum(s => s.Order_Amount).Value;
                long Real = SumList.Sum(s => s.Order_PickAmount).Value;
                long Difference = Must - Real;
                //*20170928 完成單據時揀貨時間回存
                TimeSpan ts = new TimeSpan(0, 0, second);
                string SpendTime  = (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds ;
                PickupA A = DB.PickupAs.Find(TempOrderInfo.Order);
                A.OrderSpendTime = SpendTime;
                A.OrderDifference = Difference;
                A.OrderSate = "Y";
                DB.SaveChanges();
                TOM.SQLUpdateData(SQL.PickupFinish, TempOrderInfo.Order, "Y", Difference.ToString(),A.OrderSpendTime); //更新單據狀態及差異量

                foreach (var item in SumList)
                {
                    TOM.SQLUpdateData(SQL.PickupB_Update, TempOrderInfo.Order, item.Order_Barcode, item.Order_PickAmount.ToString()); //儲存差異量
                }
                MessageBox.Show("此張單據已結案。");
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Keyboard k = new Keyboard();

            k.TB.Text = textBox1.Text;

            if (k.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = k.TextBoxMsg;//從form2取值設定到form1
            }
        }
        private void timerRecord_Tick(object sender, EventArgs e)
        {
            second++;
            //TimeSpan ts = new TimeSpan(0, 0, second);
            //label1.Text = (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds ;
        }
        private void button_OpenBox_Click(object sender, EventArgs e)
        {
            TempOrderInfo.OrderNote = textBoxNote.Text;
            TempOrderInfo.OrderShipDate = dateTimePicker1.Text;
             A5_OpenBoxList openBoxList = new A5_OpenBoxList(TempOrderInfo);
            this.Close();
            openBoxList.Show();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }
    }
}
