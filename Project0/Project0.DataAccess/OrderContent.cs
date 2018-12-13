using System;
using System.Collections.Generic;

namespace Project0.DataAccess
{
    public partial class OrderContent
    {
        public int OrderId { get; set; }
        public int ContentId { get; set; }
        public int? Amount { get; set; }

        public virtual Content Content { get; set; }
        public virtual Order Order { get; set; }
    }
}
