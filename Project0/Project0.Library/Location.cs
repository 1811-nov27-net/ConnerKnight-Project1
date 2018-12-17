using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.Library
{

    
    /// <summary>
    /// Represents a named Pizza location with an inventory of ingredients
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        //was Dictionary<IVictual,int> had to make List<KeyValuePair<IVictual,int>> for serialization
        public Dictionary<Ingredient, int> Inventory { get; set; }
        //made it so only pizzas could be ordered
        //public List<Pizza> Menu { get; set; }

        /*
        public Location(string name, Dictionary<Ingredient, int> inventory, List<Pizza> menu ,List<Order> orderHistory)
        {
            Name = name;
            Inventory = inventory;
            Menu = menu;
            OrderHistory = orderHistory;
        }
        */

        public Location(string name, Dictionary<Ingredient, int> inventory)
        {
            Name = name;
            Inventory = inventory;
        }
        public Location(string name)
        {
            Name = name;
            Inventory = new Dictionary<Ingredient, int>();
        }

        public Location()
        {
            Name = null;
            Inventory = new Dictionary<Ingredient, int>();
        }
        
        /// <summary>
        /// subtracts the ingredients in orderIngredients from the locations ingredients
        /// </summary>
        /// <param name="o"> the order that is going to be placed on this Location</param>
        /// <param name="orderIngredients"> the ingredients that the order requires</param>
        public void PlaceOrder(Order o, Dictionary<Ingredient, int> orderIngredients)
        {
            foreach(KeyValuePair<Ingredient,int> pair in orderIngredients)
            {
                //Console.WriteLine(pair.Value);
                Inventory[pair.Key] = Inventory[pair.Key] - pair.Value;
            }
        }

       
    }
}
