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
    
    public partial class RSTN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSTN()
        {
            this.RSTPs = new HashSet<RSTP>();
        }
    
        public string BSNCR { get; set; }
        public string BSCLR { get; set; }
        public string BSYMM { get; set; }
        public string BSFLG { get; set; }
        public Nullable<int> BSQTA { get; set; }
        public Nullable<decimal> BSUPA { get; set; }
        public Nullable<int> BSQT1 { get; set; }
        public Nullable<decimal> BSUP1 { get; set; }
        public Nullable<double> BSQ1X { get; set; }
        public Nullable<int> BSQT2 { get; set; }
        public Nullable<decimal> BSUP2 { get; set; }
        public Nullable<double> BSQTC { get; set; }
        public Nullable<double> BSUPC { get; set; }
        public Nullable<double> BSUPZ { get; set; }
        public Nullable<int> BSQT3 { get; set; }
        public Nullable<decimal> BSUP3 { get; set; }
        public Nullable<int> BSQT4 { get; set; }
        public Nullable<decimal> BSUP4 { get; set; }
        public Nullable<double> BSQTD { get; set; }
        public Nullable<double> BSUPD { get; set; }
        public Nullable<int> BSQT5 { get; set; }
        public Nullable<decimal> BSUP5 { get; set; }
        public Nullable<int> BSQT6 { get; set; }
        public Nullable<decimal> BSUP6 { get; set; }
        public Nullable<int> BSQ55 { get; set; }
        public Nullable<decimal> BSU55 { get; set; }
        public Nullable<int> BSQ56 { get; set; }
        public Nullable<decimal> BSU56 { get; set; }
        public Nullable<int> BSQT7 { get; set; }
        public Nullable<decimal> BSUP7 { get; set; }
        public Nullable<int> BSQT8 { get; set; }
        public Nullable<decimal> BSUP8 { get; set; }
        public Nullable<double> BSQTF { get; set; }
        public Nullable<double> BSUPF { get; set; }
        public Nullable<double> BSQTG { get; set; }
        public Nullable<double> BSUPG { get; set; }
        public Nullable<double> BSQTK { get; set; }
        public Nullable<double> BSUPK { get; set; }
        public Nullable<double> BSQTL { get; set; }
        public Nullable<double> BSUPL { get; set; }
        public Nullable<decimal> BSUPX { get; set; }
        public Nullable<int> BSQTN { get; set; }
        public Nullable<decimal> BSUPN { get; set; }
        public Nullable<decimal> BSAVG { get; set; }
        public Nullable<double> BSQWA { get; set; }
        public Nullable<double> BSUWA { get; set; }
        public Nullable<double> BSQWW { get; set; }
        public Nullable<double> BSUWW { get; set; }
        public Nullable<double> BSQWX { get; set; }
        public Nullable<double> BSUWX { get; set; }
        public Nullable<double> BSQWY { get; set; }
        public Nullable<double> BSUWY { get; set; }
        public Nullable<double> BSUWD { get; set; }
        public Nullable<double> BSQWN { get; set; }
        public Nullable<double> BSUWN { get; set; }
        public Nullable<double> BSQCA { get; set; }
        public Nullable<double> BSQCC { get; set; }
        public Nullable<double> BSQCH { get; set; }
        public Nullable<double> BSQCI { get; set; }
        public Nullable<double> BSQCJ { get; set; }
        public Nullable<double> BSQCN { get; set; }
        public Nullable<double> BSYDQ { get; set; }
        public Nullable<double> BSYDP { get; set; }
    
        public virtual BSTL BSTL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSTP> RSTPs { get; set; }
    }
}
