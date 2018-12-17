using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class ModelPizza
    {
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ModelIngredient> RequiredIng { get; set; }
    }
}
