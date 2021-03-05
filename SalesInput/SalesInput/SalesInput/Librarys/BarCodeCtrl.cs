using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesInput.Librarys
{
    class BarCodeCtrl
    {
        /// <summary>
        /// 開啟條碼機
        /// </summary>
        /// <param name="PrinterName">條碼機名型號</param>
        [DllImport("TSCLIB.dll")]
        public static extern void openport(string PrinterName);

        /// <summary>
        /// 關閉條碼機
        /// </summary>
        [DllImport("TSCLIB.dll")]
        public static extern void closeport();

        /// <summary>
        /// 送內建指令到條碼印表機
        /// </summary>
        /// <param name="command"></param>
        [DllImport("TSCLIB.dll")]
        public static extern void sendcommand(string command);

        /// <summary>
        /// 條碼機設定
        /// </summary>
        /// <param name="LabelWidth">標籤寬度，單位mm</param>
        /// <param name="LabelHeight">標籤高度，單位mm</param>
        /// <param name="Speed">速度：1.0->每秒1.0吋, 1.5->每秒1.5吋, 2.0->每秒2.0吋, 3.0->每秒3.0吋, 4.0->每秒4.0吋, 5.0->每秒5.0吋, 6.0->每秒6.0吋</param>
        /// <param name="Density">濃度：0~15，數字愈大列印結果愈黑</param>
        /// <param name="Sensor">設定使用感應器類別：0->表示使用垂直間距感測器(gap sensor), 1->表示使用黑標感測器(black mark sensor)</param>
        /// <param name="Vertical">設定gap/black mark 垂直間距高度，單位mm</param>
        /// <param name="Offset">設定gap/black mark 偏移距離，單位mm，此參數若使用一般標籤時均設為0</param>
        [DllImport("TSCLIB.dll")]
        public static extern void setup(string LabelWidth, string LabelHeight, string Speed, string Density, string Sensor, string Vertical, string Offset);

        /// <summary>
        /// 下載單色PCX格式圖檔至印表機
        /// </summary>
        /// <param name="Filename">檔案(可包含路徑)</param>
        /// <param name="ImageName">圖名(請使用大寫檔名)</param>
        [DllImport("TSCLIB.dll")]
        public static extern void downloadpcx(string Filename, string ImageName);

        /// <summary>
        /// 列印條碼
        /// </summary>
        /// <param name="X">x座標：文字X方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="Y">y座標：文字Y方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="CodeType">條碼別：128, 128M, EAN128, 25, 25C, 39, 39C, 93, EAN13, EAN13+2, EAN13+5, EAN8, EAN8+2, EAN8+5, CODA, POST, UPCA, UPCA+2, UPCA+5, UPCE, UPCE+2, UPCE+5</param>
        /// <param name="Height">條碼高度，高度以點(point)表示</param>
        /// <param name="Readable">是否列印條碼碼文：0->不列印碼文, 1->列印碼文</param>
        /// <param name="rotation">旋轉角度，逆時鐘方向旋轉。如：0->0 degree, 90->90 degree, 180->180 degree, 270->270 degree</param>
        /// <param name="Narrow">比例</param>
        /// <param name="Wide">寬度</param>
        /// <param name="Code">條碼內容</param>
        [DllImport("TSCLIB.dll")]
        public static extern void barcode(string X, string Y, string CodeType, string Height, string Readable, string rotation, string Narrow, string Wide, string Code);

        /// <summary>
        /// 使用條碼機內建文字列印
        /// </summary>
        /// <param name="X">x座標：文字X方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="Y">y座標：文字Y方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="FontName">內建字型名稱，共12種：1->8*12 dots, 2->12*20 dots, 3->16*24 dots, 4->24*32 dots, 5->32*48 dots, TST24.BF2->繁體中文24*24, TST16.BF2->繁體中文16*16, TTT24.BF2->繁體中文24*24(電信碼), TSS24.BF2->簡體中24*24, TSS16.BF2->簡體中文16*16, K->韓文24*24, L->韓文16*16</param>
        /// <param name="rotation">旋轉角度，逆時鐘方向旋轉。如：0->0 degree, 90->90 degree, 180->180 degree, 270->270 degree</param>
        /// <param name="Xmul">設定文字X方向放大倍率，1~8</param>
        /// <param name="Ymul">設定文字Y方向放大倍率，1~8</param>
        /// <param name="Content">列印文字內容</param>
        [DllImport("TSCLIB.dll")]
        public static extern void printerfont(string X, string Y, string FontName, string rotation, string Xmul, string Ymul, string Content);

        /// <summary>
        /// 清除緩衝區
        /// </summary>
        [DllImport("TSCLIB.dll")]
        public static extern void clearbuffer();

        /// <summary>
        /// 開始列印條碼
        /// </summary>
        /// <param name="NumberOfSet">列印數量</param>
        /// <param name="NumberOfCopy">複製數量</param>
        [DllImport("TSCLIB.dll")]
        public static extern void printlabel(string NumberOfSet, string NumberOfCopy);

        /// <summary>
        /// 跳頁，該函式需在setup後使用
        /// </summary>
        [DllImport("TSCLIB.dll")]
        public static extern void formfeed();
        /// <summary>
        /// 設定紙張不回吐
        /// </summary>
        [DllImport("TSCLIB.dll")]
        public static extern void nobackfeed();

        /// <summary>
        /// 設定條碼文字
        /// </summary>
        /// <param name="X">x座標：文字X方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="Y">y座標：文字Y方向起始點，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="fontheight">字體高度，以點(point)表示。(200DPI，1點=1/8mm，300DPI，1點=1/12mm)</param>
        /// <param name="rotation">旋轉角度，逆時鐘方向旋轉。如：0->0 degree, 90->90 degree, 180->180 degree, 270->270 degree</param>
        /// <param name="fontstyle">字體外型。如： 0->標準(Normal), 1->斜體(Italic), 2->粗體(Bold), 3->粗斜體(Bold and Italic)</param>
        /// <param name="fontunderline">底線。如：0->無底線, 1->有底線</param>
        /// <param name="FaceName">字體名稱。如：Arial, Times new Roman, 細明體, 標楷體</param>
        /// <param name="TextContent">列印文字內容</param>
        [DllImport("TSCLIB.dll")]
        public static extern void windowsfont(int X, int Y, int fontheight, int rotation, int fontstyle, int fontunderline, string FaceName, string TextContent);
    }
}
