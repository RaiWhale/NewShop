using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class OrderReportVM
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Employee { get; set; }
        public string Customer { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

    }
}