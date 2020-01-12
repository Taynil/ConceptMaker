
using ConceptMaker.DAL;
using System.Web;
using System.Web.Mvc;
using ConceptMaker.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConceptMaker.Logic
{
    public partial class ShoppingCart
    {


        private ConceptMakerContext db = new ConceptMakerContext();
        public string ShoppingCartId { get; set; }

        // klucz sesji
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
                cart.ShoppingCartId = cart.GetCartId(context);
                return cart;
        }
        // Helper method to simplify shopping cart calls
        //public static ShoppingCart GetCart(Controller controller)
        //{
        //    return GetCart(controller.HttpContext);
        //}

        //AddToCart instancji jako parametr przyjmuje i dodaje ja do
        //    koszyka użytkownika.Ponieważ w tabeli koszyka śledzi ilości 
        //        dla każdego pojecia, zawiera logikę w celu utworzenia nowego wiersza, 
        //        jeśli to konieczne lub po prostu zwiększyć ilość, jeśli 
        //        użytkownik ma już uporządkowane jedną kopię instancji.

        public void AddToCart(Instance instance)
        {
            // Get the matching cart and album instances
            var cartItem = db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                //&& c.AlbumId == album.AlbumId);
                && c.InstanceId == instance.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    InstanceId = instance.Id,
                    CartId = ShoppingCartId

                };
                db.ShoppingCartItems.Add(cartItem);
            }

            // Save changes
            db.SaveChanges();
        }

        //RemoveFromCart przyjmuje identyfikator pojecia i
        //    usuwa go z koszyka użytkownika.Jeśli użytkownik ma 
        //        tylko jedną kopię pojecia  w ich koszyku, wiersza są usuwane.
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.ShoppingCartItems.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ItemId == id);

            int itemCount = 0;

            //if (cartItem != null)
            //{
            //    if (cartItem.Count > 1)
            //    {
            //        cartItem.Count--;
            //        itemCount = cartItem.Count;
            //    }
            //    else
            // {
            db.ShoppingCartItems.Remove(cartItem);
            //  }
            // Save changes
            db.SaveChanges();

            return itemCount;
        }
        //EmptyCart spowoduje usunięcie wszystkich elementów z koszyka użytkownika.
        public void EmptyCart()
        {
            var cartItems = db.ShoppingCartItems.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.ShoppingCartItems.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }
        //GetCart jest metoda statyczna,
        //    co pozwala naszym kontrolerów, 
        //    aby uzyskać obiekt koszyka.Używa ona GetCartId metody, 
        //        aby obsłużyć odczyt CartId z sesji użytkownika.
        //        Metoda GetCartId wymaga element HttpContextBase 
        //        przeczytasz CartId użytkownika z sesji użytkownika.

        

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public List<CartItem> GetCartItems()
        {
            return db.ShoppingCartItems.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId);

            foreach (CartItem item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }
    }
}
        //public int GetCount()
        //{
        //    // Get the count of each item in the cart and sum them up
        //    int? count = (from cartItems in storeDB.Carts
        //                  where cartItems.CartId == ShoppingCartId
        //                  select (int?)cartItems.Count).Sum();
        //    // Return 0 if all entries are null
        //    return count ?? 0;
        //}
        //public decimal GetTotal()
        //{
        //    // Multiply album price by count of that album to get 
        //    // the current price for each of those albums in the cart
        //    // sum all album price totals to get the cart total
        //    decimal? total = (from cartItems in storeDB.Carts
        //                      where cartItems.CartId == ShoppingCartId
        //                      select (int?)cartItems.Count *
        //                      cartItems.Album.Price).Sum();

        //    return total ?? decimal.Zero;
        //}
        //public int CreateOrder(Order order)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();
        //    // Iterate over the items in the cart, 
        //    // adding the order details for each
        //    foreach (var item in cartItems)
        //    {
        //        var orderDetail = new OrderDetail
        //        {
        //            AlbumId = item.AlbumId,
        //            OrderId = order.OrderId,
        //            UnitPrice = item.Album.Price,
        //            Quantity = item.Count
        //        };
        //        // Set the order total of the shopping cart
        //        orderTotal += (item.Count * item.Album.Price);

        //        storeDB.OrderDetails.Add(orderDetail);

        //    }
        //    // Set the order's total to the orderTotal count
        //    order.Total = orderTotal;

        //    // Save the order
        //    storeDB.SaveChanges();
        //    // Empty the shopping cart
        //    EmptyCart();
        //    // Return the OrderId as the confirmation number
        //    return order.OrderId;
        //}
        // We're using HttpContextBase to allow access to cookies.

        //    // When a user has logged in, migrate their shopping cart to
        //    // be associated with their username
        //    public void MigrateCart(string userName)
        //    {
        //        var shoppingCart = storeDB.Carts.Where(
        //            c => c.CartId == ShoppingCartId);

        //        foreach (Cart item in shoppingCart)
        //        {
        //            item.CartId = userName;
        //        }
        //        storeDB.SaveChanges();
        //    }
        //}
    //}