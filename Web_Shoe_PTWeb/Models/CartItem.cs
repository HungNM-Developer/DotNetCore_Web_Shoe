using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Shoe_PTWeb.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public int TotalMoney => Amount * Product.PromationPrice.Value;
    }
}
