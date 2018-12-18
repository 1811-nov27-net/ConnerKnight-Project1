using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelPizza
    {
        public int PizzaId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.0,500.0)]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price can't have more precision then a penny")]
        public decimal Price { get; set; }
        public List<ModelIngredient> RequiredIng { get; set; }
    }
}
