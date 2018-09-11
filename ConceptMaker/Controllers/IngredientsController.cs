using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConceptMaker.DAL;
using ConceptMaker.Models;

namespace ConceptMaker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientsController : Controller
    {
        private ConceptMakerContext db = new ConceptMakerContext();

        // GET: Ingredients
        public ActionResult Index()
        {
            var ingredients = db.Ingredients.Include(i => i.BaseInstance).Include(i => i.Component).Include(i => i.SubInstance);
            return View(ingredients.ToList());
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            ViewBag.BaseInstanceId = new SelectList(db.Instances, "Id", "Name");
            ViewBag.ComponentId = new SelectList(db.Components, "Id", "Id");
            ViewBag.SubInstanceId = new SelectList(db.Instances, "Id", "Name");
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BaseInstanceId,ComponentId,SubInstanceId,NumberOfIngredients")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaseInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.BaseInstanceId);
            ViewBag.ComponentId = new SelectList(db.Components, "Id", "Id", ingredient.ComponentId);
            ViewBag.SubInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.SubInstanceId);
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.BaseInstanceId);
            ViewBag.ComponentId = new SelectList(db.Components, "Id", "Id", ingredient.ComponentId);
            ViewBag.SubInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.SubInstanceId);
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BaseInstanceId,ComponentId,SubInstanceId,NumberOfIngredients")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaseInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.BaseInstanceId);
            ViewBag.ComponentId = new SelectList(db.Components, "Id", "Id", ingredient.ComponentId);
            ViewBag.SubInstanceId = new SelectList(db.Instances, "Id", "Name", ingredient.SubInstanceId);
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
