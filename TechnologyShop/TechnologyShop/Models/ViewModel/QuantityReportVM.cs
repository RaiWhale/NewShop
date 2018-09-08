using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class QuantityReportVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string OrderCode { get; set; }
        public decimal SumQuantity { get; set; }
        public decimal SumTotal { get; set; }
    }
}