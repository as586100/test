//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesInput
{
    using System;
    using System.Collections.Generic;
    
    public partial class BACSETB
    {
        public string BBNUM { get; set; }
        public string BBITM { get; set; }
        public string BBNAM { get; set; }
        public string BBFDC { get; set; }
        public string BBFG1 { get; set; }
        public string BBACC { get; set; }
        public string BBFDP { get; set; }
        public string BBFNU { get; set; }
        public string BBRMK { get; set; }
    
        public virtual BACSETA BACSETA { get; set; }
    }
}
