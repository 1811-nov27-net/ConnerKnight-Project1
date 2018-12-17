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
        public List<User> Users { get; set; }
        public List<Location> Locations { get; set; }
        public List<PizzaMultiple> Pizzas { get; set; }
    }

    public class PizzaMultiple
    {
        public Pizza Pizza { get; set; }
        public int Quantity { get; set; }
    }
}
