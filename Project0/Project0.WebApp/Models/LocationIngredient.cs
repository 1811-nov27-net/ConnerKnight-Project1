using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{

    public class LocationIngredients
    {
        public Location Location { get; set; }
        public List<MultipleIngredient> Ingredients { get; set; }
    }

    public class MultipleIngredient
    {
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
    
}
