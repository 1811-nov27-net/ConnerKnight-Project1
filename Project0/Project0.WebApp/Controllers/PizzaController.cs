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
    public class PizzaController : Controller
    {

        public IDataRepository Repo { get; set; }

        public PizzaController(IDataRepository repo)
        {
            Repo = repo;
        }


        // GET: Pizza
        public ActionResult Index()
        {
            
            return View(Repo.GetPizzas());
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = Repo.GetPizza(id);
            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            return View(new PizzaIngredients {
                Ingredients = Repo.GetIngredients().Select(a => new FilterIngredient{Ingredient = Mapper.Map(a),Selected=false }).ToList() });
        }

        // POST: Pizza/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelPizza pizza, List<FilterIngredient> Ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<ModelIngredient> addedIng = new List<ModelIngredient>();
                    foreach(var i in Ingredients)
                    {
                        if (i.Selected)
                        {
                            addedIng.Add(i.Ingredient);
                        }
                    }
                    pizza.RequiredIng = addedIng;
                    Repo.AddPizza(Mapper.Map(pizza));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza pizza = Repo.GetPizza(id);
            return View(new PizzaIngredients { Pizza = Mapper.Map(pizza),Ingredients = Repo.GetIngredients().Select(a => new FilterIngredient { Ingredient = Mapper.Map(a), Selected = false }).ToList()
            });
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelPizza pizza, List<FilterIngredient> Ingredients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<ModelIngredient> addedIng = new List<ModelIngredient>();
                    foreach (var i in Ingredients)
                    {
                        if (i.Selected)
                        {
                            addedIng.Add(i.Ingredient);
                        }
                    }
                    pizza.RequiredIng = addedIng;
                    Repo.UpdatePizza(Mapper.Map(pizza));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = Repo.GetPizza(id);
            if (pizza != null)
            {
                return View(pizza);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //add checks to ensure delete succeeded
                Repo.DeletePizzaId(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}