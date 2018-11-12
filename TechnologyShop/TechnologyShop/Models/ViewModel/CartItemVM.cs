using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnologyShop.Models.ViewModel
{
    public class CartItemVM
    {
        public int productid { get; set; }
        
        public decimal quantity { get; set; }
    }
}