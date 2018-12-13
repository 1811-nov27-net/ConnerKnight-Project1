using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Order
    {
        public Order()
        {
            OrderContent = new HashSet<OrderContent>();
        }

        public int OrderId { get; set; }
        public int? LocationId { get; set; }
        public int? UserId { get; set; }
        public DateTime? OrderTime { get; set; }

        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderContent> OrderContent { get; set; }
    }
}
