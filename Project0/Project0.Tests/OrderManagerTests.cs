using System;
using System.Collections.Generic;
using System.Text;
using Project0.Library;
using Xunit;

namespace Project0.Tests
{
    public class OrderManagerTests
    {



        [Fact]
        public void OrderSortTest()
        {

            User u = new User("Sorry", "Cart");
            Location l = new Location("Baziznoz");
            DateTime d1 = new DateTime(1995, 1, 25);
            DateTime d2 = new DateTime(1995, 2, 5);
            DateTime d3 = new DateTime(1995, 4, 12);
            DateTime d4 = new DateTime(1994, 8, 10);
            Pizza p1 = new Pizza("Pepperoni", new List<Ingredient> { new Ingredient("Pepperoni") }, 19.95m);
            Pizza p2 = new Pizza("Supreme", new List<Ingredient> { new Ingredient("Mushroom"), new Ingredient("Pepper"), new Ingredient("Sausage") }, 25.95m);
            Pizza p3 = new Pizza("Dessert", new List<Ingredient> { new Ingredient("Cinnamon"), new Ingredient("Marshmallow") }, 7.95m);
            Dictionary<Pizza, int> oFood1 = new Dictionary<Pizza, int>() { { p1, 1 } };
            Order o1 = new Order(l, u, d1, oFood1);
            Dictionary<Pizza, int> oFood2 = new Dictionary<Pizza, int>() { { p2, 1 }, { p3, 1 } };
            Order o2 = new Order(l, u, d2, oFood2);
            Dictionary<Pizza, int> oFood3 = new Dictionary<Pizza, int>() { { p2, 1 } };
            Order o3 = new Order(l, u, d4, oFood3);
            //Dictionary<Pizza, int> oFood4 = new Dictionary<Pizza, int>() { { p2, 1 }, { p3, 1 } };
            //Order o4 = new Order(l, u, d3, oFood2);

            //act
            List<Order> OrderHistory = new List<Order>() { o1,o2,o3};
            List<Order> exp1 = new List<Order> { o3, o1, o2 };

            List<Order> act1 = OrderManager.EarliestOrderedHistory(OrderHistory);

            List<Order> exp2 = new List<Order> { o2, o1, o3 };
            List<Order> act2 = OrderManager.LatestOrderedHistory(OrderHistory);

            List<Order> exp3 = new List<Order> { o1, o3, o2 };
            List<Order> act3 = OrderManager.CheapestOrderedHistory(OrderHistory);

            List<Order> exp4 = new List<Order> { o2, o3, o1 };
            List<Order> act4 = OrderManager.ExpensiveOrderedHistory(OrderHistory);

            Assert.Equal(exp1, act1);
            Assert.Equal(exp2, act2);
            Assert.Equal(exp3, act3);
            Assert.Equal(exp4, act4);

        }

