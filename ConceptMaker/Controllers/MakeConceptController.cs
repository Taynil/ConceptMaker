using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ConceptMaker.DAL;
using ConceptMaker.Models;
using PagedList;

namespace ConceptMaker.Controllers
{
   [Authorize(Roles = "Client, Admin")]
    public class MakeConceptController : Controller

    {

        private ConceptMakerContext db = new ConceptMakerContext();
        

        //start index wybor pojecia startowego
        //public ActionResult Index(string sortOrder)
        //{

        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
        //    var concept = from s in db.Concepts
        //                  select s;
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            concept = concept.OrderByDescending(s => s.Name);
        //            break;

        //        default:
        //            concept = concept.OrderBy(s => s.Name);
        //            break;
        //    }
        //    return View(concept.ToList());
        //}
        public ActionResult Index(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var category = from s in db.Categories
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    category = category.OrderByDescending(s => s.Name);
                    break;

                default:
                    category = category.OrderBy(s => s.Name);
                    break;
            }
            return View(category.ToList());
        }

        [HttpGet]
        public ActionResult ChoseCategory(int? id)
        {
            // List<Ingredient> lista = new List<Ingredient>();
            // var IngredientList = db.Ingredients.Where(r => r.BaseInstanceId == id).ToList();

            //var InstancesList = db.Instances.Where(r => r.ConceptId == id).ToList();
            var ConceptList = db.Concepts.Where(r => r.CategoryId == id).ToList();

            // return View(InstancesList);
            return View(ConceptList);

        }


        [HttpGet]
        public ActionResult ChoseInstance(int? id)
        {

            var InstancesList = db.Instances.Where(r => r.ConceptId == id).ToList();

            return View(InstancesList);
        
        }


        [HttpGet]
        public ActionResult ChoseIngredients(int? id, int? idconceptu, string sortOrder, string currentFilter, string searchString, int? page)
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

            

            List<Ingredient> lista = new List<Ingredient>();
            var IngredientList = db.Ingredients.Where(r => r.BaseInstanceId == id).ToList();
            foreach (var item in IngredientList)
            {
                var list = db.Ingredients.Where(i => i.SubInstanceId == item.SubInstanceId && i.BaseInstance.ConceptId != idconceptu).ToList();
                lista.AddRange(list);
            }

            var ingredients = from s in lista select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ingredients = ingredients.Where(s => s.BaseInstance.Name.Contains(searchString)
                 || s.SubInstance.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc1":
                    ingredients = ingredients.OrderByDescending(s => s.BaseInstance.Name);
                    break;
                
                default:
                    ingredients = ingredients.OrderBy(s => s.BaseInstance.Name);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(ingredients.ToPagedList(pageNumber, pageSize));



           // return View(lista);
        }
        [Authorize(Roles = "Client,Admin")]
        [HttpGet]
        public ActionResult SendMail(int? id)
        {
            var ingredient = db.Ingredients.SingleOrDefault(i => i.Id == id);
            string nazwa = ingredient.BaseInstance.Name;

            
            var message = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["sender"], "programowaniezaawansowane1@gmail.com")

            {
                Subject = "Nowe wyszukanie!",
                Body = "Wyszukano konfigurację dla: " + nazwa
            };

            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["sender"],
                    ConfigurationManager.AppSettings["passwd"]),
                EnableSsl = true
            };
            //google nie pozwala sie zalogowac
            //wiec chwilowo nie da sie wyslac @ 
            //smtpClient.Send(message);
            


            return RedirectToAction("Index");
        }
    }
}