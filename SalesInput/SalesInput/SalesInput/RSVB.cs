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
    
    public partial class RSVB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RSVB()
        {
            this.RSVCs = new HashSet<RSVC>();
        }
    
        public string RBREN { get; set; }
        public string RBITM { get; set; }
        public string RBNCR { get; set; }
        public string RBCLR { get; set; }
        public string RBCLZ { get; set; }
        public Nullable<double> RBQTY { get; set; }
        public Nullable<double> RBQTY01 { get; set; }
        public Nullable<double> RBQTY02 { get; set; }
        public Nullable<double> RBQTY03 { get; set; }
        public Nullable<double> RBQTY04 { get; set; }
        public Nullable<double> RBQTY05 { get; set; }
        public Nullable<double> RBQTY06 { get; set; }
        public Nullable<double> RBQTY07 { get; set; }
        public Nullable<double> RBQTY08 { get; set; }
        public Nullable<double> RBQTY09 { get; set; }
        public Nullable<double> RBQTY10 { get; set; }
        public Nullable<double> RBQTY11 { get; set; }
        public Nullable<double> RBQTY12 { get; set; }
        public Nullable<double> RBQTY13 { get; set; }
        public Nullable<double> RBQTY14 { get; set; }
        public Nullable<double> RBQTY15 { get; set; }
        public Nullable<double> RBQTY16 { get; set; }
        public Nullable<double> RBQTY17 { get; set; }
        public Nullable<double> RBQTY18 { get; set; }
        public Nullable<double> RBQTY19 { get; set; }
        public Nullable<double> RBQTY20 { get; set; }
        public Nullable<double> RBPQY { get; set; }
        public Nullable<double> RBPQY01 { get; set; }
        public Nullable<double> RBPQY02 { get; set; }
        public Nullable<double> RBPQY03 { get; set; }
        public Nullable<double> RBPQY04 { get; set; }
        public Nullable<double> RBPQY05 { get; set; }
        public Nullable<double> RBPQY06 { get; set; }
        public Nullable<double> RBPQY07 { get; set; }
        public Nullable<double> RBPQY08 { get; set; }
        public Nullable<double> RBPQY09 { get; set; }
        public Nullable<double> RBPQY10 { get; set; }
        public Nullable<double> RBPQY11 { get; set; }
        public Nullable<double> RBPQY12 { get; set; }
        public Nullable<double> RBPQY13 { get; set; }
        public Nullable<double> RBPQY14 { get; set; }
        public Nullable<double> RBPQY15 { get; set; }
        public Nullable<double> RBPQY16 { get; set; }
        public Nullable<double> RBPQY17 { get; set; }
        public Nullable<double> RBPQY18 { get; set; }
        public Nullable<double> RBPQY19 { get; set; }
        public Nullable<double> RBPQY20 { get; set; }
        public Nullable<double> RBQTY21 { get; set; }
        public Nullable<double> RBQTY22 { get; set; }
        public Nullable<double> RBQTY23 { get; set; }
        public Nullable<double> RBQTY24 { get; set; }
        public Nullable<double> RBQTY25 { get; set; }
        public Nullable<double> RBQTY26 { get; set; }
        public Nullable<double> RBQTY27 { get; set; }
        public Nullable<double> RBQTY28 { get; set; }
        public Nullable<double> RBQTY29 { get; set; }
        public Nullable<double> RBQTY30 { get; set; }
        public Nullable<double> RBPQY21 { get; set; }
        public Nullable<double> RBPQY22 { get; set; }
        public Nullable<double> RBPQY23 { get; set; }
        public Nullable<double> RBPQY24 { get; set; }
        public Nullable<double> RBPQY25 { get; set; }
        public Nullable<double> RBPQY26 { get; set; }
        public Nullable<double> RBPQY27 { get; set; }
        public Nullable<double> RBPQY28 { get; set; }
        public Nullable<double> RBPQY29 { get; set; }
        public Nullable<double> RBPQY30 { get; set; }
        public string RBCNS { get; set; }
        public string RBRER { get; set; }
        public string RBIEM { get; set; }
    
        public virtual RSVA RSVA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSVC> RSVCs { get; set; }
    }
}
