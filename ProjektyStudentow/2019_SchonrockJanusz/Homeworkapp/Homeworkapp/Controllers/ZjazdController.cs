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
    public class ZjazdController : Controller
    {
        private HomeworkappDbContext db = new HomeworkappDbContext();

        // GET: Zjazd
        public ActionResult Index()
        {
            return View(db.Zjazdy.ToList());
        }

        // GET: Zjazd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zjazd zjazd = db.Zjazdy.Find(id);
            if (zjazd == null)
            {
                return HttpNotFound();
            }
            return View(zjazd);
        }

        // GET: Zjazd/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zjazd/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZjazdID,Numer,Dzien1,Dzien2")] Zjazd zjazd)
        {
            if (ModelState.IsValid)
            {
                db.Zjazdy.Add(zjazd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zjazd);
        }

        // GET: Zjazd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zjazd zjazd = db.Zjazdy.Find(id);
            if (zjazd == null)
            {
                return HttpNotFound();
            }
            return View(zjazd);
        }

        // POST: Zjazd/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZjazdID,Numer,Dzien1,Dzien2")] Zjazd zjazd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zjazd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zjazd);
        }

        // GET: Zjazd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zjazd zjazd = db.Zjazdy.Find(id);
            if (zjazd == null)
            {
                return HttpNotFound();
            }
            return View(zjazd);
        }

        // POST: Zjazd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zjazd zjazd = db.Zjazdy.Find(id);
            db.Zjazdy.Remove(zjazd);
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
