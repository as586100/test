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
    
    public partial class RSPA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSPA()
        {
            this.RSPBs = new HashSet<RSPB>();
        }
    
        public string RANNO { get; set; }
        public string RAREN { get; set; }
        public string RANUM { get; set; }
        public string RASHP { get; set; }
        public string RACNS { get; set; }
        public System.DateTime RADAT { get; set; }
        public System.DateTime RADAY { get; set; }
        public string RAMEN { get; set; }
        public string RALCN { get; set; }
        public string RAMNY { get; set; }
        public double RARAT { get; set; }
        public string RACS1 { get; set; }
        public double RATAX { get; set; }
        public Nullable<System.DateTime> RADEL { get; set; }
        public string RARM1 { get; set; }
        public string RARM2 { get; set; }
        public string RARM3 { get; set; }
        public string RARM4 { get; set; }
        public string RARMK { get; set; }
        public string RAREQ { get; set; }
        public string RAREV { get; set; }
        public string RADEV { get; set; }
        public string RADNM { get; set; }
        public string RASAL { get; set; }
        public string TRUSR { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string TRMOD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSPB> RSPBs { get; set; }
    }
}