    [Fact]
    public void BadOrderTests()
    {
            User u = new User("Sorry", "Cart");
            Dictionary<Ingredient, int> inventory = new Dictionary<Ingredient, int>() {
                { new Ingredient("Pepperoni"), 5},
                { new Ingredient("Mushroom"), 3 },
                { new Ingredient("Pepper"), 2},
                { new Ingredient("Sausage"), 8},
                { new Ingredient("Cinnamon"), 4},
                { new Ingredient("Marshmallow"), 2}
            };
            Location l = new Location("Baziznoz");
            DateTime d1 = new DateTime(1995, 1, 25, 12,55,22);
            DateTime d2 = new DateTime(1995, 1, 25,14,54,37);
            DateTime d3 = new DateTime(1995, 4, 12);
            DateTime d4 = new DateTime(1994, 8, 10);
            Pizza p1 = new Pizza("Pepperoni", new List<Ingredient> { new Ingredient("Pepperoni") }, 19.95m);
            Pizza p2 = new Pizza("Supreme", new List<Ingredient> { new Ingredient("Mushroom"), new Ingredient("Pepper"), new Ingredient("Sausage") }, 25.95m);
            Pizza p3 = new Pizza("Dessert", new List<Ingredient> { new Ingredient("Cinnamon"), new Ingredient("Marshmallow") }, 7.95m);
            Pizza p4 = new Pizza("Golden Pizza", new List<Ingredient> { new Ingredient("Gold"), new Ingredient("Mushroom") }, 325.95m);
            Pizza p5 = new Pizza("Spicy Pizza", new List<Ingredient> { new Ingredient("Pepperoni"), new Ingredient("Jalapeno") }, 23.95m);
            l.Inventory = inventory;
            Dictionary<Pizza, int> oFood1 = new Dictionary<Pizza, int>() { { p1, 1 } };
            Order o1 = new Order(l, u, d1, oFood1);
            Dictionary<Pizza, int> oFood2 = new Dictionary<Pizza, int>() { { p2, 1 }, { p3, 1 } };
            Order o2 = new Order(l, u, d2, oFood2);
            Dictionary<Pizza, int> oFood3 = new Dictionary<Pizza, int>() { { p2, 1 } };
            Order o3 = new Order(l, u, d4, oFood3);

            Dictionary<Pizza, int> oFood4 = new Dictionary<Pizza, int>() { {p1,6 },{ p2, 4 },{ p3,5} };
            Order o4 = new Order(l, u, d3, oFood4);

            Dictionary<Pizza, int> oFood5 = new Dictionary<Pizza, int>() { { p4,2} };
            Order o5 = new Order(l, u, d3, oFood5);

            //List<Pizza> oFood6 = new List<Pizza>() { p3,p1,null };
            //Order o6 = new Order(l, u, d3, oFood6);

            Dictionary<Pizza, int> oFood7 = new Dictionary<Pizza, int>() { { p5,1 } };
            Order o7 = new Order(l, u, d3, oFood7);

            Dictionary<Pizza, int> oFood8 = new Dictionary<Pizza, int>() { { p3, 3 } };
            Order o8 = new Order(l, u, d3, oFood8);

            List<Order> orderHistory = new List<Order>();
            OrderManager.PlaceOrder(o1,orderHistory);
            orderHistory.Add(o1);
            //Action act = () => OrderManager.PlaceOrder(o2);
            //throws an exception because order 2 is within 2 hours of order 1
            Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o2,orderHistory));
            //throws an exception because order 4 has more then 12 items
            Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o4, orderHistory));
            //throws an exception because order 5 price is more then $500
            Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o5, orderHistory));
            //**** not going to test thisthrows an exception because order 6 has a null element in it's content
            //Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o6));
            //throws an exception because this restaurant doesn't have jalapenos
            Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o7, orderHistory));
            //throws an exception because this restaurant is out of marshmellows
            Assert.Throws<BadOrderException>(() => OrderManager.PlaceOrder(o8, orderHistory));



        }


        /*
        //arrange
            User u = new User("Sorry", "Cart");
            Location l = new Location("Baziznoz");
            DateTime d1 = new DateTime(1995, 1, 25);
            DateTime d2 = new DateTime(1995, 2, 5);
            DateTime d3 = new DateTime(1995, 4, 12);
            DateTime d4 = new DateTime(1994, 8, 10);
            Pizza p1 = new Pizza("Pepperoni", new List<Ingredient> { Ingredient.Pepperoni}, 19.95m);
            Pizza p2 = new Pizza("Supreme", new List<Ingredient> { Ingredient.Mushroom, Ingredient.Pepper, Ingredient.Sausage }, 25.95m);
            Pizza p3 = new Pizza("Dessert", new List<Ingredient> { Ingredient.Cinnamon, Ingredient.Mushroom }, 7.95m);
            List<Pizza> oFood1 = new List<Pizza>() { p1};
            Order o1 = new Order(l, u, d1, oFood1);
            List<Pizza> oFood2 = new List<Pizza>() { p2,p3};
            Order o2 = new Order(l, u, d2, oFood2);
            List<Pizza> oFood3 = new List<Pizza>() { p2 };
            Order o3 = new Order(l, u, d4, oFood3);
            //act
            List<Order> exp1 = new List<Order> { o3, o1, o2 };
            List<Order> act1 = l.EarliestOrderedHistory();

            List<Order> exp2 = new List<Order> { o2, o1, o3 };
            List<Order> act2 = l.LatestOrderedHistory();

            List<Order> exp3 = new List<Order> { o1, o3, o2 };
            List<Order> act3 = l.CheapestOrderedHistory();

            List<Order> exp4 = new List<Order> { o2, o3, o1 };
            List<Order> act4 = l.ExpensiveOrderedHistory();

            //Assert
            Assert.Equal(exp1, act1);
            Assert.Equal(exp2, act2);
            Assert.Equal(exp3, act3);
            Assert.Equal(exp4, act4);
    
        */

    }
}
