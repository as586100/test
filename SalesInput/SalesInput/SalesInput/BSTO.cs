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
    
    public partial class BSTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BSTO()
        {
            this.BSTPs = new HashSet<BSTP>();
            this.BSTP_RSWA = new HashSet<BSTP_RSWA>();
            this.MultiBSONUs = new HashSet<MultiBSONU>();
            this.RSIFBs = new HashSet<RSIFB>();
            this.RSIHBs = new HashSet<RSIHB>();
            this.RSIRBs = new HashSet<RSIRB>();
            this.RTSAcalBs = new HashSet<RTSAcalB>();
        }
    
        public string BSNCR { get; set; }
        public string BSNUM { get; set; }
        public string BSONU { get; set; }
        public string BSNNU { get; set; }
        public string BSCLR { get; set; }
        public string BSITZ { get; set; }
        public string BSSIZ { get; set; }
        public string BSOSZ { get; set; }
        public Nullable<double> BSLEV { get; set; }
        public Nullable<double> BSHEV { get; set; }
        public Nullable<System.DateTime> BSDT1 { get; set; }
        public Nullable<System.DateTime> BSDT2 { get; set; }
        public Nullable<System.DateTime> BSDT3 { get; set; }
        public Nullable<System.DateTime> BSDT4 { get; set; }
        public Nullable<System.DateTime> BSDT5 { get; set; }
        public string BSSPY { get; set; }
        public string BSRMK { get; set; }
        public Nullable<double> BSQTI { get; set; }
        public Nullable<System.DateTime> BSDT6 { get; set; }
        public Nullable<System.DateTime> BSDT7 { get; set; }
        public Nullable<int> BSCLK { get; set; }
    
        public virtual BSTL BSTL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BSTP> BSTPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BSTP_RSWA> BSTP_RSWA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MultiBSONU> MultiBSONUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIFB> RSIFBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIHB> RSIHBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIRB> RSIRBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RTSAcalB> RTSAcalBs { get; set; }
    }
}
