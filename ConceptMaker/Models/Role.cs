using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa Roli")]
         public string Name { get; set; }

        [Required]
        [DisplayName("Polska Nazwa Roli")]
        public string PolishName { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}