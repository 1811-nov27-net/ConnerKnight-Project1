using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project0.Library;

namespace Project0.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private readonly Project1Context db;

        public DataRepository(Project1Context _db)
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

        private User getNewUser(Library.User user)
        {
            User u = Mapper.Map(user);
            u.DefaultLocation = db.Location.Find(user.DefaultLocation.LocationId);
            return u;
        }

        public bool DeleteUser(Library.User user)
        {
            return DeleteUserId(user.UserId);
        }
        public bool DeleteUserId(int userId)
        {
            User user = db.User.Find(userId);
            if (user != null)
            {
                db.Remove(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        //public void UpdateUser()

        public void AddLocation(Library.Location location)
        {
            Location l = getNewLocation(location);
            db.Add(l);
            db.SaveChanges();
            //foreach(var pair in location.Inventory)
            //{
            //    Ingredient i = db.Ingredient.First(a => a.Name == pair.Key.Name);
            //    if(i == null)
            //    {
            //        i = new Ingredient() { Name = pair.Key.Name };
            //    }
            //    l.Locationingredient.Add(new Locationingredient() { Ingredient = i, Location = l, Quantity = pair.Value });
                
            //}
            location.LocationId = l.LocationId;
        }

        private Location getNewLocation(Library.Location location)
        {
            Location newLoc = Mapper.Map(location);
            foreach (var pair in location.Inventory)
            {
                Ingredient i = db.Ingredient.Find(pair.Key.IngredientId);
                //probably shouldnt do this
                if (i == null)
                {
                    i = new Ingredient() { Name = pair.Key.Name };
                }
                newLoc.Locationingredient.Add(new Locationingredient() { Ingredient = i, Location = newLoc, Quantity = pair.Value });

            }
            return newLoc;
        }

        public bool DeleteLocation(Library.Location location)
        {
            return DeleteLocationId(location.LocationId);
        }

        public bool DeleteLocationId(int locationId)
        {
            var location = db.Location.Find(locationId);
            if(location != null)
            {
                db.Remove(location);
                db.SaveChanges();
                return true;
            }
            return false;
            
        }


        public void AddOrder(Library.Order order)
        {

            Library.OrderManager.PlaceOrder(order,GetUserOrderHistory(order.User));
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
                    c = new Content() { Name=pair.Key.Name,Price = pair.Key.Price};
                }
                o.OrderContent.Add(new OrderContent() { Order=o,Content=c,Amount=pair.Value});

            }
            db.Order.Add(o);

            db.SaveChanges();
            order.OrderId = o.OrderId;
            
            //db.Add(Mapper.Map(order));
        }

        public Library.User GetUser(int userId)
        {
            return Mapper.Map(db.User.First(a => a.UserId == userId));
        }

        public List<Library.User> GetUsers()
        {
            List<User> users = db.User.Include(a => a.DefaultLocation).AsNoTracking().ToList();
            return users.Select(a => Mapper.Map(a)).ToList();
        }


        public Library.Location GetLocation(int locationId)
        {
            Location l = db.Location.Include("Locationingredient.Ingredient").Where(a => a.LocationId == locationId).First();
            Dictionary<Library.Ingredient, int> tempInventory = new Dictionary<Library.Ingredient, int>();
            foreach (var i in l.Locationingredient)
            {
                tempInventory[Mapper.Map(i.Ingredient)] = i.Quantity;
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
                    tempInventory[Mapper.Map(i.Ingredient)] = i.Quantity;
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
                result.Add(new Pizza() { Name = c.Name, PizzaId = c.ContentId,RequiredIng= reqIng,Price=c.Price });
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
            return GetUserIdOrderHistory(user.UserId);
        }

        public List<Library.Order> GetUserIdOrderHistory(int userId)
        {
            List<Order> os = db.Order.Where(a => a.UserId == userId).Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").ToList();
            return GetOrderHistory(os);
            

        }
        public List<Library.Order> GetLocationOrderHistory(Library.Location location)
        {
            return GetLocationIdOrderHistory(location.LocationId);
        }

        public List<Library.Order> GetLocationIdOrderHistory(int locationId)
        {
            List<Order> os = db.Order.Where(a => a.LocationId == locationId).Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").ToList();
            return GetOrderHistory(os);
        }

        public List<Library.Order> GetOrderHistory()
        {
            return GetOrderHistory(db.Order.Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").ToList());
        }
        public Library.Order GetOrder(int orderId)
        {
            Order order = db.Order.Include(a => a.User).Include(b => b.Location).Include("OrderContent.Content").First(o => o.OrderId == orderId);
            Library.Location l = Mapper.Map(order.Location);
            Library.User u = Mapper.Map(order.User);
            Dictionary<Pizza, int> pizzas = new Dictionary<Pizza, int>();
            foreach (OrderContent oc in order.OrderContent)
            {
                Pizza p = new Pizza() { Name = oc.Content.Name, Price = oc.Content.Price };
                pizzas[p] = oc.Amount;
            }

            Library.Order result = new Library.Order{
                OrderId = order.OrderId,
                User = u, Location = l,
                Contents = pizzas,
                OrderTime = order.OrderTime };
            return result;
        }

        private List<Library.Order> GetOrderHistory(List<Order> os)
        {
            List<Library.Order> result = new List<Library.Order>();
            foreach (var o in os)
            {
                Library.Location l = Mapper.Map(o.Location);
                Library.User u = Mapper.Map(o.User);
                Dictionary<Pizza, int> pizzas = new Dictionary<Pizza, int>();
                foreach (OrderContent oc in o.OrderContent)
                {
                    Pizza p = new Pizza() { Name = oc.Content.Name,Price=oc.Content.Price};
                    pizzas[p] = oc.Amount;
                }

                Library.Order tempOrder = new Library.Order() { OrderId = o.OrderId,User = u, Location = l, Contents = pizzas, OrderTime = o.OrderTime };
                result.Add(tempOrder);
            }

            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void AddIngredient(Library.Ingredient ingredient)
        {
            Ingredient i = new Ingredient { Name = ingredient.Name };
            db.Ingredient.Add(i);
            db.SaveChanges();
            ingredient.IngredientId = i.IngredientId;
        }

        public void AddPizza(Pizza pizza)
        {
            //List<ContentIngredient> col = new List<ContentIngredient>();
            //foreach(var i in pizza.RequiredIng)
            //{
            //    var temp = db.Ingredient.Find(i.IngredientId);
            //    col.Add(new ContentIngredient { Ingredient = temp });
            //}
            Content c = GetNewContent(pizza);
            db.Content.Add(c);
            db.SaveChanges();
            pizza.PizzaId = c.ContentId;

        }

        private Content GetNewContent(Pizza pizza)
        {
            List<ContentIngredient> col = new List<ContentIngredient>();
            foreach (var i in pizza.RequiredIng)
            {
                var temp = db.Ingredient.Find(i.IngredientId);
                col.Add(new ContentIngredient { Ingredient = temp });
            }

            return new Content
            {
                Name = pizza.Name,
                Price = pizza.Price,
                ContentIngredient = col
            };
        }

        public bool UpdateIngredient(Library.Ingredient ingredient)
        {
            //_db.Entry(_db.Restaurant.Find(restaurant.Id)).CurrentValues.SetValues(Mapper.Map(restaurant));
            var temp = db.Ingredient.Find(ingredient.IngredientId);
            if (temp != null)
            {
                db.Entry(temp).CurrentValues.SetValues(new Ingredient {IngredientId = ingredient.IngredientId, Name = ingredient.Name });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteIngredientId(int ingredientId)
        {
            var ingredient = db.Ingredient.Find(ingredientId);
            if (ingredient != null)
            {
                db.Ingredient.Remove(ingredient);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePizzaId(int pizzaId)
        {
            var pizza = db.Content.Find(pizzaId);
            if(pizza != null)
            {
                db.Content.Remove(pizza);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteOrderId(int orderId)
        {
            var order = db.Order.Find(orderId);
            if(order != null)
            {
                db.Order.Remove(order);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Library.Ingredient GetIngredient(int ingredientId)
        {
            var temp = db.Ingredient.Find(ingredientId);
            return new Library.Ingredient { IngredientId = temp.IngredientId, Name = temp.Name };
        }

        public Pizza GetPizza(int pizzaId)
        {
            var temp = db.Content.Include("ContentIngredient.Ingredient").First(p=> p.ContentId == pizzaId);
            List<Library.Ingredient> reqIng = new List<Library.Ingredient>();
            foreach (var i in temp.ContentIngredient)
            {
                reqIng.Add(new Library.Ingredient() { Name = i.Ingredient.Name, IngredientId = i.Ingredient.IngredientId });
            }
            return new Pizza() { Name = temp.Name, PizzaId = temp.ContentId, RequiredIng = reqIng, Price = temp.Price };
        }

        public bool UpdateLocation(Library.Location location)
        {
            ////_db.Entry(_db.Restaurant.Find(restaurant.Id)).CurrentValues.SetValues(Mapper.Map(restaurant));
            var temp = db.Location.Find(location.LocationId);
            if(temp != null)
            {
                db.Entry(temp).CurrentValues.SetValues(getNewLocation(location).LocationId = location.LocationId);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUser(Library.User user)
        {
            var temp = db.User.Find(user.UserId);
            if (temp != null)
            {
                db.Entry(temp).CurrentValues.SetValues(getNewUser(user).UserId = user.UserId);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        //something wrong with this update, check it out later
        public bool UpdatePizza(Pizza pizza)
        {
            var temp = db.Content.Include("ContentIngredient.Ingredient").First(c => c.ContentId == pizza.PizzaId);
            if(temp != null)
            {
                //old:                 db.Entry(temp).CurrentValues.SetValues(GetNewContent(pizza).ContentId = pizza.PizzaId);
                Content updated = GetNewContent(pizza);
                updated.ContentId = pizza.PizzaId;
                db.Entry(temp).CurrentValues.SetValues(updated);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Library.Location GetLocationByName(string name)
        {
            Location temp = db.Location.First(a => a.Name == name);
            return GetLocation(temp.LocationId);
        }
    }
}
