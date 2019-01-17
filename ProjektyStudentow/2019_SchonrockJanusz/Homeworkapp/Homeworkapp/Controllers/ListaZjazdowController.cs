using Homeworkapp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Homeworkapp.Controllers
{
    [Authorize]
    public class ListaZjazdowController : Controller
    {

        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: ListaZjazdow
        public ActionResult Index()
        {
            var zjazdy = db.Zjazdy;
            var zjazdySort = (from z in zjazdy
                               where z.Dzien1 < DateTime.Now
                               orderby z.ZjazdID
                               select z);
            ViewBag.Zjazdy = zjazdySort.ToList();
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var zadania = db.Zadania.Include(p => p.Przedmiot);
            var zadaniaZjazd = (from z in zadania
                                where z.ZjazdID == id
                                select z);
            ViewBag.Zadania = zadaniaZjazd.ToList();

            var zjazdy = db.Zjazdy;
            var dataZjazdu = (from d in zjazdy
                              where d.ZjazdID == id
                              select d);
            ViewBag.Data = dataZjazdu.ToList();
            

            return View();
        }
    }
}