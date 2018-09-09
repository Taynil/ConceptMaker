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
   
    public class LawsController : Controller
    {
        private ConceptMakerContext db = new ConceptMakerContext();

        // GET: Laws
        public ActionResult Index()
        {
            var laws = db.Laws.Include(l => l.CommonConcept).Include(l => l.Concept).Include(l => l.FirstComponent).Include(l => l.SecondComponent);
            return View(laws.ToList());
        }

        // GET: Laws/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Law law = db.Laws.Find(id);
            if (law == null)
            {
                return HttpNotFound();
            }
            return View(law);
        }

        // GET: Laws/Create
        public ActionResult Create()
        {
            ViewBag.CommonConceptId = new SelectList(db.Concepts, "Id", "Name");
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name");
            ViewBag.FirstComponentId = new SelectList(db.Components, "Id", "Id");
            ViewBag.SecondComponentId = new SelectList(db.Components, "Id", "Id");
            return View();
        }

        // POST: Laws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ConceptId,FirstComponentId,SecondComponentId,CommonConceptId")] Law law)
        {
            if (ModelState.IsValid)
            {
                db.Laws.Add(law);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommonConceptId = new SelectList(db.Concepts, "Id", "Name", law.CommonConceptId);
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", law.ConceptId);
            ViewBag.FirstComponentId = new SelectList(db.Components, "Id", "Id", law.FirstComponentId);
            ViewBag.SecondComponentId = new SelectList(db.Components, "Id", "Id", law.SecondComponentId);
            return View(law);
        }

        // GET: Laws/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Law law = db.Laws.Find(id);
            if (law == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommonConceptId = new SelectList(db.Concepts, "Id", "Name", law.CommonConceptId);
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", law.ConceptId);
            ViewBag.FirstComponentId = new SelectList(db.Components, "Id", "Id", law.FirstComponentId);
            ViewBag.SecondComponentId = new SelectList(db.Components, "Id", "Id", law.SecondComponentId);
            return View(law);
        }

        // POST: Laws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ConceptId,FirstComponentId,SecondComponentId,CommonConceptId")] Law law)
        {
            if (ModelState.IsValid)
            {
                db.Entry(law).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommonConceptId = new SelectList(db.Concepts, "Id", "Name", law.CommonConceptId);
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", law.ConceptId);
            ViewBag.FirstComponentId = new SelectList(db.Components, "Id", "Id", law.FirstComponentId);
            ViewBag.SecondComponentId = new SelectList(db.Components, "Id", "Id", law.SecondComponentId);
            return View(law);
        }

        // GET: Laws/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Law law = db.Laws.Find(id);
            if (law == null)
            {
                return HttpNotFound();
            }
            return View(law);
        }

        // POST: Laws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Law law = db.Laws.Find(id);
            db.Laws.Remove(law);
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
