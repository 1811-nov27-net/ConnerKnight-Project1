using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class LocationHistory
    {
        public Location Location { get; set; }
        public List<Order> History { get; set; }
    }
}
