using Project0.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace Project0.Tests
{
    public class DataManagerTests
    {
        [Fact]
        public void TestSerializeDeserialize()
        {
            //arrange
            DataManager dm = new DataManager("../../../");
            
            
            User u1 = new User("Pete", "Sort");
            User u2 = new User("Kevin", "Pork");
            User u3 = new User("Rachel", "Mort");
            List<User> ul = new List<User>(){ u1,u2,u3};

            Location l1 = new Location("Baziznoz");
            Location l2 = new Location("Gorgoneli");
            Location l3 = new Location("Blasto's Palace");
            List<Location> ol = new List<Location>() { l1, l2, l3 };

            dm.SerializeAll(ul, ol);
            List<User> deUsers = dm.DeserializeUsers();
            List<Location> deLoc = dm.DeserializeLocation();
            Assert.Equal(ul, deUsers);
            Assert.Equal(ol, deLoc);

        }
    }
}