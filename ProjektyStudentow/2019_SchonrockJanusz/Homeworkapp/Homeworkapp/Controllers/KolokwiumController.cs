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
    public class KolokwiumController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: Kolokwium
        public ActionResult Index()
        {
            var kolokwia = db.Kolokwia.Include(k => k.Przedmiot);
            return View(kolokwia.ToList());
        }

        // GET: Kolokwium/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kolokwium kolokwium = db.Kolokwia.Find(id);
            if (kolokwium == null)
            {
                return HttpNotFound();
            }
            return View(kolokwium);
        }

        // GET: Kolokwium/Create
        public ActionResult Create()
        {
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa");
            return View();
        }

        // POST: Kolokwium/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KolokwiumID,PrzedmiotID,Termin,Opis,Url")] Kolokwium kolokwium)
        {
            if (ModelState.IsValid)
            {
                db.Kolokwia.Add(kolokwium);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", kolokwium.PrzedmiotID);
            return View(kolokwium);
        }

        // GET: Kolokwium/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kolokwium kolokwium = db.Kolokwia.Find(id);
            if (kolokwium == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", kolokwium.PrzedmiotID);
            return View(kolokwium);
        }

        // POST: Kolokwium/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KolokwiumID,PrzedmiotID,Termin,Opis,Url")] Kolokwium kolokwium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kolokwium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", kolokwium.PrzedmiotID);
            return View(kolokwium);
        }

        // GET: Kolokwium/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kolokwium kolokwium = db.Kolokwia.Find(id);
            if (kolokwium == null)
            {
                return HttpNotFound();
            }
            return View(kolokwium);
        }

        // POST: Kolokwium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kolokwium kolokwium = db.Kolokwia.Find(id);
            db.Kolokwia.Remove(kolokwium);
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
