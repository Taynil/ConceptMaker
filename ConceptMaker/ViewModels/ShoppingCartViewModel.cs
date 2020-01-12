using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConceptMaker.Models;
using ConceptMaker.Logic;



namespace ConceptMaker.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
    }
}