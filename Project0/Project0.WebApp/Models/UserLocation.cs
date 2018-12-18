using Project0.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class UserLocation
    {
        public ModelUser User { get; set; }
        public List<Location> Locations { get; set; }
    }
}
