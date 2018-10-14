using ConceptMaker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConceptMaker.DAL
{
    public class ConceptMakerIntializer :DropCreateDatabaseIfModelChanges<ConceptMakerContext>
    {
        protected override void Seed(ConceptMakerContext context)
        {

            /*public DbSet<Profile> Profiles { get; set; } */
            /*public DbSet<Instancje> Instancje { get; set; } +
            public DbSet<Komponenty> Komponenty { get; set;  } + 
            public DbSet<Pojecia> Pojecia { get; set; } +
            public DbSet<Reguly> Reguly { get; set; }
            public DbSet<Skladowe> Skladowe { get; set; } */

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user1 = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            var user2 = new ApplicationUser { UserName = "user@gmail.com", Email = "user@gmail.com" };
            userManager.Create(user1, "Admin@123");
            userManager.Create(user2, "User@123");
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Client"));

            userManager.AddToRole(user1.Id, "Admin");
            userManager.AddToRole(user2.Id, "Client");
            var roles = new List<Role>
            {
                new Role { Name = "Admin", PolishName = "Admin"},
                new Role { Name = "Client", PolishName = "Klient"}
            };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();
            var profiles = new List<Profile>
            {
                new Profile
                {
                    Username = "admin@gmail.com",
                     RoleId = 1,
                    RegisteredDate = DateTime.Now
                },
                new Profile
                {
                    Username = "user@gmail.com",
                    RoleId = 2,
                    RegisteredDate = DateTime.Now
                }

            };

            var categories = new List<Category> //ok
        {
        new Category { Name = "Komputery" , Description = "Konfigurator kompa"},
        new Category { Name = "Mlotki" , Description = "Konfigurator mlotka"},
       
       };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var concepts = new List<Concept> //ok
        {
        new Concept { Name = "Plyta_Glowna", CategorytId =1 },
        new Concept { Name = "Gniazdo_Procesora", CategorytId =1 },
        new Concept { Name = "Procesor", CategorytId =1},
        new Concept { Name = "Typ_Ramu DDR_" , CategorytId =1},
        new Concept { Name = "Pamiec_RAM", CategorytId =1 }
       };
            concepts.ForEach(s => context.Concepts.Add(s));
            context.SaveChanges();

            var components = new List<Component> //ok
        {
         new Component{ ConceptId= 1,  SubConceptId=2, Required=true  }, //plyta > gniazdoproca
         new Component{ ConceptId= 1,  SubConceptId=4, Required=true }, //plyta > ram
         new Component{ ConceptId= 3,  SubConceptId=2, Required=true }, // procseor > gniazdo
         new Component{ ConceptId= 5,  SubConceptId=4, Required=true } // Ram > typ pamieci
        };
            components.ForEach(s => context.Components.Add(s));
            context.SaveChanges();


            var instances = new List<Instance>
        {
         /*1*/    new Instance { ConceptId = 1, Name = "ASRock H310M-HDV", Description = "Plyta glwona H310" },
           /*2*/    new Instance { ConceptId = 1, Name = "MSI Z370 GAMING PLUS", Description = "Plyta glwona msi z370" },
           /*3*/    new Instance { ConceptId = 1, Name = "Gigabyte Z370 AORUS Gaming K3", Description = "Plyta Gigabyte Z370 AORUS Gaming K3" },
           /*4*/    new Instance { ConceptId = 1, Name = "ASRock Z370 EXTREME 4", Description = "Plyta glwona ASRock Z370 EXTREME 4" },
           /*5*/    new Instance { ConceptId = 1, Name = "ASRock H110M DVS R3.0", Description = "Plyta glwona ASRock H110M DVS R3.0" },
           /*6*/    new Instance { ConceptId = 1, Name = "Gigabyte GA-H110M-S2H", Description = "Plyta glwona Gigabyte GA-H110M-S2H" },
           /*7*/    new Instance { ConceptId = 1, Name = "ASRock B250M-HDV", Description = "Plyta glwona ASRock B250M-HDV" },
           /*8*/    new Instance { ConceptId = 1, Name = "Gigabyte GA-B250M-D3H", Description = "Gigabyte GA-B250M-D3H" },

           /*9*/    new Instance { ConceptId = 2, Name = "Socket 1151", Description = "gniazdo 1151" },
           /*10*/    new Instance { ConceptId = 2, Name = "Socket 1151 (Coffee Lake)", Description = "gniazdo 1151 (Coffee Lake)" },
            /*11*/    new Instance { ConceptId = 2, Name = "Socket AM4", Description = "gniazdo Socket AM4" },


            /*12*/   new Instance { ConceptId = 3, Name = "Intel Core i5-7400", Description = "Procesor i57400" },
           /*13*/    new Instance { ConceptId = 3, Name = "Intel Pentium G4560, 3.5GHz", Description = "procesor G4560" },
           /*14*/   new Instance { ConceptId = 3, Name = "AMD Ryzen 3 2200G, 3.5GHz, 4MB", Description = "Procesor AMD Ryzen 3 2200G, 3.5GHz, 4MB" },
           /*15*/    new Instance { ConceptId = 3, Name = "AMD Ryzen 5 1400, 3.2GHz, 8MB", Description = "procesor AMD Ryzen 5 1400, 3.2GHz, 8MB" },
           /*16*/   new Instance { ConceptId = 3, Name = "Intel Core i3-7100, 3.9GHz, 3MB BOX", Description = "Procesor Intel Core i3-7100, 3.9GHz, 3MB BOX" },
           /*17*/    new Instance { ConceptId = 3, Name = "Intel Core i7-8700, 3.20GHz, 12MB", Description = "Intel Core i7-8700, 3.20GHz, 12MB" },



            /*18*/   new Instance { ConceptId = 4, Name = "DDR4", Description = "Typ RAM DDR4" },
             /*19*/  new Instance { ConceptId = 4, Name = "DDR3", Description = "Typ Ram DDR3" },

             /*20*/   new Instance { ConceptId = 5, Name = "HyperX Predator DDR4, 2x8GB, 3000MHz", Description = "Pamięć HyperX Predator DDR4, 2x8GB, 3000MHz" },
             /*21*/  new Instance { ConceptId = 5, Name = "ADATA XPG Z1 DDR4 2666 DIMM 8GB CL16 Red", Description = "Pamięć ADATA XPG Z1 DDR4 2666 DIMM 8GB CL16 Red" },
             /*22*/   new Instance { ConceptId = 5, Name = "Ballistix Sport LT, DDR4, 2x4GB, 2666MHz, CL16", Description = "Pamięć Ballistix Sport LT, DDR4, 2x4GB, 2666MHz" },

            /*23*/    new Instance { ConceptId = 1, Name = "Płyta główna MSI B350 TOMAHAWK", Description = "Płyta główna MSI B350 TOMAHAWK "},





        };
            instances.ForEach(s => context.Instances.Add(s));
            context.SaveChanges();

            /*Instancjia_podstawowa_ID
             * public int KomponentID { get; set; }
            public int Instancjia_Skladowa_ID { get; set; }
             public int LiczbaSkladowych { get; set; }*/
            var ingredients = new List<Ingredient>
        {
            new Ingredient { BaseInstanceId = 1, ComponentId = 1, SubInstanceId = 9,NumberOfIngredients=1  }, //plyta 1 ma gniazdo 9
            new Ingredient { BaseInstanceId = 2, ComponentId = 1, SubInstanceId = 9,NumberOfIngredients=1  }, //plyta 2 ma gniazdo 9
            new Ingredient { BaseInstanceId = 3, ComponentId = 1, SubInstanceId = 9,NumberOfIngredients=1  }, //plyta 3 ma gniazdo 9
            new Ingredient { BaseInstanceId = 4, ComponentId = 1, SubInstanceId = 9,NumberOfIngredients=1  }, //plyta 4 ma gniazdo 9
            new Ingredient { BaseInstanceId = 5, ComponentId = 1, SubInstanceId = 9,NumberOfIngredients=1  }, //plyta 5 ma gniazdo 9
            new Ingredient { BaseInstanceId = 6, ComponentId = 1, SubInstanceId = 10,NumberOfIngredients=1  }, //plyta 6 ma gniazdo 10
            new Ingredient { BaseInstanceId = 7, ComponentId = 1, SubInstanceId = 10,NumberOfIngredients=1  }, //plyta 7 ma gniazdo 10
            new Ingredient { BaseInstanceId = 8, ComponentId = 1, SubInstanceId = 10,NumberOfIngredients=1  }, //plyta 8 ma gniazdo 10
            new Ingredient { BaseInstanceId = 23, ComponentId = 1, SubInstanceId = 11,NumberOfIngredients=1  }, //plyta 23 ma gniazdo 11

            new Ingredient { BaseInstanceId = 12, ComponentId = 3, SubInstanceId = 9,NumberOfIngredients=1  }, //procseor 12> gniazdo 9
            new Ingredient { BaseInstanceId = 13, ComponentId = 3, SubInstanceId = 9,NumberOfIngredients=1  }, //procseor 13> gniazdo 9
            new Ingredient { BaseInstanceId = 14, ComponentId = 3, SubInstanceId = 11,NumberOfIngredients=1  }, //procseor 14> gniazdo 11
            new Ingredient { BaseInstanceId = 15, ComponentId = 3, SubInstanceId = 11,NumberOfIngredients=1  }, //procseor 15> gniazdo 11
            new Ingredient { BaseInstanceId = 16, ComponentId = 3, SubInstanceId = 10,NumberOfIngredients=1  }, //procseor 16> gniazdo 10
            new Ingredient { BaseInstanceId = 17, ComponentId = 3, SubInstanceId = 10,NumberOfIngredients=1  }, //procseor 17> gniazdo 10



            new Ingredient { BaseInstanceId = 1, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=2  }, //PLYTA 1 ma ram 18
            new Ingredient { BaseInstanceId = 2, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=2  }, //PLYTA 2 ma ram 18
            new Ingredient { BaseInstanceId = 3, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=2  }, //PLYTA 3 ma ram 18
            new Ingredient { BaseInstanceId = 4, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=4  }, //PLYTA 4 ma ram 18
            new Ingredient { BaseInstanceId = 5, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=4  }, //PLYTA 5 ma ram 18
            new Ingredient { BaseInstanceId = 6, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=4  }, //PLYTA 6 ma ram 18
            new Ingredient { BaseInstanceId = 7, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=4  }, //PLYTA 7 ma ram 18
            new Ingredient { BaseInstanceId = 8, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=4  }, //PLYTA 8 ma ram 18
            new Ingredient { BaseInstanceId = 23, ComponentId = 2, SubInstanceId = 18,NumberOfIngredients=2  }, //plyta 23 ma ram 18

            new Ingredient { BaseInstanceId = 20, ComponentId = 4, SubInstanceId = 18,NumberOfIngredients=1  }, //RAM 20 ma TYP 18
            new Ingredient { BaseInstanceId = 21, ComponentId = 4, SubInstanceId = 18,NumberOfIngredients=1  }, //RAM 21 ma typ 18
            new Ingredient { BaseInstanceId = 22, ComponentId = 4, SubInstanceId = 18,NumberOfIngredients=1  }, //RAM 22 ma typ 18

           


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


            base.Seed(context);
          
    

        }
    }
}
