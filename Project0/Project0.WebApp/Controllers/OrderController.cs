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

        // GET: Order/Create
        public ActionResult Create()
        {
            return View(new OrderMaster
            {
                Users = Repo.GetUsers(),
                Locations = Repo.GetLocations(),
                Pizzas = Repo.GetPizzas().Select(a => new PizzaMultiple { Pizza = a,Quantity = 0}).ToList()
            });
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Repo.AddOrder(order);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(BadOrderException e)
            {
                //do something with the bad order exception
                return View();
            }
            catch
            {
                return View();
            }
        }


        //probably won't want to allow deleting order

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            /*
            Order order = Repo.GetOrder(id);
            if(order != null)
            {
                return View();
            }
            */

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


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}