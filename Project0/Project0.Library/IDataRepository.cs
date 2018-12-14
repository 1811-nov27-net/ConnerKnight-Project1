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

        bool UpdateLocation(Library.Location location);
        //void UpdateOrder(Library.Order order)
        bool UpdateUser(Library.User user);
        bool UpdatePizza(Pizza pizza);
        bool UpdateIngredient(Library.Ingredient ingredient);

        bool DeleteLocation(Library.Location location);
        bool DeleteLocationId(int locationId);
        bool DeleteUser(Library.User user);
        bool DeleteUserId(int userId);
        bool DeleteIngredientId(int ingredientId);
        bool DeletePizzaId(int pizzaId);
        bool DeleteOrderId(int orderId);

        List<Library.Ingredient> GetIngredients();
        Library.Ingredient GetIngredient(int ingredientId);
        Library.Location GetLocation(int locationId);
        List<Library.Order> GetLocationOrderHistory(Library.Location location);
        List<Library.Order> GetUserIdOrderHistory(int userId);
        List<Library.Location> GetLocations();
        List<Pizza> GetPizzas();
        Pizza GetPizza(int pizzaId);
        Library.User GetUser(int userId);
        List<Library.Order> GetUserOrderHistory(Library.User user);
        List<Library.Order> GetLocationIdOrderHistory(int locationId);
        List<Library.User> GetUsers();
        void Save();
    }

}
