using Homeworkapp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: Panel
        public ActionResult Index()
        {
            // Zlicza ilosc zadan do wykonania od dziś do wyznaczonych terminów
            var zadania = db.Zadania;
            var zadaniaSort = (from z in zadania
                               where z.Termin > DateTime.Now
                               orderby z.Termin
                               select z);
            ViewBag.IloscZadan = zadaniaSort.Count();

            // Zlicza ilosc kolokwiów które się odbędą
            var kolokwia = db.Kolokwia;
            var kolokwiaSort = (from k in kolokwia
                               where k.Termin > DateTime.Now
                               orderby k.Termin
                               select k);
            ViewBag.IloscKolokwiow = kolokwiaSort.Count();

            // Zlicza ilosc egzaminów które się odbędą
            var egzaminy = db.Egzaminy;
            var egzaminySort = (from e in egzaminy
                                where e.Termin > DateTime.Now
                                orderby e.Termin
                                select e);
            ViewBag.IloscEgzaminow = egzaminySort.Count();

            return View();
        }
    }
}