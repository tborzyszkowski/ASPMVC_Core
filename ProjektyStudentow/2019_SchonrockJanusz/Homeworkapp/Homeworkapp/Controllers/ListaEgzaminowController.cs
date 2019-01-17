using Homeworkapp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Homeworkapp.Controllers
{
    [Authorize]
    public class ListaEgzaminowController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: ListaEgzaminow
        public ActionResult Index()
        {
            var egzaminy = db.Egzaminy.Include(p => p.Przedmiot);
            var egzaminySort = (from e in egzaminy
                                where e.Termin > DateTime.Now
                                orderby e.Termin
                                select e);
            ViewBag.Egzaminy = egzaminySort.ToList();
            ViewBag.IloscEgzaminow = egzaminySort.Count();
            return View();
        }
    }
}