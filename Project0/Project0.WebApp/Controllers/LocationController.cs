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
            return View(location);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View(new LocationIngredients
            {
                Ingredients = Repo.GetIngredients().Select(a => new MultipleIngredient { Ingredient = a, Quantity = 0 }).ToList()
            });
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Location location, List<MultipleIngredient> ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Dictionary<Ingredient,int> inventory = new Dictionary<Ingredient,int>();
                    foreach (var i in ingredients)
                    {
                        if (i.Quantity > 0)
                        {
                            inventory[i.Ingredient] = i.Quantity;
                        }
                    }
                    location.Inventory = inventory;
                    Repo.AddLocation(location);
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
            return View();
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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