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
    
    public partial class RSPPA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSPPA()
        {
            this.RSPPBs = new HashSet<RSPPB>();
        }
    
        public string RAREN { get; set; }
        public string RANUM { get; set; }
        public string RASHP { get; set; }
        public string RACNS { get; set; }
        public Nullable<System.DateTime> RADAT { get; set; }
        public Nullable<System.DateTime> RADAY { get; set; }
        public string RAMEN { get; set; }
        public string RALCN { get; set; }
        public string RAMNY { get; set; }
        public Nullable<decimal> RARAT { get; set; }
        public Nullable<double> RATAX { get; set; }
        public Nullable<System.DateTime> RADEL { get; set; }
        public string RARM1 { get; set; }
        public string RARM2 { get; set; }
        public string RARM3 { get; set; }
        public string RARM4 { get; set; }
        public string RARMK { get; set; }
        public string RAREQ { get; set; }
        public string RAYMM { get; set; }
        public string RASEA { get; set; }
        public string RAREV { get; set; }
        public string RASEX { get; set; }
        public string RALAB { get; set; }
        public Nullable<double> RAMRK { get; set; }
        public Nullable<double> RADPR { get; set; }
        public Nullable<double> RADPS { get; set; }
        public Nullable<double> RARA2 { get; set; }
        public string RALC1 { get; set; }
        public Nullable<decimal> RAAMT { get; set; }
        public Nullable<double> RAQTY { get; set; }
        public Nullable<double> RABCT { get; set; }
        public string RANNO { get; set; }
        public string RARER { get; set; }
    
        public virtual BCU BCU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSPPB> RSPPBs { get; set; }
    }
}