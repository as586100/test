using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInput
{
    static class Program
    {
        //方法一 : 為了使此物件在 Release 模式下，執行完後不會被回收，故設定為靜態物件。
        static Mutex mutexObject = null;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Index_Form());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //try
            //{
                #region 檢查程式是否重複執行

                //在同目錄執行相同程式的情況下不允許重複執行
                string mutexName = Process.GetCurrentProcess().MainModule.FileName
                                          .Replace(Path.DirectorySeparatorChar, '_');


                bool is_createdNew;

                mutexObject = new Mutex(true, "Global\\" + mutexName, out is_createdNew);

                //有重複開啟時
                if (!is_createdNew)
                {
                    MessageBox.Show("程式已正在執行，請稍後.", "警告");
                    return;
                }
                #endregion

                Application.Run(new Index_Form());

            //方法二: GC.KeepAlive(mutexObject);
            //方法三: GC.SuppressFinalize(mutexObject);
        //}
    //        catch (Exception ex)
    //        {

    //            string.Format("ExMsg= {0}\r\nStackTrace= {1}", ex.Message, ex.StackTrace);
    //             Application.Exit();
    //}
}
    }
}
 