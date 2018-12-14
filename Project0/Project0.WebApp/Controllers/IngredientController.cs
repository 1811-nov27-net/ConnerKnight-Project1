using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project0.Library;

namespace Project0.WebApp.Controllers
{
    public class IngredientController : Controller
    {

        public IDataRepository Repo { get; set; }

        public IngredientController(IDataRepository repo)
        {
            Repo = repo;
        }

        // GET: Ingredient
        public ActionResult Index()
        {
            return View(Repo.GetIngredients());
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            Ingredient ingredient = Repo.GetIngredient(id);
            return View(ingredient);
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ingredient ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repo.AddIngredient(ingredient);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Repo.GetIngredient(id));
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Ingredient ingredient)
        {
            try
            {
                ingredient.IngredientId = id;
                if (Repo.UpdateIngredient(ingredient))
                {
                    return RedirectToAction(nameof(Index));
                }
                //if get here then the ingredient wasn't found
                return View();

            }
            catch
            {
                return View();
            }
        }


        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            Ingredient ingredient = Repo.GetIngredient(id);
            if (ingredient != null)
            {
                return View(ingredient);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete check here
                Repo.DeleteIngredientId(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}