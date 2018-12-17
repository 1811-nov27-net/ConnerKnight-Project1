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

    public class UserController : Controller
    {

        public IDataRepository Repo { get; set; }

        public UserController(IDataRepository repo)
        {
            Repo = repo;
        }

        // GET: User
        public ActionResult Index()
        {
            return View(Repo.GetUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = Repo.GetUser(id);
            
            return View(new UserHistory { User=user,History=Repo.GetUserIdOrderHistory(id) });
        }

        public ActionResult Order(int id, string sorting)
        {
            User user = Repo.GetUser(id);
            List<Order> orderHistory = Repo.GetUserIdOrderHistory(id);
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

            return View("Details",new UserHistory { User = user, History = orderHistory });
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(new UserLocation { Locations = Repo.GetLocations()});
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user,UserLocation userLocation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Location location = Repo.GetLocationByName(userLocation.RawLocation);
                    user.DefaultLocation = location;
                    Repo.AddUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.UpdateUser(user);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            User user = Repo.GetUser(id);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //maybe should check to make sure delete succeeded
                //use a var success
                Repo.DeleteUserId(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}