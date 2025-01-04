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
    public class AssociadosController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();


        public AssociadosController()
        {
            
            ViewBag.ListaCategorias = Controllers.Shared.Helper.ListaCategoria();

        }


        // GET: Associados
        public ActionResult Index(string strCriterio, bool? isAtivos)
        {
           // return View(db.tblAssociado.OrderBy(t => t.Nome).ToList());

            var Associado = from h in db.tblAssociado
                            select h;

            if (!String.IsNullOrEmpty(strCriterio))
            {
                Associado = Associado.Where(h => h.Nome.Contains(strCriterio.ToString())
                                            || h.categoria.Contains(strCriterio.ToString())
                                            || h.Matricula.Contains(strCriterio.ToString())
                                            || h.Celular.Contains(strCriterio.ToString())
                                            || h.Ativo == true);
            }


            if (isAtivos != false)
            {
                Associado = Associado.Where(h => h.Ativo == true);
            }


            Associado = Associado.OrderBy(h => h.Nome);
            return View(Associado);
        }


        // GET: Historico
        public ActionResult Historico(string strCriterio)
        {
            var historico = from h in db.tblAssociado
                            select h;

            if (!String.IsNullOrEmpty(strCriterio))
            {
                historico = historico.Where(h => h.Nome.Contains(strCriterio.ToString()));
            }
            historico = historico.OrderBy(h=>h.Nome);
            return View(historico);
        }

        // GET: Associados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssociado tblAssociado = db.tblAssociado.Find(id);
            if (tblAssociado == null)
            {
                return HttpNotFound();
            }
            return View(tblAssociado);
        }

        // GET: Associados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Associados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAssociado,Nome,Email,Celular,Nascimento,Ativo,Endereco,Complemento,Bairro,Cidade,UF,CEP,Matricula, dtCadastro, dtMigracao, Categoria")] tblAssociado tblAssociado)
        {
            if (ModelState.IsValid)
            {
                db.tblAssociado.Add(tblAssociado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAssociado);
        }

        // GET: Associados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssociado tblAssociado = db.tblAssociado.Find(id);
            if (tblAssociado == null)
            {
                return HttpNotFound();
            }
            return View(tblAssociado);
        }

        // POST: Associados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAssociado,Nome,Email,Celular,Nascimento,Ativo,Endereco,Complemento,Bairro,Cidade,UF,CEP,Matricula, dtCadastro, dtMigracao, Categoria")] tblAssociado tblAssociado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAssociado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAssociado);
        }

        // GET: Associados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssociado tblAssociado = db.tblAssociado.Find(id);
            if (tblAssociado == null)
            {
                return HttpNotFound();
            }
            return View(tblAssociado);
        }

        // POST: Associados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAssociado tblAssociado = db.tblAssociado.Find(id);
            db.tblAssociado.Remove(tblAssociado);
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
