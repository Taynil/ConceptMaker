using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Ingredient //skladowe
    {
        public int Id { get; set; }
        [Display(Name = "Id instancji bazowej")]
        public int BaseInstanceId { get; set; }
        [Display(Name = "Id Komponentu")]
        public int ComponentId { get; set; }
        [Display(Name = "Id instancji skladowej")]
        public int SubInstanceId { get; set; }
        [Display(Name = "Liczba skladowych")]
        public int NumberOfIngredients { get; set; }


        public virtual Component Component { get; set; }
        public virtual Instance BaseInstance { get; set; }
        public virtual Instance SubInstance { get; set; }
        


        public virtual ICollection<Law> Laws { get; set; }
        public virtual ICollection<Component> Components { get; set; }

        /*
        public int ID { get; set; }
        public int Instancjia_podstawowa_ID { get; set; }
        public int KomponentID{ get; set; }
        public int Instancjia_Skladowa_ID { get; set; }
        public int LiczbaSkladowych { get; set; }

        //jedna instancja wiele skladowych
        //jeden komponent do skaldowych
        public virtual ICollection<Reguly> Reguly { get; set; }
        public virtual ICollection<Komponenty> Komponenty { get; set; }
        */
    }
}
 