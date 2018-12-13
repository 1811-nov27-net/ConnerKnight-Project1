using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public Ingredient()
        {

        }
        public Ingredient(string name)
        {
            Name = name;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Ingredient i = (Ingredient)obj;
                return (IngredientId == i.IngredientId) && (Name.Equals(i.Name));
            }
        }

        public override int GetHashCode()
        {
            return IngredientId + Name.GetHashCode();
        }


    }

    
}
