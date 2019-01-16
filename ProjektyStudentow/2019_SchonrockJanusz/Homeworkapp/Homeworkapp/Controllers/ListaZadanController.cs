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
    public class ListaZadanController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: ListaZadan
        public ActionResult Index()
        {
            var zadania = db.Zadania.Include(p => p.Przedmiot);
            var zadaniaSort = (from z in zadania
                               where z.Termin > DateTime.Now
                               orderby z.Termin
                               select z);
            ViewBag.Zadania = zadaniaSort.ToList();
            ViewBag.IloscZadan = zadaniaSort.Count();
            return View();
        }
    }
}