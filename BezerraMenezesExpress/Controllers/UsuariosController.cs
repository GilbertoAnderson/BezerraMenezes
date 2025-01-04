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
    public class UsuariosController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var tblUsuario = db.tblUsuario.Include(t => t.tblPerfil);
            return View(tblUsuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.idPerfil = new SelectList(db.tblPerfil, "idPerfil", "Descricao");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,idPerfil,CodUsuario,Nome,Senha,Telefone,Email,Celular,Nascimento,Ativo")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tblUsuario.Add(tblUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPerfil = new SelectList(db.tblPerfil, "idPerfil", "Descricao", tblUsuario.idPerfil);
            return View(tblUsuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPerfil = new SelectList(db.tblPerfil, "idPerfil", "Descricao", tblUsuario.idPerfil);
            return View(tblUsuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,idPerfil,CodUsuario,Nome,Senha,Telefone,Email,Celular,Nascimento,Ativo")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPerfil = new SelectList(db.tblPerfil, "idPerfil", "Descricao", tblUsuario.idPerfil);
            return View(tblUsuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            db.tblUsuario.Remove(tblUsuario);
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

        public ActionResult Login()
        {
            ViewBag.idPerfil = new SelectList(db.tblPerfil, "idPerfil", "Descricao");
            return View();
        }


    }
}
