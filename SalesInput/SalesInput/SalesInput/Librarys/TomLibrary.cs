using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesInput.Modle;

namespace SalesInput.Librarys
{
    class TomLibrary
    {
        public string SQLConnectionString
        {
            get;
            set;
        }

        public DataTable SQLGetData(string cmd)
        {
            SqlConnection Sql_conn = new SqlConnection(this.SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            SqlDataAdapter sa = new SqlDataAdapter(Sql_cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            Sql_conn.Close();
            return dt;
        }

        public void SQLUpdateData(string cmd, string a,string b)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.ExecuteNonQuery();
            Sql_conn.Close();
           
        }
        public void SQLUpdateData(string cmd, string a, string b, string c)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@c", c);
            Sql_cmd.ExecuteNonQuery();
            Sql_conn.Close();
        }
        public void SQLUpdateData(string cmd, string a, string b, string c,string d,string e)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@c", c);
            Sql_cmd.Parameters.Add("@d", d);
            Sql_cmd.Parameters.Add("@e", e);
            Sql_cmd.ExecuteNonQuery();
            Sql_conn.Close();
        }
        public void SQLUpdateData(string cmd, string a, string b, string c,string d)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@c", c);
            Sql_cmd.Parameters.Add("@d", d);
            Sql_cmd.ExecuteNonQuery();
            Sql_conn.Close();

        }

        public void SQLUpdateData(string cmd, string a, string b, string c, string d, string e, string f, string g, string h, string i, string j, string k, string l, string m)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@c", c);
            Sql_cmd.Parameters.Add("@d", d);
            Sql_cmd.Parameters.Add("@e", e);
            Sql_cmd.Parameters.Add("@f", f);
            Sql_cmd.Parameters.Add("@g", g);
            Sql_cmd.Parameters.Add("@h", h);
            Sql_cmd.Parameters.Add("@i", i);
            Sql_cmd.Parameters.Add("@j", j);
            Sql_cmd.Parameters.Add("@k", k);
            Sql_cmd.Parameters.Add("@l", l);
            Sql_cmd.Parameters.Add("@m", m);
            Sql_cmd.ExecuteNonQuery();
            Sql_conn.Close();

        }




        public DataTable SQLGetData(string cmd, string a)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            
            SqlDataAdapter sa = new SqlDataAdapter(Sql_cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            Sql_conn.Close();
            return (dt);
        }

        //cmd=>cmdText a=>account、b=>password參數可以更改為有意義的參數名稱
        public DataTable SQLGetData(string cmd, string a, string b)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            SqlDataAdapter sa = new SqlDataAdapter(Sql_cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            Sql_conn.Close();
            return (dt);
        }
        public DataTable SQLGetData(string cmd, string a, string b, string c)
        {
            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.CommandTimeout = 0;
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@C", c);
            SqlDataAdapter sa = new SqlDataAdapter(Sql_cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            Sql_conn.Close();
            return (dt);
        }
        public DataTable SQLGetData(string cmd, string a, string b, string c, string d, string e)
        {

            SqlConnection Sql_conn = new SqlConnection(SQLConnectionString);
            Sql_conn.Open();
            SqlCommand Sql_cmd = new SqlCommand(cmd, Sql_conn);
            Sql_cmd.CommandTimeout = 0;
            Sql_cmd.Parameters.Add("@a", a);
            Sql_cmd.Parameters.Add("@b", b);
            Sql_cmd.Parameters.Add("@c", c);
            Sql_cmd.Parameters.Add("@d", c);
            Sql_cmd.Parameters.Add("@e", c);
            SqlDataAdapter sa = new SqlDataAdapter(Sql_cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            Sql_conn.Close();
            return (dt);
        }

        public void DataTableToExcelFile(DataTable dt, string Dir, string FileName)
        {
            IWorkbook wb = new HSSFWorkbook();
            bool flag = dt.TableName != string.Empty;
            ISheet ws;
            if (flag)
            {
                ws = wb.CreateSheet(dt.TableName);
            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }
            ws.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ws.CreateRow(j + 1);
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    ws.GetRow(j + 1).CreateCell(k).SetCellValue(dt.Rows[j][k].ToString());
                }
            }
            FileStream file = new FileStream(Dir + "\\" + FileName + ".xls", FileMode.Create);
            wb.Write(file);
            file.Close();
        }
        public void DataTableToExcelFile(DataTable dt, string FileName)
        {
            IWorkbook wb = new HSSFWorkbook();
            bool flag = dt.TableName != string.Empty;
            ISheet ws;
            if (flag)
            {
                ws = wb.CreateSheet(dt.TableName);
            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }
            ws.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.GetRow(0).CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ws.CreateRow(j + 1);
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    ws.GetRow(j + 1).CreateCell(k).SetCellValue(dt.Rows[j][k].ToString());
                }
            }
            FileStream file = new FileStream(FileName, FileMode.Create);
            wb.Write(file);
            file.Close();
        }

        public void ListToExcelFile(List<PickupB> b, string Dir, string FileName)
        {
          
            IWorkbook wb = new HSSFWorkbook();
            bool flag =b.Count != 0;
            ISheet ws;
            if (flag)
            {
                ws = wb.CreateSheet("差異表");
            }
            else
            {
                ws = wb.CreateSheet("Sheet1");
            }
            ws.CreateRow(0);
            int row = 1;

          
            ws.GetRow(0).CreateCell(0).SetCellValue("櫃號");//櫃號
            ws.GetRow(0).CreateCell(1).SetCellValue("條碼");//條碼
            ws.GetRow(0).CreateCell(2).SetCellValue("型號");//型號
            ws.GetRow(0).CreateCell(3).SetCellValue("顏色");//顏色
            ws.GetRow(0).CreateCell(4).SetCellValue("尺寸");//尺寸
            ws.GetRow(0).CreateCell(5).SetCellValue("應揀");//應揀
            ws.GetRow(0).CreateCell(6).SetCellValue("實揀");//實揀
            ws.GetRow(0).CreateCell(7).SetCellValue("差異量");//儲位
            ws.GetRow(0).CreateCell(8).SetCellValue("儲位");//儲位
            ws.GetRow(0).CreateCell(9).SetCellValue("儲位2");//儲位2

            foreach (var item in b)
            {
                ws.CreateRow(row);
                ws.GetRow(row).CreateCell(0).SetCellValue(item.Order_Store);//櫃號
                ws.GetRow(row).CreateCell(1).SetCellValue(item.Order_Barcode);//條碼
                ws.GetRow(row).CreateCell(2).SetCellValue(item.Order_Type);//型號
                ws.GetRow(row).CreateCell(3).SetCellValue(item.Order_Color);//顏色
                ws.GetRow(row).CreateCell(4).SetCellValue(item.Order_Size);//尺寸
                ws.GetRow(row).CreateCell(5).SetCellValue(item.Order_Amount.ToString());//應揀
                ws.GetRow(row).CreateCell(6).SetCellValue(item.Order_PickAmount.ToString());//實揀
                ws.GetRow(row).CreateCell(7).SetCellValue((item.Order_Amount- item.Order_PickAmount).ToString());//差異量
                ws.GetRow(row).CreateCell(8).SetCellValue(item.Order_Location);//儲位
                ws.GetRow(row).CreateCell(9).SetCellValue(item.Order_Location2);//儲位2
                row++;
            }


            FileStream file = new FileStream(Dir + "\\" + FileName + ".xls", FileMode.Create);
            wb.Write(file);
            file.Close();
        }

    }
}
