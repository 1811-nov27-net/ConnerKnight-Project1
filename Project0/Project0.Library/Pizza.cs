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

    public class Pizza : IVictual
    {
        public int PizzaId { get; set; }
        public string Name { get ; set ; }
        public decimal Price { get ; set ; }
        public List<Ingredient> RequiredIng { get; set; }

        public Pizza(string name, List<Ingredient> reqIng, decimal price)
        {
            Name = name;
            Price = price;
            RequiredIng = reqIng;
        }

        public Pizza()
        {
            RequiredIng = new List<Ingredient>();
        }



    }
}
