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
    
    public partial class BSTN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BSTN()
        {
            this.BSTLs = new HashSet<BSTL>();
        }
    
        public string BSNCR { get; set; }
        public string BSNAM { get; set; }
        public string BSENM { get; set; }
        public string BSLAB { get; set; }
        public string BSCLA { get; set; }
        public string BSCLS { get; set; }
        public string BSGLA { get; set; }
        public string BSGLB { get; set; }
        public string BSYEA { get; set; }
        public string BSSEA { get; set; }
        public string BSOYA { get; set; }
        public string BSOSA { get; set; }
        public string BSSEX { get; set; }
        public string BSUPS { get; set; }
        public string BSWAY { get; set; }
        public Nullable<double> BSLEV { get; set; }
        public Nullable<double> BSHEV { get; set; }
        public string BSUNI { get; set; }
        public Nullable<double> BSUPC { get; set; }
        public Nullable<double> BSAVG { get; set; }
        public Nullable<double> BSSVG { get; set; }
        public string BSCS2 { get; set; }
        public Nullable<double> BSDEP { get; set; }
        public Nullable<double> BSDEB { get; set; }
        public Nullable<double> BSDEC { get; set; }
        public Nullable<double> BSDED { get; set; }
        public Nullable<double> BSEEP { get; set; }
        public Nullable<double> BSEEB { get; set; }
        public Nullable<double> BSEEC { get; set; }
        public Nullable<double> BSEED { get; set; }
        public string BSCUS { get; set; }
        public Nullable<System.DateTime> BSDT4 { get; set; }
        public Nullable<System.DateTime> BSDT5 { get; set; }
        public string BSRMK { get; set; }
        public Nullable<double> BSFOR { get; set; }
        public string BSMNY { get; set; }
        public Nullable<double> BSTIM { get; set; }
        public string BSNOT { get; set; }
        public string BSFLG { get; set; }
        public Nullable<double> BSUPF { get; set; }
        public string BSFAC { get; set; }
        public Nullable<double> BSQTI { get; set; }
        public string BSGL1 { get; set; }
        public string BSGL2 { get; set; }
        public string BSGL3 { get; set; }
        public string BSGL4 { get; set; }
        public string BSGL5 { get; set; }
        public string BSTAG { get; set; }
        public string BSDN1 { get; set; }
        public string BSDC1 { get; set; }
        public string BSDN2 { get; set; }
        public string BSDC2 { get; set; }
        public string BSDA1 { get; set; }
        public string BSDA2 { get; set; }
        public string BSDA3 { get; set; }
        public string BSDA4 { get; set; }
        public string BSDA5 { get; set; }
        public string BSDA6 { get; set; }
        public string BSSWG { get; set; }
        public string BSDB4 { get; set; }
        public string BSDB1 { get; set; }
        public string BSDB2 { get; set; }
        public string BSDB3 { get; set; }
        public string BSDB5 { get; set; }
        public string BSDB6 { get; set; }
        public string BSDB7 { get; set; }
        public string BSDB8 { get; set; }
        public string BSbatch { get; set; }
        public string BSDLE { get; set; }
        public string BScheck { get; set; }
        public string BSTYN { get; set; }
        public string BSMYM { get; set; }
        public string BSONO { get; set; }
        public Nullable<System.DateTime> BSUDT { get; set; }
        public string BSASI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BSTL> BSTLs { get; set; }
        public virtual BSTXA BSTXA { get; set; }
    }
}