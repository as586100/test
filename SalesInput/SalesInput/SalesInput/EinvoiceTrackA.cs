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
    
    public partial class EinvoiceTrackA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EinvoiceTrackA()
        {
            this.EinvoiceTrackBs = new HashSet<EinvoiceTrackB>();
        }
    
        public string RAREN { get; set; }
        public string UnifiedBusinessNo { get; set; }
        public string RACS1 { get; set; }
        public string RAYMM { get; set; }
        public string InvoiceWordTrack { get; set; }
        public string BeginNo { get; set; }
        public string EndNo { get; set; }
        public int InvoiceBooklet { get; set; }
        public string RAMEN { get; set; }
        public Nullable<System.DateTime> RAXDT { get; set; }
        public string TRUSR { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public Nullable<System.DateTime> BranchTrackTranDateTime { get; set; }
        public Nullable<System.DateTime> BlankTranDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EinvoiceTrackB> EinvoiceTrackBs { get; set; }
    }
}
