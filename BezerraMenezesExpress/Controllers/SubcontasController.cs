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
    public class SubcontasController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: Subcontas
        public ActionResult Index()
        {
            return View(db.tblSubconta.OrderBy(x => x.Codigo).ToList());
        }

        // GET: Subcontas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubconta tblSubconta = db.tblSubconta.Find(id);
            if (tblSubconta == null)
            {
                return HttpNotFound();
            }
            return View(tblSubconta);
        }

        // GET: Subcontas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subcontas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSubconta,Descricao,Ativo,Codigo")] tblSubconta tblSubconta)
        {
            if (ModelState.IsValid)
            {
                db.tblSubconta.Add(tblSubconta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSubconta);
        }

        // GET: Subcontas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubconta tblSubconta = db.tblSubconta.Find(id);
            if (tblSubconta == null)
            {
                return HttpNotFound();
            }
            return View(tblSubconta);
        }

        // POST: Subcontas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSubconta,Descricao,Ativo,Codigo")] tblSubconta tblSubconta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSubconta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSubconta);
        }

        // GET: Subcontas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubconta tblSubconta = db.tblSubconta.Find(id);
            if (tblSubconta == null)
            {
                return HttpNotFound();
            }
            return View(tblSubconta);
        }

        // POST: Subcontas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSubconta tblSubconta = db.tblSubconta.Find(id);
            db.tblSubconta.Remove(tblSubconta);
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
