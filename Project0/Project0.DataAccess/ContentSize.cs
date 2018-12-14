using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class ContentSize
    {
        public int ContentId { get; set; }
        public string Size { get; set; }
        public decimal? PriceMod { get; set; }

        public virtual Content Content { get; set; }
    }
}
