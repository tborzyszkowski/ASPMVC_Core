using MidiMarket.DAL;
using MidiMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidiMarket.Controllers
{
    // Kontroler koszyka: Listing 8.14
    public class CartController : Controller
    {
        private MarketContext db = new MarketContext();

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Dodaje produkt do koszyka
        /// </summary>
        public RedirectToRouteResult AddToCart(Cart cart, int id, string returnUrl)
        {
            // odszukaj produkt po ID
            Product product = db.Products.FirstOrDefault(p => p.ID == id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// Kasuje wystąpienie produktu z koszyka
        /// </summary>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault( item => item.ID == productId);
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult Zamow()
        {
            return RedirectToAction("Buy", "Orders", new { cart = GetCart() } );
        }

        /// <summary>
        /// Odczytuje koszyk z sesji.
        /// </summary>
        /// przestałem z tego używać na korzyść modelBinderu
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        /// <summary>
        /// Generuje widok częściowy, dla ikonki podsumowania koszyka
        /// </summary>
        /// Cart cart korzysta ze zdefiniowanego łącznika - CartModelBinder
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}