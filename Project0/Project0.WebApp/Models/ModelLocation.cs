using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelLocation
    {
        public int LocationId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Dictionary<ModelIngredient, int> Inventory { get; set; }
    }
}
