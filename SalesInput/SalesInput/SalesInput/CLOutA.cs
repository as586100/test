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
    
    public partial class CLOutA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLOutA()
        {
            this.CLOutBs = new HashSet<CLOutB>();
        }
    
        public string RAREN { get; set; }
        public string RACNS { get; set; }
        public Nullable<System.DateTime> RADAY { get; set; }
        public Nullable<System.DateTime> SentDay { get; set; }
        public string SentPeriod { get; set; }
        public Nullable<int> RAQTY { get; set; }
        public string RAMEN { get; set; }
        public string ReceiveName { get; set; }
        public string ReceivePhone { get; set; }
        public string ReceiveMobile { get; set; }
        public string ReceiveAddress { get; set; }
        public string DeliveryMen { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string AddMen { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public string TRUSR { get; set; }
        public Nullable<System.DateTime> TRDAT { get; set; }
        public string RARER { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLOutB> CLOutBs { get; set; }
    }
}
