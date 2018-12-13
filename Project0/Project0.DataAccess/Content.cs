using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class Content
    {
        public Content()
        {
            ContentIngredient = new HashSet<ContentIngredient>();
            OrderContent = new HashSet<OrderContent>();
        }

        public int ContentId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ContentIngredient> ContentIngredient { get; set; }
        public virtual ICollection<OrderContent> OrderContent { get; set; }
    }
}
