using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MidiMarket.DAL;
using MidiMarket.Models;

namespace MidiMarket.DAL
{
    public class OrdersController : Controller
    {
        private MarketContext db = new MarketContext();

        /// <summary>
        /// obecnie zalogowany uzytkownik
        /// </summary>
        /// <returns></returns>
        public ApplicationUser getLoggedUser()
        {
            // logged username
            string loggedUser = User.Identity.Name;
            if (loggedUser == "") return null;

            // logged user
            ApplicationUser currentUser = db.Users
                            .Where(usr => usr.UserName == loggedUser)
                            .FirstOrDefault()
                            ;
            return currentUser;
        }

         

        /// <summary>
        /// Podawanie danych wysylki
        /// </summary>
        public ActionResult Checkout(Cart cart)
        {
            // jezeli jest zalogowany
            if (User.Identity.IsAuthenticated)
            {
                // jezeli koszyk pusty
                if ( cart.Lines.Count() == 0 )
                {
                    return View("CartIsEmpty");
                }

                // wygeneruj form na dane wysylki
                return View(new ShippingDetails());
            }
            return View("YourAreNotLoggedIn");
        }


        /// <summary>
        /// Obsługa formularza z danymi wysyłki
        /// </summary>
        [Authorize][HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                // teoretycznie tutaj nei powinno dojsc
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }

            // czy uzytkownik zalogowany
            if (User.Identity.IsAuthenticated)
            {
                // czy przesłany form jest poprawny
                if (true || ModelState.IsValid)
                {
                    if (ProcessOrder(cart, shippingDetails) == true)
                    {
                        cart.Clear();
                    }
                    return View("Completed");
                }
                else
                {
                    //jezeli form niepoprawny to ponownie wygeneruj formularz z danymi
                    return View(shippingDetails);
                }
            }
            else
            return View("YourAreNotLoggedIn");
        }


        /// <summary>
        /// realizacja zamówienia
        /// </summary>
        private bool ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            ApplicationUser currentUser = getLoggedUser();

            // nowe zamowienie
            var order = new Order
            {
                ApplicationUser = currentUser,
                OrderDate = DateTime.Now,
                ShipingName = shippingDetails.Name,
                ShipingAddress = shippingDetails.Address,
                ShipingCity = shippingDetails.City,
                ShipingZip = shippingDetails.Zip,
                ShipingCountry = shippingDetails.Country
            };
            db.Orders.Add(order);

            //dodaj każdą z pozycji do zamowienia
            foreach (var line in cart.Lines)
            {
                db.OrderItems.Add(
                    new OrderItem { Order = order, Product = line.Product, Amount = line.Quantity }
                    );
            }
            db.SaveChanges();
            return true;
        }


        /// <summary>
        /// Wyswietla liste zamowien
        /// </summary>
        [Authorize]
        public ViewResult ListaZamowien()
        {
            var currentUser = getLoggedUser();
            var userRole = currentUser.Roles;

            List<Order> Orders;
            if (User.IsInRole("Admin"))
            {
                Orders = db.Orders
                        .Include(items => items.OrderItems.Select(product => product.Product))
                        .ToList();
            }
            else
            {
                Orders = db.Orders
                    .Include(items => items.OrderItems.Select(product => product.Product))
                    .Where(x => x.ApplicationUser.UserName == currentUser.UserName)
                    .ToList()
                    ;
            }

            ViewBag.Orders = Orders;
            return View();
        }

    }
}
