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
    
    public partial class RSDA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSDA()
        {
            this.RSDBs = new HashSet<RSDB>();
        }
    
        public string RANUM { get; set; }
        public string RAFNU { get; set; }
        public string RACTS { get; set; }
        public string RATYP { get; set; }
        public string RAYMM { get; set; }
        public string RAREN { get; set; }
        public string RAADR { get; set; }
        public string BAREN { get; set; }
        public Nullable<System.DateTime> RADAT { get; set; }
        public string RAREV { get; set; }
        public string RAREX { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSDB> RSDBs { get; set; }
    }
}