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
    
    public partial class BSTL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BSTL()
        {
            this.BSTOes = new HashSet<BSTO>();
            this.ProductClassInPOS = new HashSet<ProductClassInPOS>();
            this.RSIABs = new HashSet<RSIAB>();
            this.RSICBs = new HashSet<RSICB>();
            this.RSIDBs = new HashSet<RSIDB>();
            this.RSIFBs = new HashSet<RSIFB>();
            this.RSIIBs = new HashSet<RSIIB>();
            this.RSIMBs = new HashSet<RSIMB>();
            this.RSIRBs = new HashSet<RSIRB>();
            this.RSIVBs = new HashSet<RSIVB>();
            this.RSIYBs = new HashSet<RSIYB>();
            this.RSIZBs = new HashSet<RSIZB>();
            this.RSRMBs = new HashSet<RSRMB>();
            this.RSTNs = new HashSet<RSTN>();
            this.SetBSPOS = new HashSet<SetBSPOS>();
        }
    
        public string BSNCR { get; set; }
        public string BSCLR { get; set; }
        public string BSOCR { get; set; }
        public string BSONR { get; set; }
        public string BSMAT { get; set; }
        public Nullable<decimal> BSUPC { get; set; }
        public Nullable<decimal> BSAVG { get; set; }
        public Nullable<double> BSSVG { get; set; }
        public Nullable<decimal> BSDEP { get; set; }
        public Nullable<decimal> BSDEB { get; set; }
        public Nullable<decimal> BSDEC { get; set; }
        public Nullable<decimal> BSDED { get; set; }
        public Nullable<decimal> BSEEP { get; set; }
        public Nullable<decimal> BSEEB { get; set; }
        public Nullable<decimal> BSEEC { get; set; }
        public Nullable<decimal> BSEED { get; set; }
        public string BSPIC { get; set; }
        public Nullable<System.DateTime> BSDT4 { get; set; }
        public Nullable<System.DateTime> BSDT5 { get; set; }
        public Nullable<double> BSFOR { get; set; }
        public string BSMNY { get; set; }
        public Nullable<double> BSTIM { get; set; }
        public Nullable<double> BSLEV { get; set; }
        public Nullable<double> BSHEV { get; set; }
        public Nullable<double> BSQTI { get; set; }
        public string BSMA1 { get; set; }
        public string BSMA2 { get; set; }
        public string BSYEA { get; set; }
        public string BSSEA { get; set; }
        public string BSOYA { get; set; }
        public string BSOSA { get; set; }
        public string BSDT6 { get; set; }
        public string BSDT7 { get; set; }
        public string BSPS3 { get; set; }
        public string BSPS4 { get; set; }
        public Nullable<System.DateTime> BSDT8 { get; set; }
        public Nullable<double> BSPER { get; set; }
        public string BSOBC { get; set; }
        public string BSIBC { get; set; }
        public Nullable<double> BSMOC { get; set; }
        public Nullable<double> BSGAC { get; set; }
        public string BSSHL { get; set; }
        public string BSMA3 { get; set; }
        public string BSMA4 { get; set; }
    
        public virtual BSTN BSTN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BSTO> BSTOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductClassInPOS> ProductClassInPOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIAB> RSIABs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSICB> RSICBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIDB> RSIDBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIFB> RSIFBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIIB> RSIIBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIMB> RSIMBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIRB> RSIRBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIVB> RSIVBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIYB> RSIYBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSIZB> RSIZBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSRMB> RSRMBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSTN> RSTNs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SetBSPOS> SetBSPOS { get; set; }
    }
}