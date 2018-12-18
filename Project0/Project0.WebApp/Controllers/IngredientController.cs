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
        public ActionResult Create(ModelIngredient ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!Repo.IngredientNameExists(ingredient.Name))
                    {
                        Repo.AddIngredient(new Ingredient { Name = ingredient.Name });
                    }
                    else
                    {
                        ModelState.AddModelError("Create", "ingredient already exists");
                        return View();
                    }
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
        public ActionResult Edit(int id, ModelIngredient ingredient)
        {
            try
            {
                ingredient.IngredientId = id;
                if (Repo.UpdateIngredient(new Ingredient { Name = ingredient.Name, IngredientId = ingredient.IngredientId}))
                {
                    return RedirectToAction(nameof(Index));
                }
                //if get here then the ingredient wasn't found
                ModelState.AddModelError("Edit", "ingredient doesnt exist");
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
                if (Repo.DeleteIngredientId(id))
                {
                    return RedirectToAction(nameof(Index));
                }

                
                ModelState.AddModelError("Delete", "ingredient doesnt exist");
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}