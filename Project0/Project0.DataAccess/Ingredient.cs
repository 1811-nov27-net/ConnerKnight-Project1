using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            ContentIngredient = new HashSet<ContentIngredient>();
            Locationingredient = new HashSet<Locationingredient>();
        }

        public int IngredientId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ContentIngredient> ContentIngredient { get; set; }
        public virtual ICollection<Locationingredient> Locationingredient { get; set; }
    }
}
