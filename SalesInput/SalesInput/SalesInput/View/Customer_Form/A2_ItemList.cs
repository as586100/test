using SalesInput.Librarys;
using SalesInput.SqlString;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput.View.Customer_Form
{
    public partial class A2_ItemList : Form
    {
        public A2_ItemList(string ID)
        {
            InitializeComponent();
              try
            {
                TomLibrary TOM = new TomLibrary();
                SalesInputSQLstring SQL = new SalesInputSQLstring();
                TOM.SQLConnectionString = SQL.FILASQLConnectionString;
                string SqlString = SQL.NoticeBDetail;
                SqlString += "'" + ID + "'";
                DataTable Table = TOM.SQLGetData(SqlString);
                dataGridView1.DataSource = Table;
            }
            catch { MessageBox.Show("網路訊號不良，請移動到訊號良好地方，重新在試。"); }
        
        }
    }
}
