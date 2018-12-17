using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelOrder
    {
        public int OrderId { get; set; }
        public ModelLocation Location { get; set; }
        public ModelUser User { get; set; }
        public DateTime OrderTime { get; set; }
        public Dictionary<ModelPizza, int> Contents { get; set; }
    }
}
