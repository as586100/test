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
    
    public partial class DailyWorkD
    {
        public string RDREN { get; set; }
        public string RBITM { get; set; }
        public string RDITM { get; set; }
        public string RDMEN { get; set; }
        public Nullable<decimal> RDAMT { get; set; }
        public string MainMen { get; set; }
        public Nullable<decimal> oPercent { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string TRUSR { get; set; }
        public string RDRMA { get; set; }
        public Nullable<decimal> RDAMT01 { get; set; }
        public Nullable<decimal> RDAMT02 { get; set; }
        public Nullable<decimal> RDAMT03 { get; set; }
        public Nullable<decimal> RDAMT04 { get; set; }
        public Nullable<decimal> RDAMT05 { get; set; }
        public Nullable<decimal> RDAMT06 { get; set; }
        public Nullable<decimal> RDAMT11 { get; set; }
        public Nullable<decimal> RDAMT12 { get; set; }
        public Nullable<decimal> RDAMT13 { get; set; }
        public Nullable<decimal> RDAMT14 { get; set; }
        public Nullable<decimal> RDAMT15 { get; set; }
        public Nullable<decimal> RDAMT16 { get; set; }
        public Nullable<decimal> RDAMTNeed { get; set; }
        public Nullable<decimal> DrawnAMT { get; set; }
        public Nullable<decimal> CreditcardAMT { get; set; }
    
        public virtual DailyWorkB DailyWorkB { get; set; }
    }
}