using System;
using System.Collections.Generic;

#nullable disable

namespace Web_Shoe_PTWeb.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public double? Total { get; set; }
        public string ShipAdress { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
