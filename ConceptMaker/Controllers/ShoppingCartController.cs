using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConceptMaker.DAL;
using ConceptMaker.Models;
using ConceptMaker.Logic;
using ConceptMaker.ViewModels;

namespace ConceptMaker.Controllers
{

    [Authorize(Roles = "Client, Admin")]
    public class ShoppingCartController : Controller
    {
        private ConceptMakerContext db = new ConceptMakerContext();
        // MusicStoreEntities storeDB = new MusicStoreEntities();

        // GET: CartItems
        //public ActionResult Index()
        //{
        //    var shoppingCartItems = db.ShoppingCartItems.Include(c => c.Instance);
        //    return View(shoppingCartItems.ToList());
        //}
      
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
              
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        [HttpGet]
        public ActionResult AddToCart(int? id)
        {
            // Retrieve the album from the database
            var addedInstance = db.Instances
                .Single(instance => instance.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedInstance);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string instanceName = db.ShoppingCartItems
                .Single(item => item.ItemId == id).Instance.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "Element: " +  Server.HtmlEncode(instanceName) +
                    " zostal pomysle usuniety z koszyka ",
                 DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        //public ActionResult CartSummary()
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    ViewData["CartCount"] = cart.GetCount();
        //    return PartialView("CartSummary");
        //}
    }

    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        db.Dispose();
    //    }
    //    base.Dispose(disposing);
    //}
}




//// GET: CartItems/Details/5
//public ActionResult Details(string id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    CartItem cartItem = db.ShoppingCartItems.Find(id);
//    if (cartItem == null)
//    {
//        return HttpNotFound();
//    }
//    return View(cartItem);
//}


//public void AddToCart(Album album)
//{
//    // Get the matching cart and album instances
//    var cartItem = storeDB.Carts.SingleOrDefault(
//        c => c.CartId == ShoppingCartId
//        && c.AlbumId == album.AlbumId);

//    if (cartItem == null)
//    {
//        // Create a new cart item if no cart item exists
//        cartItem = new Cart
//        {
//            AlbumId = album.AlbumId,
//            CartId = ShoppingCartId,
//            Count = 1,
//            DateCreated = DateTime.Now
//        };
//        storeDB.Carts.Add(cartItem);
//    }
//    else
//    {
//        // If the item does exist in the cart, 
//        // then add one to the quantity
//        cartItem.Count++;
//    }
//    // Save changes
//    storeDB.SaveChanges();
//}
//GET: CartItems/Create
//public ActionResult Create()
//{
//    ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name");
//    ViewBag.InstanceId = new SelectList(db.Instances, "Id", "Name");
//    return View();
//}

//POST: CartItems/Create
//To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create([Bind(Include = "ItemId,CartId,InstanceId")] CartItem cartItem)
//{
//    if (ModelState.IsValid)
//    {
//        db.ShoppingCartItems.Add(cartItem);
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", cartItem.ConceptId);
//    ViewBag.InstanceId = new SelectList(db.Instances, "Id", "Name", cartItem.InstanceId);
//    return View(cartItem);
//}

//GET: CartItems/Edit/5
//public ActionResult Edit(string id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    CartItem cartItem = db.ShoppingCartItems.Find(id);
//    if (cartItem == null)
//    {
//        return HttpNotFound();
//    }
//    ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", cartItem.ConceptId);
//    ViewBag.InstanceId = new SelectList(db.Instances, "Id", "Name", cartItem.InstanceId);
//    return View(cartItem);
//}

//POST: CartItems/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit([Bind(Include = "ItemId,CartId,InstanceId")] CartItem cartItem)
//{
//    if (ModelState.IsValid)
//    {
//        db.Entry(cartItem).State = EntityState.Modified;
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    ViewBag.ConceptId = new SelectList(db.Concepts, "Id", "Name", cartItem.ConceptId);
//    ViewBag.InstanceId = new SelectList(db.Instances, "Id", "Name", cartItem.InstanceId);
//    return View(cartItem);
//}

//GET: CartItems/Delete/5
//public ActionResult Delete(string id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    CartItem cartItem = db.ShoppingCartItems.Find(id);
//    if (cartItem == null)
//    {
//        return HttpNotFound();
//    }
//    return View(cartItem);
//}

//POST: CartItems/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public ActionResult DeleteConfirmed(string id)
//{
//    CartItem cartItem = db.ShoppingCartItems.Find(id);
//    db.ShoppingCartItems.Remove(cartItem);
//    db.SaveChanges();
//    return RedirectToAction("Index");
//}

