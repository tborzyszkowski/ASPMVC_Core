using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homeworkapp.DAL;
using Homeworkapp.Models;

namespace Homeworkapp.Controllers
{
    [Authorize(Roles = "Administrator,Moderator")]
    public class ZadanieController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: Zadanie
        public ActionResult Index()
        {
            var zadania = db.Zadania.Include(z => z.Przedmiot).Include(z => z.Zjazd);
            var zadaniaSort = (from z in zadania
                               orderby z.ZadanieID descending
                               select z);
            return View(zadaniaSort.ToList());
        }

        // GET: Zadanie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadanie zadanie = db.Zadania.Find(id);
            if (zadanie == null)
            {
                return HttpNotFound();
            }
            return View(zadanie);
        }

        // GET: Zadanie/Create
        public ActionResult Create()
        {
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa");
            ViewBag.ZjazdID = new SelectList(db.Zjazdy, "ZjazdID", "Numer");
            return View();
        }

        // POST: Zadanie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZadanieID,ZjazdID,PrzedmiotID,Opis,Url,Termin")] Zadanie zadanie)
        {
            if (ModelState.IsValid)
            {
                db.Zadania.Add(zadanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", zadanie.PrzedmiotID);
            ViewBag.ZjazdID = new SelectList(db.Zjazdy, "ZjazdID", "Numer", zadanie.ZjazdID);
            return View(zadanie);
        }

        // GET: Zadanie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadanie zadanie = db.Zadania.Find(id);
            if (zadanie == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", zadanie.PrzedmiotID);
            ViewBag.ZjazdID = new SelectList(db.Zjazdy, "ZjazdID", "Numer", zadanie.ZjazdID);
            return View(zadanie);
        }

        // POST: Zadanie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZadanieID,ZjazdID,PrzedmiotID,Opis,Url,Termin")] Zadanie zadanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zadanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", zadanie.PrzedmiotID);
            ViewBag.ZjazdID = new SelectList(db.Zjazdy, "ZjazdID", "Numer", zadanie.ZjazdID);
            return View(zadanie);
        }

        // GET: Zadanie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zadanie zadanie = db.Zadania.Find(id);
            if (zadanie == null)
            {
                return HttpNotFound();
            }
            return View(zadanie);
        }

        // POST: Zadanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zadanie zadanie = db.Zadania.Find(id);
            db.Zadania.Remove(zadanie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
