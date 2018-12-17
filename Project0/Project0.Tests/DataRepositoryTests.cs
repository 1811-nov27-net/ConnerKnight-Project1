using System;
using System.Collections.Generic;
using System.Text;
using Lib = Project0.Library;
using Project0.DataAccess;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Project0.Tests
{
    public class DataRepositoryTests
    {
        [Fact]
        public void SavingChangesWithNothingDoesntThrowException()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project1Context>().UseInMemoryDatabase("no_changes_test").Options;
            using (var db = new Project1Context(options))
            {
                //nothing
            }

            // act
            using (var db = new Project1Context(options))
            {
                var repo = new DataRepository(db);
                repo.Save();
            }

            // assert
            // (no exception should have been thrown)
        }

        [Fact]
        public void GetUsersWorks()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project1Context>().UseInMemoryDatabase("get_users_test").Options;

            using (var db = new Project1Context(options))
            {
                db.User.Add(new DataAccess.User { FirstName = "a", LastName = "b" });
                db.User.Add(new DataAccess.User { FirstName = "c", LastName = "d"});

                db.SaveChanges();
            }

            List<Lib.User> users = new List<Lib.User>();
            using (var db = new Project1Context(options))
            {
                //nothing
                var repo = new DataRepository(db);
                users = repo.GetUsers();
            }
            Assert.Equal("a", users[0].FirstName);
            Assert.Equal("b", users[0].LastName);
            Assert.Equal("c", users[1].FirstName);
            Assert.Equal("d", users[1].LastName);

        }

        [Fact]
        public void GetLocationsWorks()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project1Context>().UseInMemoryDatabase("get_locations_test").Options;

            using (var db = new Project1Context(options))
            {
                db.Location.Add(new DataAccess.Location { Name = "a"});
                db.Location.Add(new DataAccess.Location { Name = "b"});

                db.SaveChanges();
            }

            List<Library.Location> locations = new List<Library.Location>();
            using (var db = new Project1Context(options))
            {
                //nothing
                var repo = new DataRepository(db);
                locations = repo.GetLocations();
            }
            Assert.Equal("a", locations[0].Name);
            Assert.Equal("b", locations[1].Name);

        }

        [Fact]
        public void AddLocationsWorks()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project1Context>().UseInMemoryDatabase("add_locations_test").Options;
            var i1 = new Lib.Ingredient { Name = "Sausage" };
            var location = new Lib.Location
            {
                Name = "alfo",
                Inventory = {
                    { i1, 5}
                }
            };

            using (var db = new Project1Context(options))
            {
                var repo = new DataRepository(db);
                repo.AddLocation(location);

                db.SaveChanges();
            }
            Location l;
            using (var db = new Project1Context(options))
            {
                //nothing
                l = db.Location.Include("Locationingredient.Ingredient").Where(a=> a.LocationId == location.LocationId).First();

                
            }
            Assert.NotNull(l);
            Assert.Equal(location.Name, l.Name);
            //Console.WriteLine(location.Inventory.First().Key.Name);
            Assert.Equal(location.Inventory.First().Key.Name, l.Locationingredient.First().Ingredient.Name);
            Assert.Equal(location.Inventory.First().Value, l.Locationingredient.First().Quantity);

        }

        [Fact]
        public void UpdatePizzaWorks()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project1Context>().UseInMemoryDatabase("update_pizza_test").Options;

            using (var db = new Project1Context(options))
            {
                List<Lib.Ingredient> ingredients = new List<Lib.Ingredient> { new Lib.Ingredient { Name = "Orange" }, new Lib.Ingredient { Name = "Grape" } };
                var repo = new DataRepository(db);
                repo.AddIngredient(ingredients[0]);
                repo.AddIngredient(ingredients[1]);
                Lib.Pizza oldZa = new Lib.Pizza { Name = "Fruit Pizza", Price = 16.99m, RequiredIng = { ingredients[0], ingredients[1] } };
                repo.AddPizza(oldZa);
                oldZa.RequiredIng = new List<Lib.Ingredient>{ ingredients[0] };
                //Lib.Pizza updatedZa = new Lib.Pizza { Name = "Fruit Pizza", Price = 14.49m, RequiredIng = { ingredients[0] } };
                repo.UpdatePizza(oldZa);
                db.SaveChanges();
                Lib.Pizza checkPizza = repo.GetPizza(oldZa.PizzaId);
                Assert.Equal(2, checkPizza.RequiredIng.Count());
                Assert.Equal(ingredients[0], checkPizza.RequiredIng[0]);
                Assert.Equal(ingredients[1], checkPizza.RequiredIng[1]);

            }

            List<Library.Location> locations = new List<Library.Location>();
            using (var db = new Project1Context(options))
            {
                //nothing
                var repo = new DataRepository(db);
                locations = repo.GetLocations();
            }

        }


    }
}
