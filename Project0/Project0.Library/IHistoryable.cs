using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library
{
    public interface IHistoryable
    {
        //public List<Order> OrderHistory;
        List<Order> OrderHistory { get; set; }
    }
}
