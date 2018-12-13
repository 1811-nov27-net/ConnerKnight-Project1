using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library
{
    /// <summary>
    /// Used to Place orders for a user at a location
    /// </summary>
    public static class OrderManager
    {

        public static void PlaceOrder(Order o, List<Order> orderHistory)
        {
            //checking to make sure the order wasn't placed within 2 hours of an order to the same location for the user
            double twoHoursInSeconds = 60 * 60 * 2;
            bool checkUserOrder = orderHistory.Where(a => Math.Abs(a.OrderTime.Subtract(o.OrderTime).TotalSeconds) < (twoHoursInSeconds))
                .Any(b => b.Location.LocationId == o.Location.LocationId && b.User.UserId == o.User.UserId);
            if (checkUserOrder)
            {
                throw new BadOrderException("order was placed within two hours of another order at the same location");
            }
            //checks to make sure there are no more then 12 items in the order and the price does not exceed $500
            //redo this
            
            else if (o.Price() > 500)
            {
                throw new BadOrderException("price of the order exceeded $500");
            }


            //checks to make sure all the contents of the order can be filled by the Location
            Dictionary<Ingredient, int> orderIngredients = new Dictionary<Ingredient, int>();
            //first find all the ingredients necesary and in what amount
            int count = 0;
            foreach (var item in o.Contents)
            {
                /*
                if (item == null)
                {
                    throw new BadOrderException("item in orders contents was null");
                }
                */
                count += item.Value;
                //constant 1 used for each ingredient in RequiredIng, could change
                //Console.WriteLine("num required ingredients: "+ item.Key.RequiredIng.Count);
                foreach (var ing in item.Key.RequiredIng)
                {
                    if (!orderIngredients.ContainsKey(ing))
                    {                            
                        orderIngredients[ing] = item.Value;
                    }
                    else 
                    {
                        orderIngredients[ing] += item.Value;
                    }
                }
                //}             
            }
            if (count > 12)
            {
                throw new BadOrderException("size of the order exceeded 12 items");
            }

            //check to make sure that the Location has the ingredients to fulfill order
            foreach (KeyValuePair<Ingredient,int> pair in orderIngredients)
            {
                if(!o.Location.Inventory.ContainsKey(pair.Key))
                {
                    throw new BadOrderException($"Location does not contain the required Ingredient {pair.Key} ");
                }
                else if(o.Location.Inventory[pair.Key] < pair.Value)
                {
                    throw new BadOrderException($"Location does not have any more {pair.Key}");
                }
            }
            o.User.PlaceOrder(o);
            o.Location.PlaceOrder(o, orderIngredients);
        }

        //display order history sorted by earliest, latest, cheapest, most expensive
        public static List<Order> EarliestOrderedHistory(List<Order> input)
        {
            return input.OrderBy(h => h.OrderTime).ToList();
        }

        public static List<Order> LatestOrderedHistory(List<Order> input)
        {
            return input.OrderByDescending(h => h.OrderTime).ToList();
        }

        public static List<Order> CheapestOrderedHistory(List<Order> input)
        {
            return input.OrderBy(h => h.Price()).ToList();
        }

        public static List<Order> ExpensiveOrderedHistory(List<Order> input)
        {
            return input.OrderByDescending(h => h.Price()).ToList();
        }
    }
}
