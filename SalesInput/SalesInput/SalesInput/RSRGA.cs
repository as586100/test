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
    
    public partial class RSRGA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSRGA()
        {
            this.RSRGBs = new HashSet<RSRGB>();
        }
    
        public string RAREN { get; set; }
        public Nullable<System.DateTime> RADAY { get; set; }
        public Nullable<System.DateTime> RADTA { get; set; }
        public string RARPA { get; set; }
        public Nullable<System.DateTime> RAVDT { get; set; }
        public string RAVUR { get; set; }
        public string RAVKD { get; set; }
        public string RARMK { get; set; }
        public Nullable<System.DateTime> RADAT { get; set; }
        public string RAUSR { get; set; }
        public string RAUDP { get; set; }
        public string TRMOD { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string TRUSR { get; set; }
    
        public virtual BRPA BRPA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSRGB> RSRGBs { get; set; }
    }
}
