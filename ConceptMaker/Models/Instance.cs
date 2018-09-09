using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Instance //instancje
    {

        public int Id { get; set; }
        public int ConceptId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; } //czy do pojecia? do skladowych tak
        public virtual Concept Concept { get; set; }

        /*
        public int ID { get; set; }
        public int PojecieID { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<Skladowe> Skladowe { get; set; } //czy do pojecia? do skladowych tak
        public virtual Pojecia Pojecia { get; set; }*/
    }
}
 