using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Locationingredient
    {
        public int IngredientId { get; set; }
        public int LocationId { get; set; }
        public int? Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Location Location { get; set; }
    }
}
