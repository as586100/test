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
    
    public partial class RSVA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSVA()
        {
            this.RSVBs = new HashSet<RSVB>();
        }
    
        public string RAREN { get; set; }
        public System.DateTime RADAT { get; set; }
        public System.DateTime RADAY { get; set; }
        public string RAMEN { get; set; }
        public Nullable<System.DateTime> RADA2 { get; set; }
        public string RAME2 { get; set; }
        public string TRUSR { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string PTRUSR { get; set; }
        public Nullable<System.DateTime> PTRDAT { get; set; }
        public string PKSRC { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public string IsDownloaded { get; set; }
        public string RACTS { get; set; }
        public string RARER { get; set; }
        public string RTSAcalNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSVB> RSVBs { get; set; }
    }
}
