using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BezerraMenezesExpress.Models;
using BezerraMenezesExpress.HtmlHelpers;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using Rotativa;
using Rotativa.Options;

namespace BezerraMenezesExpress.Controllers
{
    public class ParcelasController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();


        public ParcelasController()
        {

            ViewBag.ListaNrParcela = Controllers.Shared.Helper.ListaNrParcela();
            ViewBag.ListaAnos = Controllers.Shared.Helper.ListaAnos();

        }


        // GET: Parcelas
        public ActionResult Index()
        {
            var tblParcela = db.tblParcela.Include(t => t.tblAssociado).Include(t => t.tblFormaPagamento).Include(t => t.tblSubconta);
            return View(tblParcela.ToList());
        }


        // GET: Parcelas
        public ActionResult AssociadoParcelas(int? id)
        {
            var tblParcela = db.tblParcela.Include(t => t.tblAssociado).OrderBy(t =>t.tblAssociado.Nome).Include(t => t.tblFormaPagamento).Include(t => t.tblSubconta);
            tblParcela = tblParcela.Where(t => t.idAssociado == id).OrderByDescending(t => new { t.Ano, t.Mes });

            tblAssociado _Associado = new tblAssociado();
           _Associado = db.tblAssociado.Where(x => x.idAssociado == id).FirstOrDefault();

           ViewBag.NomeAssociado = _Associado.Nome;

            return View(tblParcela.ToList());
        }

