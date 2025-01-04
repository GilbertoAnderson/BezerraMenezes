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
    public class CarrinhosEntradaController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

        //public ActionResult Validacao(tblCarrinhoEntrada carrinho) {
        //    if (string.IsNullOrEmpty(carrinho.tblLivros.Titulo))
        //        ModelState.AddModelError("Titulo", "O Titulo é obrigatório");

        //    if (string.IsNullOrEmpty(carrinho.Fornecedor))
        //        ModelState.AddModelError("Fornecedor", "O Fornecedor é obrigatório");

        //    if(carrinho.Quantidade <= 0)
        //        ModelState.AddModelError("Quantidade", "A Quantidade deve ser maior que 0 (zero)");

        //    if (carrinho.Valor <= 0)
        //        ModelState.AddModelError("Valor", "O Valor deve ser maior que 0 (zero)");

        //    if (carrinho.ISBN.ToString() == null)
        //        ModelState.AddModelError("ISBN", "O ISBN é obrigatório");

        //    if (!ModelState.IsValid)
        //    {
        //        //sim falhou 
        //        return View();
        //    }
        //    else
        //    {
        //        //esta tudo ok
        //        return Redirect("/");
        //    }


        //}


        // GET: CarrinhosEntrada
        public ActionResult Index()
        {
            var tblCarrinhoEntrada = db.tblCarrinhoEntrada.Include(t => t.tblLivros);

            return View(tblCarrinhoEntrada.ToList());
        }

        // GET: CarrinhosEntrada/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            if (tblCarrinhoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tblCarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Create
        public ActionResult Create()
        {
            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo");
            return View();
        }

        // POST: CarrinhosEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCarrinhoEntrada,NotaFiscal,Fornecedor,dtMovimento,ISBN,Quantidade,Valor")] tblCarrinhoEntrada tblCarrinhoEntrada)
        {
            if (ModelState.IsValid)
            {
                db.tblCarrinhoEntrada.Add(tblCarrinhoEntrada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);
            return View(tblCarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            if (tblCarrinhoEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);
            return View(tblCarrinhoEntrada);
        }

        // POST: CarrinhosEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCarrinhoEntrada,NotaFiscal,Fornecedor,dtMovimento,ISBN,Quantidade,Valor")] tblCarrinhoEntrada tblCarrinhoEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCarrinhoEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);
            return View(tblCarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            if (tblCarrinhoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tblCarrinhoEntrada);
        }

        // POST: CarrinhosEntrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            db.tblCarrinhoEntrada.Remove(tblCarrinhoEntrada);
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

        #region Entrada ---------------------------------------------------------------------------------------------------------------
        // GET: CarrinhosEntrada
        public ActionResult EntradaIndex(string strCriterio)
        {
            var tblCarrinhoEntrada = db.tblCarrinhoEntrada.Include(t => t.tblLivros);
            tblCarrinhoEntrada = tblCarrinhoEntrada.Where(ce => ce.idTipoEstoque == 1);
            if (strCriterio != "" && strCriterio != null) {
                tblCarrinhoEntrada = tblCarrinhoEntrada.Where(ce => ce.NotaFiscal.Contains(strCriterio)
                                                           || ce.dtMovimento.ToString().Contains(strCriterio)
                                                           || ce.Fornecedor.Contains(strCriterio));
            }

            List<tblCarrinhoEntrada> list_CarrinhoEntrada = new List<tblCarrinhoEntrada>();
            string _notaAtual = "";
            foreach(var _nota in tblCarrinhoEntrada) {

                tblCarrinhoEntrada _notaNew = new tblCarrinhoEntrada();

                _notaNew.idCarrinhoEntrada = _nota.idCarrinhoEntrada;
                _notaNew.NotaFiscal = _nota.NotaFiscal;
                _notaNew.Fornecedor = _nota.Fornecedor;
                _notaNew.dtMovimento = _nota.dtMovimento.Date;
                if (_notaAtual != _nota.NotaFiscal) {
                    list_CarrinhoEntrada.Add(_notaNew);
                    _notaAtual = _nota.NotaFiscal;
                }
            }

            
            return View(list_CarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Create
        public ActionResult EntradaCreate(int? id)
        {
            //ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo");
            ViewBag.idTipoEstoque = new SelectList(db.tblTipoEstoque, "idTipoEstoque", "Descricao");
            ViewBag.ISBN = "";
            ViewBag.NomeLivro = "";
            ViewBag.ValorTotal = 0;
            if (id != null)
            {
                var _UltimoCarrinho = db.tblCarrinhoEntrada.Where(x => x.idCarrinhoEntrada == id).FirstOrDefault();
                List<tblCarrinhoEntrada> _tblCarrinhoEntrada = db.tblCarrinhoEntrada.Where(x => x.NotaFiscal == _UltimoCarrinho.NotaFiscal).OrderBy(x => x.ISBN).ToList();
                // ......... calucla o total da nota
                Decimal _vlrTotal = 0;
                foreach (var _livro in _tblCarrinhoEntrada)
                {
                    _vlrTotal = _vlrTotal + (Convert.ToDecimal(_livro.Quantidade) * Convert.ToDecimal(_livro.Valor));
                }

                ViewBag.CursorCampo = "ISBN";
                ViewBag.ValorTotal = _vlrTotal.ToString("#.00");
                ViewBag.LivrosMesmaNotaFiscal = _tblCarrinhoEntrada;
                ViewBag.NotaFiscal = _UltimoCarrinho.NotaFiscal;
                ViewBag.Fornecedor = _UltimoCarrinho.Fornecedor;
                ViewBag.DtMovimento = _UltimoCarrinho.dtMovimento;
                ViewBag.ReadOnly = true;
            }
            else
            {
                ViewBag.DtMovimento = DateTime.Now.ToString();
                ViewBag.ReadOnly =  false;
                ViewBag.NotaFiscal = "";
                ViewBag.Fornecedor = "";
                ViewBag.CursorCampo = "";
            }

            return View();
        }

        // POST: CarrinhosEntrada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]      
        public ActionResult EntradaCreate([Bind(Include = "idCarrinhoEntrada,NotaFiscal,Fornecedor,dtMovimento,ISBN,Quantidade,Valor,idTipoEstoque")] tblCarrinhoEntrada tblCarrinhoEntrada)
        {
            
            if (ModelState.IsValid)            {
                db.tblCarrinhoEntrada.Add(tblCarrinhoEntrada);
                db.SaveChanges();

                var _UltimoCarrinho = db.tblCarrinhoEntrada.OrderByDescending(x => x.idCarrinhoEntrada).FirstOrDefault();
                List<tblCarrinhoEntrada> _tblCarrinhoEntrada = db.tblCarrinhoEntrada.Where(x => x.NotaFiscal == _UltimoCarrinho.NotaFiscal).OrderBy(x => x.ISBN).ToList();
                // ......... calucla o total da nota
                Decimal _vlrTotal = 0;
                foreach (var _livro in _tblCarrinhoEntrada)
                {
                    _vlrTotal = _vlrTotal + (Convert.ToDecimal(_livro.Quantidade) * Convert.ToDecimal(_livro.Valor));
                }
                ViewBag.CursorCampo = "ISBN";
                ViewBag.ValorTotal = _vlrTotal.ToString("#.00");
                ViewBag.LivrosMesmaNotaFiscal = _tblCarrinhoEntrada;
                ViewBag.DtMovimento = DateTime.Now.ToString();
                ViewBag.NotaFiscal = _UltimoCarrinho.NotaFiscal.ToString();

                return RedirectToAction("EntradaCreate", new { id = tblCarrinhoEntrada.idCarrinhoEntrada});
            }

            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);
            return View(tblCarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Edit/5
        public ActionResult EntradaEdit(int? id)
        {

            //List<tblCarrinhoEntrada> _tblCarrinhoEntrada = new List<tblCarrinhoEntrada>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //else
            //{
            //    var _UltimoCarrinho = db.tblCarrinhoEntrada.Where(x => x.idCarrinhoEntrada == id).FirstOrDefault();
            //    _tblCarrinhoEntrada = db.tblCarrinhoEntrada.Where(x => x.NotaFiscal == _UltimoCarrinho.NotaFiscal).OrderBy(x => x.ISBN).ToList();
            //    ViewBag.LivrosMesmaNotaFiscal = _tblCarrinhoEntrada;
            //    ViewBag.NotaFiscal = _UltimoCarrinho.NotaFiscal;
            //    ViewBag.Fornecedor = _UltimoCarrinho.Fornecedor;
            //    ViewBag.DtMovimento = _UltimoCarrinho.dtMovimento;
            //    ViewBag.ReadOnly = true;


            //}
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            if (tblCarrinhoEntrada == null)
            {
                return HttpNotFound();
            }


            string _nomeLivro = "Livro não encontrado, favor cadastrar";
            tblLivros tblLivros = db.tblLivros.Find(tblCarrinhoEntrada.ISBN);
            if (tblLivros != null)
            {
                _nomeLivro = tblLivros.Titulo + " " + tblLivros.Autor;
            }

            List<tblCarrinhoEntrada> _tblCarrinhoEntrada = db.tblCarrinhoEntrada.Where(x => x.NotaFiscal == tblCarrinhoEntrada.NotaFiscal).OrderBy(x => x.ISBN).ToList();
            ViewBag.NomeLivro = _nomeLivro;
            ViewBag.LivrosMesmaNotaFiscal = _tblCarrinhoEntrada;
            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);

            return View(tblCarrinhoEntrada);
        }



        // POST: CarrinhosEntrada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EntradaEdit([Bind(Include = "idCarrinhoEntrada,NotaFiscal,Fornecedor,dtMovimento,ISBN,Quantidade,Valor,idTipoEstoque")] tblCarrinhoEntrada tblCarrinhoEntrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCarrinhoEntrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EntradaIndex");
            }
            ViewBag.ISBN = new SelectList(db.tblLivros, "ISBN", "Titulo", tblCarrinhoEntrada.ISBN);
            return View(tblCarrinhoEntrada);
        }

        // GET: CarrinhosEntrada/Delete/5
        public ActionResult EntradaDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            if (tblCarrinhoEntrada == null)
            {
                return HttpNotFound();
            }
            return View(tblCarrinhoEntrada);
        }

        // POST: CarrinhosEntrada/Delete/5
        [HttpPost, ActionName("EntradaDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult EntradaDeleteConfirmed(int id)
        {
            tblCarrinhoEntrada tblCarrinhoEntrada = db.tblCarrinhoEntrada.Find(id);
            string _notafiscal = tblCarrinhoEntrada.NotaFiscal;
            db.tblCarrinhoEntrada.Remove(tblCarrinhoEntrada);
            db.SaveChanges();
            // se houver mais algum livro nesta nota volta pro edit da nota, caso contrario volta pra lista de entradas
            tblCarrinhoEntrada tbCarrinhoExiste = db.tblCarrinhoEntrada.Where(x => x.NotaFiscal == _notafiscal).FirstOrDefault();
            if (tbCarrinhoExiste != null)
            {
                return RedirectToAction("EntradaEdit", new { id = tbCarrinhoExiste.idCarrinhoEntrada });
            }


            return RedirectToAction("EntradaIndex");
        }



        public string PesquisarNomeLivro(Int64? ISBN)
        {
            string _nomeLivro ="Livro não encontrado, favor cadastrar";
            tblLivros tblLivros = db.tblLivros.Find(ISBN);
            if (tblLivros != null)
            {
                _nomeLivro = tblLivros.Titulo + " " + tblLivros.Autor;
            }
            
            return _nomeLivro;
            
        }

        #endregion Entrada





    }
}
