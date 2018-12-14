using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library
{

    public interface IDataRepository
    {
        void AddLocation(Library.Location location);
        void AddOrder(Library.Order order);
        void AddUser(Library.User user);
        void AddIngredient(Library.Ingredient ingredient);
        void AddPizza(Library.Pizza pizza);
        void DeleteLocation(Library.Location location);
        void DeleteLocationId(int locationId);
        void DeleteUser(Library.User user);
        void DeleteUserId(int userId);
        void DeleteIngredientId(int ingredientId);
        void DeletePizzaId(int pizzaId);
        void DeleteOrderId(int orderId);
        List<Library.Ingredient> GetIngredients();
        Library.Location GetLocation(int locationId);
        List<Library.Order> GetLocationOrderHistory(Library.Location location);
        List<Library.Order> GetUserIdOrderHistory(int userId);
        List<Library.Location> GetLocations();
        List<Pizza> GetPizzas();
        Library.User GetUser(int userId);
        List<Library.Order> GetUserOrderHistory(Library.User user);
        List<Library.Order> GetLocationIdOrderHistory(int locationId);
        List<Library.User> GetUsers();
        void Save();
    }

}
