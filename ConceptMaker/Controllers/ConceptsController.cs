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
    public class ConceptsController : Controller
    {
        private ConceptMakerContext db = new ConceptMakerContext();

        // GET: Concepts
        
        public ActionResult Index(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var concept = from s in db.Concepts
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    concept = concept.OrderByDescending(s => s.Name);
                    break;
           
                default:
                    concept = concept.OrderBy(s => s.Name);
                    break;
            }
            return View(concept.ToList());
        }

        // GET: Concepts/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = db.Concepts.Find(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", concept.CategoryId);
            
            return View(concept);
        }

        // GET: Concepts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Concepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Concepts.Add(concept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", concept.CategoryId);
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", concept.CategoryId);
            return View(concept);
        }

        // GET: Concepts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = db.Concepts.Find(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", concept.CategoryId);
            return View(concept);
        }
        [AllowAnonymous]
        public ContentResult GetCount()
        {
            var count = db.Concepts.Count();
            return Content(count.ToString());
        }

        // POST: Concepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", concept.CategoryId);
            return View(concept);
           
        }

        // GET: Concepts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concept concept = db.Concepts.Find(id);
            if (concept == null)
            {
                return HttpNotFound();
            }
            return View(concept);
        }

        // POST: Concepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concept concept = db.Concepts.Find(id);
            db.Concepts.Remove(concept);
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
