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

       
      
        [HttpGet]
        public ActionResult ChoseInstance(int? id)
        {

            var InstancesList = db.Instances.Where(r => r.ConceptId == id).ToList();

            return View(InstancesList);
        
        }


        [HttpGet]
        public ActionResult ChoseIngredients(int? id, int? idconceptu)
        {
            List<Ingredient> lista = new List<Ingredient>();
            var IngredientList = db.Ingredients.Where(r => r.BaseInstanceId == id).ToList();
            foreach (var item in IngredientList)
            {
                var list = db.Ingredients.Where(i => i.SubInstanceId == item.SubInstanceId && i.BaseInstance.ConceptId != idconceptu).ToList();
                lista.AddRange(list);
            }


            return View(lista);
        }
    }
}