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
        void DeleteLocation(Library.Location location);
        void DeleteUser(Library.User user);
        List<Library.Ingredient> GetIngredients();
        Library.Location GetLocation(string name);
        List<Library.Order> GetLocationOrderHistory(Library.Location location);
        List<Library.Location> GetLocations();
        List<Pizza> GetPizzas();
        Library.User GetUser(string firstName, string lastName);
        List<Library.Order> GetUserOrderHistory(Library.User user);
        List<Library.User> GetUsers();
        void Save();
    }
    
}
