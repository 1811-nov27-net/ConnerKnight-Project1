using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class ContentIngredient
    {
        public int ContentId { get; set; }
        public int IngredientId { get; set; }

        public virtual Content Content { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
