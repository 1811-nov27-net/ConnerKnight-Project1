using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class UserLocation
    {
        public User User { get; set; }
        public List<Location> Locations { get; set; }
        public String RawLocation { get; set; }
    }
}
