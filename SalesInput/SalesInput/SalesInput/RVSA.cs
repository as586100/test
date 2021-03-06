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
    
    public partial class RVSA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RVSA()
        {
            this.RVSBs = new HashSet<RVSB>();
        }
    
        public string RAREN { get; set; }
        public string RACS1 { get; set; }
        public System.DateTime RADAT { get; set; }
        public string RAINV { get; set; }
        public string RACYM { get; set; }
        public string RAICK { get; set; }
        public string RANUM { get; set; }
        public string RANUI { get; set; }
        public string RAINT { get; set; }
        public string RAINA { get; set; }
        public string RACS2 { get; set; }
        public string RACS4 { get; set; }
        public Nullable<double> RAUPC { get; set; }
        public Nullable<double> RATAX { get; set; }
        public Nullable<double> RAAMT { get; set; }
        public string RAA02 { get; set; }
        public Nullable<double> RARED { get; set; }
        public Nullable<double> RARET { get; set; }
        public Nullable<double> RAN01 { get; set; }
        public string RAA01 { get; set; }
        public string RAMNG { get; set; }
        public string RAFGZ { get; set; }
        public Nullable<System.DateTime> RADTG { get; set; }
        public string RACNX { get; set; }
        public string RARMK { get; set; }
        public string RAMEN { get; set; }
        public Nullable<System.DateTime> RADAY { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string CurCountry { get; set; }
        public string CurAddress { get; set; }
        public string CurRoad { get; set; }
        public string CurNeighbor { get; set; }
        public string CurCity { get; set; }
        public string CurTown { get; set; }
        public string CurZipCode { get; set; }
        public string Shipment { get; set; }
        public string AddUser { get; set; }
        public string Memo { get; set; }
        public string Receiver { get; set; }
        public string ReceiverTel { get; set; }
        public string ShipOrgNo { get; set; }
        public Nullable<System.DateTime> SalesDate { get; set; }
        public string IsSendInvoice { get; set; }
        public string INALLOW { get; set; }
        public Nullable<decimal> PrintCount { get; set; }
        public string Source { get; set; }
        public string ReceiverMobileNo { get; set; }
        public string LotNo { get; set; }
        public string RANAM { get; set; }
        public string RAOEN { get; set; }
        public string SellerBan { get; set; }
        public string BuyerBan { get; set; }
        public Nullable<System.DateTime> InvoiceDateTime { get; set; }
        public string CheckNumber { get; set; }
        public string IsEInvoice { get; set; }
        public Nullable<System.DateTime> TranDateTime { get; set; }
        public Nullable<System.DateTime> CancelDateTime { get; set; }
        public string CancelReason { get; set; }
        public string ReturnTaxDocumentNumber { get; set; }
        public Nullable<System.DateTime> TranCancelDateTime { get; set; }
        public string CarrierType { get; set; }
        public string CarrierId1 { get; set; }
        public string CarrierId2 { get; set; }
        public string NPOBAN { get; set; }
        public string IsVoidInvoice { get; set; }
        public Nullable<System.DateTime> VoidInvoiceDateTime { get; set; }
        public Nullable<System.DateTime> TranVoidInvoiceDateTime { get; set; }
        public string PrintMark { get; set; }
        public string PrintType { get; set; }
        public string InvoiceType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RVSB> RVSBs { get; set; }
    }
}
