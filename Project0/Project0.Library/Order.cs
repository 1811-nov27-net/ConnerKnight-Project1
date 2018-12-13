using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library
{
    public class Order
    {
        public int OrderId { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public DateTime OrderTime { get; set; }
        //made it so you can only order pizzas
        public Dictionary<Pizza,int> Contents { get; set; }
        //public decimal Price;

        public Order(Location l, User u, DateTime ot, Dictionary<Pizza,int> c)
        {
            Location = l;
            User = u;
            OrderTime = ot;
            Contents = c;
            //Price = 0;//Contents.Sum(s => s.Price);
        }

        public Order()
        {
            Contents = new Dictionary<Pizza, int>();
        }

        public decimal Price()
        {
            decimal total = 0.0M;
            foreach(var pair in Contents)
            {
                total += pair.Key.Price * pair.Value;
            }

            return total;
        }


        //serializing order is going to break everything
        //going to have to do this another way, maybe ...
        //don't know thinkabout this
        //don't have to use xml so dont use this
        /*
        public Order()
        {
            Location = new Location();
            User = new User();
            OrderTime = new DateTime();
            Contents = new List<Pizza>();
        }
        */
        

        /*
        public void PlaceOrder()
        {
            if(Contents.Count > 12)
            {
                throw new BadOrderException("size of the order exceeded 12 items")
            }
            else if(Price > 500)
            {
                //...
            }
        }
        */

        /*
        public override bool Equals(object obj)
        {
            if((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order o = (Order)obj;
                return 
            }
        }
        */

    }
}
