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
    public class ContasController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: Contas
        public ActionResult Index()
        {
            return View(db.tblConta.OrderBy(x => x.Descricao).ToList());
        }

        // GET: ListaContas
        public ActionResult ListaContas()
        {
            return View(db.tblConta.OrderBy(x => x.Descricao).ToList());
        }

        // GET: Contas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConta tblConta = db.tblConta.Find(id);
            if (tblConta == null)
            {
                return HttpNotFound();
            }
            return View(tblConta);
        }

        // GET: Contas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConta,Descricao,Ativo")] tblConta tblConta)
        {
            if (ModelState.IsValid)
            {
                db.tblConta.Add(tblConta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblConta);
        }

        // GET: Contas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConta tblConta = db.tblConta.Find(id);
            if (tblConta == null)
            {
                return HttpNotFound();
            }
            return View(tblConta);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConta,Descricao,Ativo")] tblConta tblConta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblConta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblConta);
        }

        // GET: Contas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblConta tblConta = db.tblConta.Find(id);
            if (tblConta == null)
            {
                return HttpNotFound();
            }
            return View(tblConta);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblConta tblConta = db.tblConta.Find(id);
            db.tblConta.Remove(tblConta);
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
