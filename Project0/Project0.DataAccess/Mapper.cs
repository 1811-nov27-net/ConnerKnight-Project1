using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.DataAccess
{
    public static class Mapper
    {

        public static Library.User Map(User user) => new Library.User
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DefaultLocation = user.DefaultLocation != null ? Map(user.DefaultLocation) : null
        };

        public static User Map(Library.User user) => new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Order = new List<Order>(),
            //DefaultLocation = user.DefaultLocation != null ? Map(user.DefaultLocation) : null
        };

        public static Library.Location Map(Location location) => new Library.Location
        {
            LocationId = location.LocationId,
            Name = location.Name
            //Inventory = Map(location.Locationingredient)
        };

        public static Location Map(Library.Location location) => new Location
        {
            //Id = location.id
            Name = location.Name
            //List < Locationingredient > result = new List<Locationingredient>();
            /*
            foreach(KeyValuePair<Ingredient, int> pair in location.Inventory)
            {
                result.Add(new Locationingredient() { })
            }
            */
        };

        public static Library.Ingredient Map(Ingredient ingredient) => new Library.Ingredient
        {
            IngredientId = ingredient.IngredientId,
            Name = ingredient.Name
        };

        public static Ingredient Map(Library.Ingredient ingredient) => new Ingredient
        {
            //IngredientId = ingredient.IngredientId,
            Name = ingredient.Name
        };

        public static List<Locationingredient> Map(Dictionary<Library.Ingredient, int> inventory) {
            List<Locationingredient> result = new List<Locationingredient>();
            foreach (var pair in inventory)
            {
                result.Add(new Locationingredient() { IngredientId = pair.Key.IngredientId, Quantity = pair.Value });
            }

            return result;

        }



    }
}
