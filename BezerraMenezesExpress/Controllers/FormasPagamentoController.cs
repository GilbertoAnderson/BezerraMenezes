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
    public class FormasPagamentoController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: FormasPagamento
        public ActionResult Index()
        {
            return View(db.tblFormaPagamento.ToList());
        }

        // GET: FormasPagamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFormaPagamento tblFormaPagamento = db.tblFormaPagamento.Find(id);
            if (tblFormaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(tblFormaPagamento);
        }

        // GET: FormasPagamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormasPagamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFormaPagamento,Descricao,Ativo")] tblFormaPagamento tblFormaPagamento)
        {
            if (ModelState.IsValid)
            {
                db.tblFormaPagamento.Add(tblFormaPagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblFormaPagamento);
        }

        // GET: FormasPagamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFormaPagamento tblFormaPagamento = db.tblFormaPagamento.Find(id);
            if (tblFormaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(tblFormaPagamento);
        }

        // POST: FormasPagamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFormaPagamento,Descricao,Ativo")] tblFormaPagamento tblFormaPagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFormaPagamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFormaPagamento);
        }

        // GET: FormasPagamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFormaPagamento tblFormaPagamento = db.tblFormaPagamento.Find(id);
            if (tblFormaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(tblFormaPagamento);
        }

        // POST: FormasPagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFormaPagamento tblFormaPagamento = db.tblFormaPagamento.Find(id);
            db.tblFormaPagamento.Remove(tblFormaPagamento);
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
