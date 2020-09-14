using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Law  //reguła ok
    {

        public int Id { get; set; }

        [DisplayName("Nazwa reguly")]
        public String LawName { get; set; }


        [DisplayName("Id pojecia")]
        public int ConceptId { get; set; }

        [DisplayName("Id pierwszego komponentu")]
        public int FirstComponentId { get; set; }

        [DisplayName("Id drugiego komponentu")]
        public int SecondComponentId { get; set; }


        [DisplayName("Id wspolnego pojecia")]
        public int CommonConceptId { get; set; }



        public virtual Concept Concept { get; set; }

        public virtual Concept CommonConcept { get; set; }

        public virtual Component FirstComponent { get; set; }

        public virtual Component SecondComponent { get; set; }

        /*
        public int ID { get; set; }
        public int PojecieID { get; set; }
        public int Komponent_1_ID { get; set; }
        public int Komponent_2_ID { get; set; }
        public int Wspolne_Pojecie_ID { get; set; }

        public virtual Pojecia Pojecia { get; set; }
        public virtual Komponenty Komponenty { get; set; }*/
    }
}
 