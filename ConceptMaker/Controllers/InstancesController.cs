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
using PagedList;

namespace ConceptMaker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstancesController : Controller
    {
        private ConceptMakerContext db = new ConceptMakerContext();

        // GET: Instances
        
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc1" : "";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var instances = from s in db.Instances.Include(i => i.Concept) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                instances = instances.Where(s => s.Name.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc1":
                    instances = instances.OrderByDescending(s => s.Name);
                    break;
                 default:
                    instances = instances.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(instances.ToPagedList(pageNumber, pageSize));
            //return View(instances.ToList());
        }

        // GET: Instances/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instance instance = db.Instances.Find(id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            return View(instance);
        }

        // GET: Instances/Create
        
        public ActionResult Create()
        {
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name");
            return View();
        }

        // POST: Instances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ConceptId,Name,Description")] Instance instance)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(instance);
                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if (file != null && file.ContentLength > 0)
                {
                    instance.Obrazek = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Obrazki/") + instance.Obrazek);
                }

                db.Instances.Add(instance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", instance.ConceptId);
            return View(instance);
        }

        // GET: Instances/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instance instance = db.Instances.Find(id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", instance.ConceptId);
            return View(instance);
        }

        // POST: Instances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ConceptId,Name,Description")] Instance instance)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(instance);
                HttpPostedFileBase file = Request.Files["plikZObrazkiem"];
                if (file != null && file.ContentLength > 0)
                {
                    instance.Obrazek = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Obrazki/") + instance.Obrazek);
                }
                db.Entry(instance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", instance.ConceptId);
            return View(instance);
        }

        // GET: Instances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instance instance = db.Instances.Find(id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            return View(instance);
        }

        // POST: Instances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instance instance = db.Instances.Find(id);
            db.Instances.Remove(instance);
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
