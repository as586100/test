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
    
    public partial class UserAuthority
    {
        public int UserAuthorityKey { get; set; }
        public Nullable<int> extlMenuAKey { get; set; }
        public Nullable<int> extlUserKey { get; set; }
        public Nullable<decimal> Authority { get; set; }
        public string Show { get; set; }
    
        public virtual MenuA MenuA { get; set; }
        public virtual User User { get; set; }
    }
}
