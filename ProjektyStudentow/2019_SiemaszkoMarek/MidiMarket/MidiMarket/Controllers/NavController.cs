using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MidiMarket.DAL;
using MidiMarket.Models;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace MidiMarket.Controllers
{
    /// <summary>
    /// Kontroler generujący rzeczywistą listę kategorii
    /// </summary>
    public class NavController : Controller
    {
        private MarketContext db = new MarketContext();

        // Implementacja metody Menu: Listing 8.7
        public PartialViewResult Menu(string category = null)
        {
            // informacja o bierzącej kategorii
            ViewBag.SelectedCategory = category;

            IQueryable<Category?> categories =  
                                                db.Products
                                                .Select(x => x.Category)
                                                .Distinct()
                                                .OrderBy(x => x)
                                                ;
            return PartialView(categories);
        }
    }
}