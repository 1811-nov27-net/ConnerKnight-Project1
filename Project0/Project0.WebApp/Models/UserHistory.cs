using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class UserHistory
    {
        public User User { get; set; }
        public List<Order> History { get; set; }
    }
}
