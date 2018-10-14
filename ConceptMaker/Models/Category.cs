using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Category //kategoria
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }


        public virtual ICollection<Concept> Concepts { get; set; }
    }
}