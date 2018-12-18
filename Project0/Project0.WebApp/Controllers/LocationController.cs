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
    public class LocationController : Controller
    {
        public IDataRepository Repo { get; set; }

        public LocationController(IDataRepository repo)
        {
            Repo = repo;
        }

        // GET: Location
        public ActionResult Index()
        {
            return View(Repo.GetLocations());
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            Location location = Repo.GetLocation(id);
            var history = Repo.GetLocationIdOrderHistory(id);
            return View(new LocationHistory { Location = location, History = history});
        }

        public ActionResult Order(int id, string sorting)
        {
            Location location = Repo.GetLocation(id);
            List<Order> orderHistory = Repo.GetLocationIdOrderHistory(id);
            switch (sorting)
            {
                case "cheap":
                    orderHistory = OrderManager.CheapestOrderedHistory(orderHistory);
                    break;
                case "expensive":
                    orderHistory = OrderManager.ExpensiveOrderedHistory(orderHistory);
                    break;
                case "latest":
                    orderHistory = OrderManager.LatestOrderedHistory(orderHistory);
                    break;
                case "earliest":
                    orderHistory = OrderManager.EarliestOrderedHistory(orderHistory);
                    break;
            }

            return View("Details", new LocationHistory { Location = location, History = orderHistory });
        }


        // GET: Location/Create
        public ActionResult Create()
        {
            return View(new LocationIngredients
            {
                Ingredients = Repo.GetIngredients().Select(a => new MultipleIngredient { Ingredient = Mapper.Map(a), Quantity = 0 }).ToList()
            });
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelLocation location, List<MultipleIngredient> ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Dictionary<ModelIngredient,int> inventory = new Dictionary<ModelIngredient, int>();
                    foreach (var i in ingredients)
                    {
                        if (i.Quantity > 0)
                        {
                            inventory[i.Ingredient] = i.Quantity;
                        }
                    }
                    location.Inventory = inventory;
                    Repo.AddLocation(Mapper.Map(location));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            Location location = Repo.GetLocation(id);
            return View(new LocationIngredients
            {
                Location = Mapper.Map(location),
                Ingredients = Repo.GetIngredients().Select(a => new MultipleIngredient { Ingredient = Mapper.Map(a), Quantity = 0 }).ToList()
            });
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelLocation location, List<MultipleIngredient> ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Dictionary<ModelIngredient, int> inventory = new Dictionary<ModelIngredient, int>();
                    foreach (var i in ingredients)
                    {
                        if (i.Quantity > 0)
                        {
                            inventory[i.Ingredient] = i.Quantity;
                        }
                    }
                    location.Inventory = inventory;
                    Repo.UpdateLocation(Mapper.Map(location));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            Location location = Repo.GetLocation(id);
            if (location != null)
            {
                return View(location);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Location/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //check for success later
                Repo.DeleteLocationId(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}