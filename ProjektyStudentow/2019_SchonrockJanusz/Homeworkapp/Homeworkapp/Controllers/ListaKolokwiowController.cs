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
    public class ListaKolokwiowController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: ListaKolokwiow
        public ActionResult Index()
        {
            var kolokwia = db.Kolokwia.Include(p => p.Przedmiot);
            var kolokwiaSort = (from k in kolokwia
                                where k.Termin > DateTime.Now
                                orderby k.Termin
                                select k);
            ViewBag.Kolokwia = kolokwiaSort.ToList();
            ViewBag.IloscKolokwiow = kolokwiaSort.Count();
            return View();
        }
    }
}