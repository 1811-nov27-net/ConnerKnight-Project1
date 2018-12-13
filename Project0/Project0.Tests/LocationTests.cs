using Project0.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace Project0.Tests
{
    public class LocationTests
    {
        [Fact]
        public void BasicLocationCreateTest()
        {

            Location actLoc = new Location("Fortozo");

            Assert.Equal("Fortozo", actLoc.Name);

        }

        [Fact]
        public void NameMenuLocationCreateTest() {

            Pizza p1 = new Pizza("Pepperoni", new List<Ingredient> { new Ingredient("Pepperoni") }, 19.95m);
            Pizza p2 = new Pizza("Supreme", new List<Ingredient> { new Ingredient("Mushroom"), new Ingredient("Pepperoni"), new Ingredient("Sausage") }, 25.95m);
            Pizza p3 = new Pizza("Dessert", new List<Ingredient> { new Ingredient("Cinnamon"), new Ingredient("Marshmallow") }, 7.95m);
            List<Pizza> menu = new List<Pizza>() { p1, p2, p3 };
            Location actLoc = new Location("Zanos", menu);

            Assert.Equal("Zanos", actLoc.Name);
            Assert.Equal(menu, actLoc.Menu);

        }

        [Fact]
        public void NameInventoryMenuCreateTest() {

            Pizza p1 = new Pizza("Pepperoni", new List<Ingredient> { new Ingredient("Pepperoni") }, 19.95m);
            Pizza p2 = new Pizza("Supreme", new List<Ingredient> { new Ingredient("Mushroom"), new Ingredient("Pepper"), new Ingredient("Sausage") }, 25.95m);
            Pizza p3 = new Pizza("Dessert", new List<Ingredient> { new Ingredient("Cinnamon"), new Ingredient("Marshmallow") }, 7.95m);
            List<Pizza> menu = new List<Pizza>() { p1, p2, p3 };
            Dictionary<Ingredient, int> inventory = new Dictionary<Ingredient, int>() {
                { new Ingredient("Pepperoni"), 5},
                { new Ingredient("Mushroom"), 3 },
                { new Ingredient("Pepper"), 2},
                { new Ingredient("Sausage"), 8},
            };
            Location actLoc = new Location("Zanos", inventory,menu);
            Assert.Equal("Zanos", actLoc.Name);
            Assert.Equal(menu, actLoc.Menu);
            Assert.Equal(inventory, actLoc.Inventory);

        }

            
        
    }
}