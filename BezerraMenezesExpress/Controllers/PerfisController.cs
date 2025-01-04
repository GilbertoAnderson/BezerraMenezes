using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BezerraMenezesExpress.Models;

namespace BezerraMenezesExpress.Controllers
{
    public class PerfisController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: Perfis
        public ActionResult Index()
        {
            return View(db.tblPerfil.ToList());
        }

        // GET: Perfis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerfil tblPerfil = db.tblPerfil.Find(id);
            if (tblPerfil == null)
            {
                return HttpNotFound();
            }
            return View(tblPerfil);
        }

        // GET: Perfis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPerfil,Descricao,Ativo")] tblPerfil tblPerfil)
        {
            if (ModelState.IsValid)
            {
                db.tblPerfil.Add(tblPerfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblPerfil);
        }

        // GET: Perfis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerfil tblPerfil = db.tblPerfil.Find(id);
            if (tblPerfil == null)
            {
                return HttpNotFound();
            }
            return View(tblPerfil);
        }

        // POST: Perfis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPerfil,Descricao,Ativo")] tblPerfil tblPerfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPerfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblPerfil);
        }

        // GET: Perfis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPerfil tblPerfil = db.tblPerfil.Find(id);
            if (tblPerfil == null)
            {
                return HttpNotFound();
            }
            return View(tblPerfil);
        }

        // POST: Perfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPerfil tblPerfil = db.tblPerfil.Find(id);
            db.tblPerfil.Remove(tblPerfil);
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
