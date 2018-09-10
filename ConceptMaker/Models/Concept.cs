using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Concept //pojecie
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nazwa Pojecia")]
        public string Name { get; set; }

        public virtual ICollection<Law> Laws { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<Instance> Instances { get; set; }

    }
}