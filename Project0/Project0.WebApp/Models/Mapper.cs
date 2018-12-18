using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project0.Library;

namespace Project0.WebApp.Models
{
    public static class Mapper
    {
        public static Order Map(ModelOrder order) => new Order
        {
            OrderId = order.OrderId,
            User = Map(order.User),
            Location = Map(order.Location),
            OrderTime = order.OrderTime,
            Contents = Map(order.Contents)
        };

        public static ModelOrder Map(Order order) => new ModelOrder
        {
            OrderId = order.OrderId,
            User = Map(order.User),
            Location = Map(order.Location),
            OrderTime = order.OrderTime,
            Contents = Map(order.Contents)
        };

        public static Dictionary<ModelPizza, int> Map(Dictionary<Pizza, int> contents)
        {
            Dictionary<ModelPizza, int> result = new Dictionary<ModelPizza, int>();
            foreach (var item in contents)
            {
                result[Map(item.Key)] = item.Value;
            }
            return result;
        }

        public static Dictionary<Pizza, int> Map(Dictionary<ModelPizza, int> contents)
        {
            Dictionary<Pizza, int> result = new Dictionary<Pizza, int>();
            foreach (var item in contents)
            {
                result[Map(item.Key)] = item.Value;
            }
            return result;
        }

        public static Library.User Map(ModelUser user) => new Library.User
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DefaultLocation = user.DefaultLocation// != null ? Map(user.DefaultLocation) : null
        };

        public static ModelUser Map(Library.User user) => new ModelUser
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DefaultLocation = user.DefaultLocation //!= null ? Map(user.DefaultLocation) : null

        };

        public static Library.Location Map(ModelLocation location) => new Library.Location
        {
            LocationId = location.LocationId,
            Name = location.Name,
            Inventory = location.Inventory != null ? Map(location.Inventory) : null
        };

        public static ModelLocation Map(Library.Location location) => new ModelLocation
        {
            LocationId = location.LocationId,
            Name = location.Name,
            Inventory = location.Inventory != null ? Map(location.Inventory) : null
        };

        public static Dictionary<ModelIngredient, int> Map(Dictionary<Ingredient,int> inventory)
        {
            Dictionary<ModelIngredient, int> result = new Dictionary<ModelIngredient, int>();
            foreach (var item in inventory)
            {
                result[Map(item.Key)] = item.Value;
            }
            return result;
        }

        public static Dictionary<Ingredient, int> Map(Dictionary<ModelIngredient, int> inventory)
        {
            Dictionary<Ingredient, int> result = new Dictionary<Ingredient, int>();
            foreach (var item in inventory)
            {
                result[Map(item.Key)] = item.Value;
            }
            return result;
        }

        public static Library.Pizza Map(ModelPizza pizza) => new Library.Pizza
        {
            PizzaId = pizza.PizzaId,
            Name = pizza.Name,
            Price = pizza.Price,
            RequiredIng = pizza.RequiredIng != null ? Map(pizza.RequiredIng) : null
        };

        public static ModelPizza Map(Library.Pizza pizza) => new ModelPizza
        {
            PizzaId = pizza.PizzaId,
            Name = pizza.Name,
            Price = pizza.Price,
            RequiredIng = pizza.RequiredIng != null ? Map(pizza.RequiredIng) : null
        };

        public static List<ModelIngredient> Map(List<Ingredient> requiredIng)
        {
            return requiredIng.Select(a => Map(a)).ToList();
        }

        public static List<Ingredient> Map(List<ModelIngredient> requiredIng)
        {
            return requiredIng.Select(a => Map(a)).ToList();
        }


        public static Library.Ingredient Map(ModelIngredient ingredient) => new Library.Ingredient
        {
            IngredientId = ingredient.IngredientId,
            Name = ingredient.Name
        };

        public static ModelIngredient Map(Library.Ingredient ingredient) => new ModelIngredient
        {
            IngredientId = ingredient.IngredientId,
            Name = ingredient.Name
        };



    }
}
