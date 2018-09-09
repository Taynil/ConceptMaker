using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa uzytkownika")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Rola w systemie")]
        public int RoleId { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime RegisteredDate { get; set; }




        public virtual Role Role { get; set; }
    }
}