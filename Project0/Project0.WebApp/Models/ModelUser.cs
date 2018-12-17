using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ModelLocation DefaultLocation { get; set; }
    }
}
