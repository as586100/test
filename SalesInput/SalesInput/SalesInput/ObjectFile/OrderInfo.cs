using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInput.ObjectFile
{
    public class OrderInfo
    {
        public string Order { get; set; }
        public string OrderType { get; set; }
        public string OrderOutput { get; set; }
        public string OrderDate { get; set; }
        public string OrderShipDate { get; set; }
        public string OrderNote { get; set; }
        public string OrderPayment{get;set;}
    }
}
