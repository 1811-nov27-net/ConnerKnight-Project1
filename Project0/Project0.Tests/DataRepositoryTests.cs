using System;
using System.Collections.Generic;
using System.Text;
using Project0.Library;
using Project0.DataAccess;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Project0.Tests
{
    public class DataRepositoryTests
    {
        [Fact]
        public void SavingChangesWithNothingDoesntThrowException()
        {
            // arrange
            var options = new DbContextOptionsBuilder<Project0Context>().UseInMemoryDatabase("no_changes_test").Options;
            using (var db = new Project0Context(options))
            {
                //nothing
            }

            // act
            using (var db = new Project0Context(options))
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
            var options = new DbContextOptionsBuilder<Project0Context>().UseInMemoryDatabase("get_users_test").Options;

            using (var db = new Project0Context(options))
            {
                db.User.Add(new DataAccess.User { FirstName = "a", LastName = "b" });
                db.User.Add(new DataAccess.User { FirstName = "c", LastName = "d"});

                db.SaveChanges();
            }

            List<Library.User> users = new List<Library.User>();
            using (var db = new Project0Context(options))
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
            var options = new DbContextOptionsBuilder<Project0Context>().UseInMemoryDatabase("get_locations_test").Options;

            using (var db = new Project0Context(options))
            {
                db.Location.Add(new DataAccess.Location { Name = "a"});
                db.Location.Add(new DataAccess.Location { Name = "b"});

                db.SaveChanges();
            }

            List<Library.Location> locations = new List<Library.Location>();
            using (var db = new Project0Context(options))
            {
                //nothing
                var repo = new DataRepository(db);
                locations = repo.GetLocations();
            }
            Assert.Equal("a", locations[0].Name);
            Assert.Equal("b", locations[1].Name);



            // assert
            // (no exception should have been thrown)
        }


    }
}
