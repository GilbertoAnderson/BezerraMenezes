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
    public class LivrosController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: Livros
        public ActionResult Index()
        {
            var tblLivros = db.tblLivros.Include(t => t.tblCategorias);
            return View(tblLivros.ToList());
        }

        // GET: Livros/Details/5
        public ActionResult Details(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLivros tblLivros = db.tblLivros.Find(id);
            if (tblLivros == null)
            {
                return HttpNotFound();
            }
            return View(tblLivros);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.Categoria = new SelectList(db.tblCategorias, "idCategoria", "Descricao");
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN,Titulo,Autor,Editora,Edicao,Categoria,AnoPublicacao,Descricao,Capa,Status,AutorEspiritual")] tblLivros tblLivros)
        {
            if (ModelState.IsValid)
            {
                db.tblLivros.Add(tblLivros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categoria = new SelectList(db.tblCategorias, "idCategoria", "Descricao", tblLivros.Categoria);
            return View(tblLivros);
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLivros tblLivros = db.tblLivros.Find(id);
            if (tblLivros == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.tblCategorias, "idCategoria", "Descricao", tblLivros.Categoria);
            return View(tblLivros);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,Titulo,Autor,Editora,Edicao,Categoria,AnoPublicacao,Descricao,Capa,Status,AutorEspiritual")] tblLivros tblLivros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLivros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria = new SelectList(db.tblCategorias, "idCategoria", "Descricao", tblLivros.Categoria);
            return View(tblLivros);
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(Int64? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLivros tblLivros = db.tblLivros.Find(id);
            if (tblLivros == null)
            {
                return HttpNotFound();
            }
            return View(tblLivros);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLivros tblLivros = db.tblLivros.Find(id);
            db.tblLivros.Remove(tblLivros);
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
