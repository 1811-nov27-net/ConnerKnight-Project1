using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class OrderMaster
    {
        public Order Order { get; set; }
        public List<DisplayUser> Users { get; set; }
        public List<Location> Locations { get; set; }
        public List<PizzaMultiple> Pizzas { get; set; }
        public Order Favorite { get; set; }
    }

    public class DisplayUser
    {
        public User User { get; set; }
        public string FullName { get { return User.FirstName + " " + User.LastName; }}
    }

    public class PizzaMultiple
    {
        public Pizza Pizza { get; set; }
        public int Quantity { get; set; }
    }
}
