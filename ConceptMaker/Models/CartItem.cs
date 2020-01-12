using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConceptMaker.Models
{
    public class CartItem //koszyk
    {
        [Key]
        public int ItemId { get; set; }

        //[Display(Name = "Id koszyka uzytkownika")]
        public string CartId { get; set; }

        [Display(Name = "Id instancji")]
        public int InstanceId { get; set; }
        
        public virtual Instance Instance { get; set; }
     

    }
}