using Project0.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project0.WebApp.Models
{
    public class PizzaIngredients
    {
        public Pizza Pizza { get; set; }
        public List<FilterIngredient> Ingredients { get; set; }
    }

}
