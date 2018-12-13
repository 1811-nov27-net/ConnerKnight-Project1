using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Location
    {
        public Location()
        {
            Locationingredient = new HashSet<Locationingredient>();
            Order = new HashSet<Order>();
            User = new HashSet<User>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Locationingredient> Locationingredient { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
