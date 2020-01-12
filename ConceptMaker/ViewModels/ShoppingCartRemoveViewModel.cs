using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConceptMaker.Models;
using ConceptMaker.Logic;

namespace ConceptMaker.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public int DeleteId { get; set; }
    }
}