using System;
using System.Collections.Generic;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
