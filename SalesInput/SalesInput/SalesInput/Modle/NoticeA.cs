//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesInput.Modle
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoticeA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeA()
        {
            this.NoticeBs = new HashSet<NoticeB>();
        }
    
        public long ID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Require { get; set; }
        public string Response { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public string ERPMaping { get; set; }
        public string NoticeMaping { get; set; }
        public string WorkState { get; set; }
        public string OrderState { get; set; }
        public string shipOrder { get; set; }
        public string shipImg { get; set; }
        public string shipSN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeB> NoticeBs { get; set; }
    }
}
