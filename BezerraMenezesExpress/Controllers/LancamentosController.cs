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
    public class LancamentosController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();
        public LancamentosController()
        {
            //ViewBag.strCriterio = "";
            //ViewBag.strData = "";
            //ViewBag.strDataFim = "";
            //ViewBag.strSubConta = "";


        }

        // GET: Lancamentos
        public ActionResult Index(string strCriterio,string strData, string strDataFim, string strSubConta)
        {
            ViewBag.strCriterio = strCriterio;
            ViewBag.strData = strData;
            ViewBag.strDataFim = strDataFim;
            ViewBag.strSubConta = strSubConta;

            var tblLancamento = db.tblLancamento.Include(t => t.tblConta).Include(t => t.tblSubconta);           

            //return View(tblLancamento.ToList());

            //var Lancamento = from h in db.tblAssociado
            //                select h;

            if (!String.IsNullOrEmpty(strCriterio))
            {
                tblLancamento = tblLancamento.Where(h => h.Descricao.Contains(strCriterio.ToString()));
            }

            if (!String.IsNullOrEmpty(strData))
            {
                tblLancamento = tblLancamento.Where(h => h.Pagamento.Equals(strData.ToString()));
            }
            tblLancamento = tblLancamento.OrderByDescending(h => h.Pagamento);
            return View(tblLancamento);

        }
        

        // GET: LancamentosConta
        public ActionResult LancamentosConta(int? id, string strSubConta, string strCriterio, string strData, string strDataFim)
        {
            ViewBag.strCriterio = strCriterio;
            ViewBag.strData = strData;
            ViewBag.strDataFim = strDataFim;
            ViewBag.strSubConta = strSubConta;

            tblConta _Conta = new tblConta();
            _Conta = db.tblConta.Where(x => x.idConta == id).FirstOrDefault();
            ViewBag.NomeConta = _Conta.Descricao;
            ViewBag.idConta = id;

           
            var tblLancamento = db.tblLancamento.Include(t => t.tblConta).Include(t => t.tblSubconta);
            tblLancamento = tblLancamento.Where(l => l.idConta == id).OrderByDescending(l => new { l.Pagamento });

            if (!String.IsNullOrEmpty(strSubConta))
            {
                tblLancamento = tblLancamento.Where(h => h.tblSubconta.Descricao.Contains(strSubConta.ToString()));
            }

            if (!String.IsNullOrEmpty(strCriterio))
            {
                tblLancamento = tblLancamento.Where(h => h.Descricao.Contains(strCriterio.ToString()));
            }

            if (String.IsNullOrEmpty(strData) && String.IsNullOrEmpty(strDataFim))
            {
                DateTime d = DateTime.Now;
                d = d.AddDays(-240); //Isto é oque vc está procurando.
                tblLancamento = tblLancamento.Where(h => h.Pagamento >= d);
            }            
            else
            {
                if (!String.IsNullOrEmpty(strData))
                {
                    var dtcriterio = Convert.ToDateTime(strData);
                    tblLancamento = tblLancamento.Where(h => h.Pagamento >= dtcriterio);
                }
                if (!String.IsNullOrEmpty(strDataFim))
                {
                    var dtcriterio = Convert.ToDateTime(strDataFim);
                    tblLancamento = tblLancamento.Where(h => h.Pagamento <= dtcriterio);
                }

            }
            //tblLancamento = tblLancamento.OrderByDescending(h => h.Pagamento);

            return View(tblLancamento.ToList().OrderByDescending(l => l.Previsto));
        }
            
        


        // GET: Lancamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLancamento tblLancamento = db.tblLancamento.Find(id);
            if (tblLancamento == null)
            {
                return HttpNotFound();
            }
            return View(tblLancamento);
        }

        // GET: Lancamentos/Create
        public ActionResult Create(int ?id)
        {
            ViewBag.idConta = new SelectList(db.tblConta, "idConta", "Descricao");

            string strIdConta = Convert.ToString(id);

            var _subConta = db.tblSubconta.Where(x => x.Codigo.Substring(0, 1) == strIdConta);

            var _newsubConta =(from s in _subConta.ToList()
                    select new
                    {
                        idSubconta = s.idSubconta,
                        Descricao = s.Codigo + " " + s.Descricao
                    });

            ViewBag.idSubconta = new SelectList(_newsubConta, "idSubconta", "Descricao");
            return View();
        }

        // POST: Lancamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLancamento,idConta,idSubconta,Descricao,Pagamento,Valor,Previsto")] tblLancamento tblLancamento)
        {
            if (ModelState.IsValid)
            {
                db.tblLancamento.Add(tblLancamento);
                db.SaveChanges();
                return RedirectToAction("LancamentosConta", new { id = tblLancamento.idConta });
            }

            ViewBag.idConta = new SelectList(db.tblConta, "idConta", "Descricao", tblLancamento.idConta);
            ViewBag.idSubconta = new SelectList(db.tblSubconta, "idSubconta", "Descricao", tblLancamento.idSubconta);
            return View(tblLancamento);
        }

        // GET: Lancamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLancamento tblLancamento = db.tblLancamento.Find(id);
            if (tblLancamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idConta = new SelectList(db.tblConta, "idConta", "Descricao", tblLancamento.idConta);

            string strIdConta = Convert.ToString(tblLancamento.idConta);

            var _subConta = db.tblSubconta.Where(x => x.Codigo.Substring(0, 1) == strIdConta);

            var _newsubConta = (from s in _subConta.ToList()
                                select new
                                {
                                    idSubconta = s.idSubconta,
                                    Descricao = s.Codigo + " " + s.Descricao
                                });

            ViewBag.idSubconta = new SelectList(_newsubConta, "idSubconta", "Descricao", tblLancamento.idSubconta);
            return View(tblLancamento);
        }

        // POST: Lancamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLancamento,idConta,idSubconta,Descricao,Pagamento,Valor,Previsto")] tblLancamento tblLancamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLancamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LancamentosConta", new { id = tblLancamento.idConta });
            }
            ViewBag.idConta = new SelectList(db.tblConta, "idConta", "Descricao", tblLancamento.idConta);
            ViewBag.idSubconta = new SelectList(db.tblSubconta, "idSubconta", "Descricao", tblLancamento.idSubconta);
            return View(tblLancamento);
        }

        // GET: Lancamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLancamento tblLancamento = db.tblLancamento.Find(id);
            if (tblLancamento == null)
            {
                return HttpNotFound();
            }
            return View(tblLancamento);
        }

        // POST: Lancamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLancamento tblLancamento = db.tblLancamento.Find(id);

            var idConta = tblLancamento.idConta;
            db.tblLancamento.Remove(tblLancamento);
            db.SaveChanges();
            return RedirectToAction("LancamentosConta", new { id = idConta });
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
