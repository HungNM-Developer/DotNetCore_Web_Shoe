using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Shoe_PTWeb.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { set; get; }
        public int TotalMoney => Amount * Product.PromationPrice.Value;
    }
}