        // GET: Parcelas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParcela tblParcela = db.tblParcela.Find(id);
            if (tblParcela == null)
            {
                return HttpNotFound();
            }
            return View(tblParcela);
        }

        // GET: Parcelas/Create
        public ActionResult Create()
        {           
            //var _SubContas = db.tblSubconta.Where(x => x.Codigo.Substring(0,1) == "1");

            tblConta _Conta = new tblConta();            
            _Conta = db.tblConta.Where(x => x.Descricao == "RECEBER").FirstOrDefault();
            var _SubContas = db.tblSubconta.Where(x => x.Codigo.Substring(0, 1) == _Conta.idConta.ToString());
            var _Associados = db.tblAssociado.OrderBy( h => h.Nome);
            var _Forma = db.tblFormaPagamento.OrderBy(f => f.idFormaPagamento);


            ViewBag.idAssociado = new SelectList(_Associados, "idAssociado", "Nome"); 
            ViewBag.idformaPagamento = new SelectList(_Forma, "idFormaPagamento", "Descricao");
            ViewBag.idSubconta = new SelectList(_SubContas, "idSubconta", "Descricao");

            ViewBag.AnoDefault = DateTime.Now.Year;
            ViewBag.MesDefault = DateTime.Now.Month;
            ViewBag.Pagamento = DateTime.Now.ToShortDateString();

            return View();
        }

        // POST: Parcelas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idParcela,idAssociado,idSubconta,idformaPagamento,Ano,Mes,Pagamento,Valor")] tblParcela tblParcela)
        {
            if (ModelState.IsValid)
            {
                db.tblParcela.Add(tblParcela);
                db.SaveChanges();

                //.......................... grava um lançamento financeiro
                tblSubconta _SubConta = new tblSubconta();
                tblAssociado _Associado = new tblAssociado();
                tblConta _Conta = new tblConta();
                tblFormaPagamento _FormaPagto = new tblFormaPagamento();


                _Associado = db.tblAssociado.Where(x => x.idAssociado == tblParcela.idAssociado).FirstOrDefault();
                _Conta = db.tblConta.Where(x => x.Descricao == "RECEBER").FirstOrDefault();
                //_SubConta = db.tblSubconta.Where(x => x.Descricao == "MENSALIDADES").FirstOrDefault();
                _FormaPagto = db.tblFormaPagamento.Where(x => x.idFormaPagamento == tblParcela.idformaPagamento).FirstOrDefault();

                tblLancamento lancamento = new tblLancamento();
                lancamento.Descricao = _Associado.Nome + " [" + tblParcela.Mes + "-" +tblParcela.Ano + " (" + tblParcela.idParcela + ")] " + _FormaPagto.Descricao;
                lancamento.idConta = _Conta.idConta;
                lancamento.idSubconta = tblParcela.idSubconta;
                lancamento.Pagamento = tblParcela.Pagamento;
                lancamento.Valor = tblParcela.Valor;
                db.tblLancamento.Add(lancamento);
                db.SaveChanges();

                
                return RedirectToAction("Details", new { id = tblParcela.idParcela });
            }

            ViewBag.idAssociado = new SelectList(db.tblAssociado, "idAssociado", "Nome", tblParcela.idAssociado);
            ViewBag.idformaPagamento = new SelectList(db.tblFormaPagamento, "idFormaPagamento", "Descricao", tblParcela.idformaPagamento);
            ViewBag.idSubconta = new SelectList(db.tblSubconta, "idSubconta", "Descricao", tblParcela.idSubconta);

            return View(tblParcela);
        }

        // GET: Parcelas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParcela tblParcela = db.tblParcela.Find(id);
            if (tblParcela == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAssociado = new SelectList(db.tblAssociado, "idAssociado", "Nome", tblParcela.idAssociado);
            ViewBag.idformaPagamento = new SelectList(db.tblFormaPagamento, "idFormaPagamento", "Descricao", tblParcela.idformaPagamento);
            ViewBag.idSubconta = new SelectList(db.tblSubconta, "idSubconta", "Descricao", tblParcela.idSubconta);
            return View(tblParcela);
        }

        // POST: Parcelas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idParcela,idAssociado,idformaPagamento,Ano,Mes,Pagamento,Valor")] tblParcela tblParcela)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblParcela).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAssociado = new SelectList(db.tblAssociado, "idAssociado", "Nome", tblParcela.idAssociado);
            ViewBag.idformaPagamento = new SelectList(db.tblFormaPagamento, "idFormaPagamento", "Descricao", tblParcela.idformaPagamento);
            ViewBag.idSubconta = new SelectList(db.tblSubconta, "idSubconta", "Descricao", tblParcela.idSubconta);
            return View(tblParcela);
        }

        // GET: Parcelas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParcela tblParcela = db.tblParcela.Find(id);
            if (tblParcela == null)
            {
                return HttpNotFound();
            }
            return View(tblParcela);
        }

        // POST: Parcelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblParcela tblParcela = db.tblParcela.Find(id);
            tblAssociado _Associado = new tblAssociado();
            tblConta _Conta = new tblConta();
            tblLancamento _Lancamento = new tblLancamento();

            _Associado = db.tblAssociado.Where(x => x.idAssociado == tblParcela.idAssociado).FirstOrDefault();
            _Conta = db.tblConta.Where(x => x.Descricao == "RECEBER").FirstOrDefault();
                        
            _Lancamento = db.tblLancamento.Where(l => l.Descricao == _Associado.Nome 
                                                   && l.Pagamento == tblParcela.Pagamento
                                                   && l.idConta == _Conta.idConta
                                                   && l.idSubconta == tblParcela.idSubconta
                                                   && l.Valor == tblParcela.Valor).FirstOrDefault();
            if (_Lancamento != null)
            {
                db.tblLancamento.Remove(_Lancamento);
                db.SaveChanges();
            }

            // .......................... remove a parcela
            db.tblParcela.Remove(tblParcela);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = tblParcela.idAssociado });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

            


        //// GET: Parcelas/Details/5
        //public ActionResult ParcelaPdf(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblParcela tblParcela = db.tblParcela.Find(id);
        //    if (tblParcela == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var pdf = new ViewAsPdf
        //    {
        //        ViewName = "ParcelaImpressao",
        //        Model = tblParcela,
        //        IsGrayScale = true,
        //        PageWidth = 58,
        //        PageHeight = 105,
        //        PageMargins = { Bottom = 2, Left = 2, Right = 2, Top = 2 },                
        //    };
        //    return pdf;


        //}




        // GET: Parcelas/Details/5
        public ActionResult ParcelaPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParcela tblParcela = db.tblParcela.Find(id);
            if (tblParcela == null)
            {
                return HttpNotFound();
            }

            return View(tblParcela);


        }
        public FileResult ExportTo(string ReportType)
        {
            return File("xyz","PDF");
        }



        [HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblParcela tblParcela = db.tblParcela.Find(id);
            if (tblParcela == null)
            {
                return HttpNotFound();
            }
            //111111
            //return View(tblParcela);
            ViewBag.idParcela = tblParcela.idParcela;
            return RedirectToAction("Details", new { id = tblParcela.idParcela });

        }


        // ........................................ comando antigos


        //[HttpPost, ActionName("Details")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Details(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblParcela tblParcela = db.tblParcela.Find(id);
        //    if (tblParcela == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var pdf = new ViewAsPdf
        //    {
        //        ViewName = "ParcelaImpressao",
        //        Model = tblParcela,
        //        IsGrayScale = true,
        //        PageWidth = 58,
        //        PageHeight = 105,
        //        PageMargins = { Bottom = 2, Left = 2, Right = 2, Top = 2 },
        //    };
        //    return pdf;

        //}
    

        //[HttpPost, ActionName("Details")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Details(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblParcela tblParcela = db.tblParcela.Find(id);
        //    if (tblParcela == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    tblAssociado _Associado = new tblAssociado();
        //    tblSubconta _SubConta = new tblSubconta();
        //    tblFormaPagamento _FormaPagamento = new tblFormaPagamento();


        //    _Associado = db.tblAssociado.Where(x => x.idAssociado == tblParcela.idAssociado).FirstOrDefault();
        //    _SubConta = db.tblSubconta.Where(x => x.idSubconta == tblParcela.idSubconta).FirstOrDefault();
        //    _FormaPagamento = db.tblFormaPagamento.Where(x => x.idFormaPagamento == tblParcela.idformaPagamento).FirstOrDefault();
            


        //    var pdf = new ViewAsPdf
        //    {
        //        ViewName = "ParcelaPdf"
        //    };
        //    return pdf;

        //}



        //[HttpPost, ActionName("Detailsold")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Detailsold(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblParcela tblParcela = db.tblParcela.Find(id);
        //    if (tblParcela == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    tblAssociado _Associado = new tblAssociado();
        //    tblSubconta _SubConta = new tblSubconta();
        //    tblFormaPagamento _FormaPagamento = new tblFormaPagamento();


        //    _Associado = db.tblAssociado.Where(x => x.idAssociado == tblParcela.idAssociado).FirstOrDefault();
        //    _SubConta = db.tblSubconta.Where(x => x.idSubconta == tblParcela.idSubconta).FirstOrDefault();
        //    _FormaPagamento = db.tblFormaPagamento.Where(x => x.idFormaPagamento == tblParcela.idformaPagamento).FirstOrDefault();

        //    string _contribuicao = "";


        //    _contribuicao = _contribuicao + "  COMPROVANTE  DE  CONTRIBUICAO\n\n";
        //    _contribuicao = _contribuicao + "Parcela.. :" + tblParcela.Mes + "/" + tblParcela.Ano + "\n\n";
        //    _contribuicao = _contribuicao + "Matricula : " + _Associado.Matricula + "\n";
        //    _contribuicao = _contribuicao + "Nome..... : " + _Associado.Nome + "\n";
        //    _contribuicao = _contribuicao + "Data .... : " + tblParcela.Pagamento.Day + "/" + tblParcela.Pagamento.Month + "/" + tblParcela.Pagamento.Year + "\n";
        //    _contribuicao = _contribuicao + "Descritivo: " + _SubConta.Descricao + "\n";
        //    _contribuicao = _contribuicao + "Valor ... : " + tblParcela.Valor + "\n";
        //    _contribuicao = _contribuicao + "Forma ... : " + _FormaPagamento.Descricao + "\n\n\n";
        //    _contribuicao = _contribuicao + "\n ";
        //    _contribuicao = _contribuicao + "-------------------------\n";
        //    _contribuicao = _contribuicao + "assinatura do responsavel\n";
        //    _contribuicao = _contribuicao + "\n ";
        //    _contribuicao = _contribuicao + "\n ";
        //    _contribuicao = _contribuicao + "\n ";

        //    //var impressora = new System.IO.StreamWriter(@"\\.\PRN");
        //    //var impressora = new System.IO.StreamWriter("c:\teste.txt",true);
        //    //impressora.Write((char)15);
        //    //impressora.Write("teste");
        //    //impressora.Write("Matricula");
        //    //impressora.Write(_Associado.Matricula);
        //    //impressora.Write("Nome Contribuinte");
        //    //impressora.Write(_Associado.Nome);
        //    //impressora.Write("Data de Pagamento");
        //    //impressora.Write(tblParcela.Pagamento);
        //    //impressora.Write(_SubConta.Descricao);
        //    //impressora.Write("Valor");
        //    //impressora.Write(tblParcela.Valor);
        //    //impressora.Write("Forma de Pagamento");
        //    //impressora.Write(_FormaPagamento.Descricao);
        //    //impressora.Write(" ");
        //    //impressora.Write(" ");
        //    //impressora.Write("----------------------------------");
        //    //impressora.Write("assinatura do responsável");
        //    //impressora.Flush();
        //    //impressora.Close();
        //    //ImprimirDiretamentenaImpressoraPadrao(_contribuicao);

        //    return View(tblParcela);

        //}


        public void ImprimirDiretamentenaImpressoraPadrao( string conteudo)
        {
            string nomeImpressoraPadrao = (new PrinterSettings()).PrinterName;
            RawPrinterHelper.SendStringToPrinter(nomeImpressoraPadrao, conteudo);
        
        }

        public ActionResult PDFPadrao()
        {
            var pdf = new ViewAsPdf
            {
                ViewName = "Modelo"
            };
            return pdf;
        }

        /*
         * Configura algumas propriedades do PDF, inclusive o nome do arquivo gerado,
         * Porem agora ele baixa o pdf ao invés de mostrar no browser
         */
        public ActionResult PDFConfigurado()
        {
            var pdf = new ViewAsPdf
            {
                ViewName = "Modelo",
                FileName = "NomeDoArquivoPDF.pdf",
                //PageSize = Size.A4,
                IsGrayScale = true,
                //PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 5 },
            };
            return pdf;
        }


        // .........................................................



    }
}
