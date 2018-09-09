using ConceptMaker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConceptMaker.DAL
{
    public class ConceptMakerIntializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ConceptMakerContext>
    {
        protected override void Seed(ConceptMakerContext context)
        {

            /*public DbSet<Profile> Profiles { get; set; } */
            /*public DbSet<Instancje> Instancje { get; set; } +
            public DbSet<Komponenty> Komponenty { get; set;  } + 
            public DbSet<Pojecia> Pojecia { get; set; } +
            public DbSet<Reguly> Reguly { get; set; }
            public DbSet<Skladowe> Skladowe { get; set; } */

            var profiles = new List<Profile>
            {
                new Profile{ Username = "admin"},

            };
            profiles.ForEach(p => context.Profiles.Add(p));
            context.SaveChanges();


            var concepts = new List<Concept> //ok
        {
        new Concept { Name = "Plyta_Glowna" },
        new Concept { Name = "Gniazdo_Procesora" },
        new Concept { Name = "Procesor"},
        new Concept { Name = "Typ_Ramu" }
       };
            concepts.ForEach(s => context.Concepts.Add(s));
            context.SaveChanges();

            var components = new List<Component> //ok
        {
         new Component{ ConceptId= 1,  SubConceptId=2, Required=true }, //plyta > gniazdoproca
         new Component{ ConceptId= 1,  SubConceptId=4, Required=true }, //plyta > ram
         new Component{ ConceptId= 3,  SubConceptId=2, Required=true } // procseor > gniazdo
        };
            components.ForEach(s => context.Components.Add(s));
            context.SaveChanges();


            var instances = new List<Instance>
        {
         /*1*/    new Instance { ConceptId = 1, Name = "ASRock H310M-HDV", Description = "Plyta glwona H310" },
           /*2*/    new Instance { ConceptId = 1, Name = "MSI Z370 GAMING PLUS", Description = "Plyta glwona msi z370" },

           /*3*/    new Instance { ConceptId = 2, Name = "Socket 1151", Description = "gniazdo 1151" },
           /*4*/    new Instance { ConceptId = 2, Name = "Socket 1151 (Coffee Lake)", Description = "gniazdo 1151 (Coffee Lake)" },

            /*5*/   new Instance { ConceptId = 3, Name = "Intel Core i5-7400", Description = "Procesor i57400" },
           /*6*/    new Instance { ConceptId = 3, Name = "Intel Pentium G4560, 3.5GHz", Description = "procesor G4560" },

            /*7*/   new Instance { ConceptId = 4, Name = "DDR4", Description = "Typ RAM DDR4" },
             /*8*/  new Instance { ConceptId = 4, Name = "DDR3", Description = "Typ Ram DDR3" }


        };
            instances.ForEach(s => context.Instances.Add(s));
            context.SaveChanges();

            /*Instancjia_podstawowa_ID
             * public int KomponentID { get; set; }
            public int Instancjia_Skladowa_ID { get; set; }
             public int LiczbaSkladowych { get; set; }*/
            var ingredients = new List<Ingredient>
        {
            new Ingredient { BaseInstanceId = 1, ComponentId = 1, SubInstanceId = 3,NumberOfIngredients=1  }, //plyta 1 ma gniazdo 3
            new Ingredient { BaseInstanceId = 2, ComponentId = 1, SubInstanceId = 4,NumberOfIngredients=1  }, //plyta 2 ma gniazdo 4
            new Ingredient { BaseInstanceId = 5, ComponentId = 3, SubInstanceId = 3,NumberOfIngredients=1  }, //procseor 5> gniazdo 3
            new Ingredient { BaseInstanceId = 6, ComponentId = 3, SubInstanceId = 3,NumberOfIngredients=1  }, //procseor 6> gniazdo 3
            new Ingredient { BaseInstanceId = 1, ComponentId = 2, SubInstanceId = 7,NumberOfIngredients=2  }, //PLYTA 1 ma ram 7
            new Ingredient { BaseInstanceId = 2, ComponentId = 2, SubInstanceId = 7,NumberOfIngredients=4  } //PLYTA 2 ma ram 7


        };
            ingredients.ForEach(s => context.Ingredients.Add(s));
            context.SaveChanges();

            /*
             public int PojecieID { get; set; }
            public int Komponent_1_ID { get; set; }
            public int Komponent_2_ID { get; set; }
            public int Wspolne_Pojecie_ID { get; set; }*/

            var laws = new List<Law>
        {
            new Law { ConceptId = 1, FirstComponentId = 1, SecondComponentId = 3,CommonConceptId=2  }//plytaglowna, gniazdo, gniazdo procesora procesor i plyta maja wspolne pojecie
           



        };
            laws.ForEach(s => context.Laws.Add(s));
            context.SaveChanges();


            
            var roleManager = new RoleManager<IdentityRole>(
                   new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("admin"));
            var user = new ApplicationUser { UserName = "qwert.1@wp.pl" };
            string pass = "haslo1";

            userManager.Create(user, pass);
            userManager.AddToRole(user.Id, "admin");

    

        }
    }
}
