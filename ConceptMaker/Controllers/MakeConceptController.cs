﻿using System;
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
   // [Authorize(Roles = "Client")]
    public class MakeConceptController : Controller

    {

        private ConceptMakerContext db = new ConceptMakerContext();
        

        //start index wybor pojecia startowego
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

       
        /*
        public ActionResult ChoseInstance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var InstancesList = db.Instances.Where(r=> r.ConceptId==id).ToList();
     
            return View(InstancesList);
        }
        */
        [HttpGet]
        public ActionResult ChoseInstance(int? id)
        {

            var InstancesList = db.Instances.Where(r => r.ConceptId == id).ToList();

            // ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", component.ConceptId);
            // ViewBag.SubConceptId = new SelectList(db.Concepts, "Id", "Name", component.SubConceptId);
            return View(InstancesList);
            // return View(InstancesList);
        }
        //otrzymane pojecie postem  wersja w chuj 10
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChoseInstance([Bind(Include = "Id,ConceptId,SubConceptId,Required")]Instance instance)
        {
           
            var instances = db.Instances.Where(r=>r.ConceptId==.Find("ConceptId")).ToList();

            // ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", component.ConceptId);
            // ViewBag.SubConceptId = new SelectList(db.Concepts, "Id", "Name", component.SubConceptId);
           return View(instances);
           // return View(InstancesList);
        }
        */
        
    }
}