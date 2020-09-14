using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Component //komponenty
    {
        public int Id { get; set; }
        [DisplayName("Nazwa polaczenia")]
        public String ComponentName { get; set; }
        [DisplayName("Id pojecia")]
        public int ConceptId { get; set; }
        [DisplayName("Id pod pojecia")]
        public int SubConceptId { get; set; }
        [DisplayName("Czy wymagane?")]
        public bool Required { get; set; }





        public virtual Concept Concept { get; set; }

        public virtual Concept SubConcept { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        /*
        public virtual ICollection<Skladowe> Skladowe { get; set; }
        public virtual ICollection<Reguly> Reguly { get; set; }

        public virtual Pojecia Pojecia { get; set; }*/
    }
}
 