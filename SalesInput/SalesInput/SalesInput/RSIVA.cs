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
    
    public partial class RSIVA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSIVA()
        {
            this.RSIVBs = new HashSet<RSIVB>();
        }
    
        public string RAREN { get; set; }
        public string RACNS { get; set; }
        public string RACN2 { get; set; }
        public string RAOEN { get; set; }
        public string RANUM { get; set; }
        public string RASHP { get; set; }
        public string RACLS { get; set; }
        public string RACTS { get; set; }
        public Nullable<System.DateTime> RADAY { get; set; }
        public Nullable<System.DateTime> RADAV { get; set; }
        public string RAINV { get; set; }
        public string RARMA { get; set; }
        public string RAMEN { get; set; }
        public string RASAL { get; set; }
        public Nullable<System.DateTime> RAEDT { get; set; }
        public string RACSR { get; set; }
        public string RARER { get; set; }
        public string RACAR { get; set; }
        public string RACS1 { get; set; }
        public string RACS2 { get; set; }
        public Nullable<double> RATAX { get; set; }
        public Nullable<double> RATOL { get; set; }
        public string RAVIP { get; set; }
        public string RAMN3 { get; set; }
        public string RAADR { get; set; }
        public Nullable<double> RAAMR { get; set; }
        public string RATEL { get; set; }
        public Nullable<System.DateTime> RASDT { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string TRMOD { get; set; }
        public string TRUSR { get; set; }
        public Nullable<System.DateTime> RAXDT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIVB> RSIVBs { get; set; }
    }
}
