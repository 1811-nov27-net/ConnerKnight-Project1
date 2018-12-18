using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project0.Library;
using Project0.WebApp.Models;

namespace Project0.WebApp.Controllers
{
    public class OrderController : Controller
    {
        public IDataRepository Repo { get; set; }

        public OrderController(IDataRepository repo)
        {
            Repo = repo;
        }

        // GET: Order
        public ActionResult Index()
        {
            //will probably have to send something to the view
            return View(Repo.GetOrderHistory());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Order order = Repo.GetOrder(id);
            return View();
        }

        public ActionResult LogIn()
        {
            return View(new OrderMaster
            {
                Users = Repo.GetUsers().Select(u => new DisplayUser { User = u }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(OrderMaster orderMaster)
        {
            User user = Repo.GetUser(orderMaster.Order.User.UserId);
            Order favorite = user.SuggestedOrder(Repo.GetUserOrderHistory(user));
            return View("Create",new OrderMaster
            {
                Order = new Order { User = user},
                Users = Repo.GetUsers().Select(u => new DisplayUser { User = u }).ToList(),
                Locations = Repo.GetLocations(),
                Pizzas = Repo.GetPizzas().Select(a => new PizzaMultiple { Pizza = a, Quantity = 0 }).ToList(),
                Favorite = favorite
            });
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View(new OrderMaster
            {   
                Users = Repo.GetUsers().Select(u => new DisplayUser {User=u }).ToList(),
                Locations = Repo.GetLocations(),
                Pizzas = Repo.GetPizzas().Select(a => new PizzaMultiple { Pizza = a,Quantity = 0}).ToList()
            });
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderMaster orderMaster)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (orderMaster.Favorite != null)
                    {
                        User u = Repo.GetUser(orderMaster.Order.User.UserId);
                        List<Order> os = Repo.GetUserOrderHistory(u);
                        Order chosen = u.SuggestedOrder(os);
                        chosen.User = u;
                        Repo.AddOrder(chosen);
                    }
                    else
                    {

                        Dictionary<Pizza, int> pizzas = new Dictionary<Pizza, int>();
                        List<Pizza> allPizzas = Repo.GetPizzas();
                        foreach (var i in orderMaster.Pizzas)
                        {
                            if (i.Quantity > 0)
                            {
                                var temp = allPizzas.First(a => a.PizzaId == i.Pizza.PizzaId);
                                pizzas[temp] = i.Quantity;
                            }
                        }
                        User user = Repo.GetUser(orderMaster.Order.User.UserId);
                        Location location;
                        if (orderMaster.Order.Location.LocationId == -1)
                        {
                            location = Repo.GetLocation(orderMaster.Order.User.DefaultLocation.LocationId);
                        }
                        else
                        {
                            location = Repo.GetLocation(orderMaster.Order.Location.LocationId);
                        }

                        DateTime orderTime = orderMaster.Order.OrderTime;
                        Order order = new Order(location, user, orderTime, pizzas);
                        Repo.AddOrder(order);
                    }
                    //location.Inventory = inventory;
                    //Repo.AddOrder(order);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(BadOrderException e)
            {
                //do something with the bad order exception
                ModelState.AddModelError("Order", e.Message);
                return View(new OrderMaster {
                    Order = orderMaster.Order,
                    Users = Repo.GetUsers().Select(u => new DisplayUser { User = u }).ToList(),
                    Locations = Repo.GetLocations(),
                    Pizzas = Repo.GetPizzas().Select(a => new PizzaMultiple { Pizza = a, Quantity = 0 }).ToList()
                });
            }
            catch
            {
                return View(new OrderMaster
                {
                    Order = orderMaster.Order,
                    Users = Repo.GetUsers().Select(u => new DisplayUser { User = u }).ToList(),
                    Locations = Repo.GetLocations(),
                    Pizzas = Repo.GetPizzas().Select(a => new PizzaMultiple { Pizza = a, Quantity = 0 }).ToList()
                });
            }
        }


        //probably won't want to allow deleting order

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            
            Order order = Repo.GetOrder(id);
            if(order != null)
            {
                return View(order);
            }
            

            return RedirectToAction(nameof(Index));
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Repo.DeleteOrderId(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}