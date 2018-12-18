using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{

    public class LocationIngredients
    {
        public ModelLocation Location { get; set; }
        public List<MultipleIngredient> Ingredients { get; set; }
    }

    public class MultipleIngredient
    {
        public ModelIngredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
    
}
