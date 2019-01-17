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
    public class EgzaminController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: Egzamin
        public ActionResult Index()
        {
            var egzaminy = db.Egzaminy.Include(e => e.Przedmiot);
            return View(egzaminy.ToList());
        }

        // GET: Egzamin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzaminy.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            return View(egzamin);
        }

        // GET: Egzamin/Create
        public ActionResult Create()
        {
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa");
            return View();
        }

        // POST: Egzamin/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EgzaminID,PrzedmiotID,Termin,Opis,Url")] Egzamin egzamin)
        {
            if (ModelState.IsValid)
            {
                db.Egzaminy.Add(egzamin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", egzamin.PrzedmiotID);
            return View(egzamin);
        }

        // GET: Egzamin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzaminy.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", egzamin.PrzedmiotID);
            return View(egzamin);
        }

        // POST: Egzamin/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EgzaminID,PrzedmiotID,Termin,Opis,Url")] Egzamin egzamin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(egzamin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "PrzedmiotID", "Nazwa", egzamin.PrzedmiotID);
            return View(egzamin);
        }

        // GET: Egzamin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Egzamin egzamin = db.Egzaminy.Find(id);
            if (egzamin == null)
            {
                return HttpNotFound();
            }
            return View(egzamin);
        }

        // POST: Egzamin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Egzamin egzamin = db.Egzaminy.Find(id);
            db.Egzaminy.Remove(egzamin);
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
