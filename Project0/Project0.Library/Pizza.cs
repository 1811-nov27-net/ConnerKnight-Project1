using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library
{
    /*
    public struct EzIng
    { public Ingredient Pepperoni = new Ingredient("Pepperoni");
        Sausage, Pepper, Mushroom, Olive, Cinnamon, Marshmallow, Gold, Jalapeno }
        */

    public class Pizza
    {
        public int PizzaId { get; set; }
        public string Name { get ; set ; }
        public decimal Price { get ; set ; }
        public List<Ingredient> RequiredIng { get; set; }
        //public string Size { get; set; }
        //public Dictionary<string,decimal> sizePrices { get; set; }


        public Pizza(string name, List<Ingredient> reqIng, decimal price)
        {
            Name = name;
            RequiredIng = reqIng;
            Price = price;
        }

        public Pizza()
        {
            RequiredIng = new List<Ingredient>();
        }



    }
}
