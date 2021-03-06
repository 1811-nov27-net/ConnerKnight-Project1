﻿using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DefaultLocationId { get; set; }

        public virtual Location DefaultLocation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
