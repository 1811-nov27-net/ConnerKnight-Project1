using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project0.Library;

namespace Project0.DataAccess
{
    public class DataRepository : Library.IDataRepository
    {
        private readonly Project0Context db;

        public DataRepository(Project0Context _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(_db));
        }

        public void AddUser(Library.User user)
        {
            User u = Mapper.Map(user);
            u.DefaultLocation = db.Location.Find(user.DefaultLocation.LocationId);
            db.Add(u);
            db.SaveChanges();
            user.UserId = u.UserId;

        }

        public void DeleteUser(Library.User user)
        {
            db.Remove(db.User.Find(user.UserId));
        }

        //public void UpdateUser()

        public void AddLocation(Library.Location location)
        {
            Location l = Mapper.Map(location);
            db.Add(l);
            db.SaveChanges();
            foreach(var pair in location.Inventory)
            {
                Ingredient i = db.Ingredient.First(a => a.Name == pair.Key.Name);
                if(i == null)
                {
                    i = new Ingredient() { Name = pair.Key.Name };
                }
                l.Locationingredient.Add(new Locationingredient() { Ingredient = i, Location = l, Quantity = pair.Value });
                
            }
            location.LocationId = l.LocationId;
            db.SaveChanges();
        }

        public void DeleteLocation(Library.Location location)
        {
            db.Remove(db.Location.Find(location.LocationId));
        }


        public void AddOrder(Library.Order order)
        {

            //Console.WriteLine("size of contents: " + order.Contents.Count);
            Library.OrderManager.PlaceOrder(order,GetUserOrderHistory(order.User));
            //dont know if this will work

            User u = db.User.Find(order.User.UserId);
            Location l = db.Location.Find(order.Location.LocationId);
            //Console.WriteLine("the real pepperoni" + order.Location.Inventory[order.Location.Inventory.Keys.Where(a => a.Name == "Pepperoni").First()]);
            l.Locationingredient = Mapper.Map(order.Location.Inventory);
            Order o = new Order() { User = u, Location = l, OrderTime = order.OrderTime};


            foreach(var pair in order.Contents)
            {
                Content c = db.Content.Where(a => a.Name == pair.Key.Name).First();
                if(c == null)
                {
                    c = new Content() { Name=pair.Key.Name,Price= pair.Key.Price};
                }
                o.OrderContent.Add(new OrderContent() { Order=o,Content=c,Amount=pair.Value});

            }
            db.Order.Add(o);

            db.SaveChanges();
            
            //db.Add(Mapper.Map(order));
        }

        public Library.User GetUser(string firstName, string lastName)
        {
            return Mapper.Map(db.User.Where(a => a.FirstName == firstName && a.LastName == lastName).First());
        }

        public List<Library.User> GetUsers()
        {
            List<User> users = db.User.Include(a => a.DefaultLocation).AsNoTracking().ToList();
            return users.Select(a => Mapper.Map(a)).ToList();
        }

        public Library.Location GetLocation(string name)
        {
            Location l = db.Location.Include("Locationingredient.Ingredient").Where(a => a.Name == name).First();
            Dictionary<Library.Ingredient, int> tempInventory = new Dictionary<Library.Ingredient, int>();
            foreach (var i in l.Locationingredient)
            {
                tempInventory[Mapper.Map(i.Ingredient)] = i.Quantity ?? 0;
            }
            return new Library.Location() { LocationId = l.LocationId, Name = l.Name, Inventory = tempInventory };

        }

        public List<Library.Location> GetLocations()
        {
            List<Location> locations = db.Location.Include("Locationingredient.Ingredient").ToList();
            List<Library.Location> result = new List<Library.Location>();
            foreach(var l in locations)
            {
                Dictionary<Library.Ingredient, int> tempInventory = new Dictionary<Library.Ingredient, int>();
                foreach(var i in l.Locationingredient)
                {
                    tempInventory[Mapper.Map(i.Ingredient)] = i.Quantity ?? 0;
                }
                Library.Location temp = new Library.Location() { LocationId = l.LocationId,Name = l.Name, Inventory = tempInventory };
                result.Add(temp);
            }

            return result;

        }

        public List<Library.Pizza> GetPizzas()
        {
            List<Content> content = db.Content.Include("ContentIngredient.Ingredient").ToList();
            List<Library.Pizza> result = new List<Library.Pizza>();
            foreach(var c in content)
            {
                List<Library.Ingredient> reqIng = new List<Library.Ingredient>();
                foreach(var i in c.ContentIngredient)
                {
                    reqIng.Add(new Library.Ingredient() { Name = i.Ingredient.Name, IngredientId = i.Ingredient.IngredientId });
                }
                result.Add(new Pizza() { Name = c.Name, Price = c.Price ?? 0, PizzaId = c.ContentId,RequiredIng= reqIng });
            }
            return result;
            //return db.Content.Select(a => new Library.Pizza() { Name = a.Name, Price = a.Price ?? 0 }).ToList();
        }

        public List<Library.Ingredient> GetIngredients()
        {
            return db.Ingredient.Select(a => new Library.Ingredient() { Name = a.Name, IngredientId = a.IngredientId }).ToList();
        }


        public List<Library.Order> GetUserOrderHistory(Library.User user)
        {
            List<Order> os = db.Order.Where(a => a.UserId == user.UserId).Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").ToList();
            return GetOrderHistory(os);
            

        }

        public List<Library.Order> GetLocationOrderHistory(Library.Location location)
        {
            List<Order> os = db.Order.Where(a => a.LocationId == location.LocationId).Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").ToList();
            return GetOrderHistory(os);
        }

        public List<Library.Order> GetOrderHistory(List<Order> os)
        {
            List<Library.Order> result = new List<Library.Order>();
            foreach (var o in os)
            {
                Library.Location l = Mapper.Map(o.Location);
                Library.User u = Mapper.Map(o.User);
                Dictionary<Pizza, int> pizzas = new Dictionary<Pizza, int>();
                foreach (OrderContent oc in o.OrderContent)
                {
                    Pizza p = new Pizza() { Name = oc.Content.Name, Price = oc.Content.Price ?? 0 };
                    pizzas[p] = oc.Amount ?? 0;
                }

                Library.Order tempOrder = new Library.Order() { OrderId = o.OrderId,User = u, Location = l, Contents = pizzas, OrderTime = o.OrderTime ?? DateTime.Now };
                result.Add(tempOrder);
            }

            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
