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
    
    public partial class RIPA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIPA()
        {
            this.RIPBs = new HashSet<RIPB>();
        }
    
        public string RAREN { get; set; }
        public string RANUM { get; set; }
        public string RASHP { get; set; }
        public string RACNS { get; set; }
        public System.DateTime RADAT { get; set; }
        public System.DateTime RADAY { get; set; }
        public string RAMEN { get; set; }
        public string RACS1 { get; set; }
        public string RACS2 { get; set; }
        public Nullable<double> RATOL { get; set; }
        public Nullable<double> RAQTY { get; set; }
        public Nullable<System.DateTime> RADEL { get; set; }
        public string RARM1 { get; set; }
        public string RARM2 { get; set; }
        public string RARM3 { get; set; }
        public string RARM4 { get; set; }
        public string RARMK { get; set; }
        public string RAFLG { get; set; }
        public Nullable<decimal> RATAX { get; set; }
        public string RACMN { get; set; }
        public string RAPAY { get; set; }
        public string RAADR { get; set; }
        public string RAMN2 { get; set; }
        public string RATE2 { get; set; }
    
        public virtual BCU BCU { get; set; }
        public virtual BCU BCU1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIPB> RIPBs { get; set; }
    }
}