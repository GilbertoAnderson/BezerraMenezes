using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using BezerraMenezesExpress.Models;


namespace BezerraMenezesExpress.Controllers
{

    public class RelatoriosController : Controller
    {
        private db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();



        // GET: Relatorios
        public ActionResult Index()
        {
            var _Contas = db.tblConta.OrderByDescending( h => h.Descricao);
            ViewBag.Contas = new SelectList(_Contas, "idConta", "Descricao");
            IEnumerable<SelectListItem> items = db.tblConta.Select(c => new SelectListItem
            {
                Value = c.idConta.ToString(),
                Text = c.Descricao
                }
            );
            return View();
        }

        

        [HttpGet]
        public ActionResult Extrato()
        {
            var _Contas = db.tblConta.OrderByDescending(h => h.Descricao);
            ViewBag.Contas = new SelectList(_Contas, "idConta", "Descricao");
            IEnumerable<SelectListItem> items = db.tblConta.Select(c => new SelectListItem
                {
                    Value = c.idConta.ToString(),
                    Text = c.Descricao
                }
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Extrato(Relatorios extrato )
        {
            return RedirectToAction("ExtratoReport","Relatorios", new { Conta = extrato.Conta, DtInicio = extrato.DtInicio, DtFim = extrato.DtFim,Filtro = extrato.Filtro });
        }



        public ActionResult ExtratoReport(Int32 Conta, DateTime DtInicio, DateTime DtFim, string Filtro)
        {
            var extratoReport =  db.sp_fin_rep_extrato(Conta, DtInicio, DtFim, Filtro);
            
            tblConta _Conta = new tblConta();
            _Conta = db.tblConta.Where(x => x.idConta == Conta).FirstOrDefault();

            ViewBag.Conta = _Conta.Descricao;
            ViewBag.DtInicio = DtInicio;
            ViewBag.DtFim = DtFim;
            ViewBag.Filtro = Filtro;

            //var tblExtrato = db.tblExtrato.OrderBy(d => d.Data).OrderBy(d => d.Subconta).OrderBy(d => d.Descricao);
            var tblExtrato = db.tblExtrato;
            return View(tblExtrato.ToList());
            
        }



        public ActionResult AssociadoCategoria()
        {
            var _associadocategoria = from p in db.tblAssociado
                            orderby p.categoria, p.dtCadastro, p.dtMigracao
                            select p;           
            return View(_associadocategoria.ToList());

        }



        [HttpGet]
        public ActionResult UltimaParcela()
        {
            ViewBag.DtAtual = DateTime.Today;
            List<UltimaParcela> listaUltimaParcela = new List<UltimaParcela>();

            var _subconta = from s in db.tblSubconta
                            where s.Descricao.ToString() == "MENSALIDADES"
                            select s;
            var _subcontaFirst = _subconta.FirstOrDefault();
            int _idSubConta = _subcontaFirst.idSubconta;


            var _parcelas = from p in db.tblParcela
                            where p.idSubconta == _idSubConta
                            orderby p.idAssociado, p.Ano, p.Mes
                            select p;
            //_parcelas = _parcelas.OrderBy(a => a.idAssociado);
            //_parcelas = _parcelas.OrderBy(m => m.Ano);
            //_parcelas = _parcelas.OrderBy(x => x.Mes);

            var _parcelasFirst = _parcelas.FirstOrDefault();
             
            int _idAssociado = _parcelasFirst.idAssociado;
            int _Ano = _parcelasFirst.Ano;
            int _Mes = _parcelasFirst.Mes;
            string _Matricula = "";
            string _Nome = "";
            string _Categoria = "";
            foreach (var _parcela in _parcelas)
            {

                if (_parcela.idAssociado != _idAssociado)
                {
                    var _associados = from a in db.tblAssociado
                                      where a.idAssociado == _idAssociado
                                      select a;
                    var _associadosFirst = _associados.FirstOrDefault();
                    _Nome = _associadosFirst.Nome;
                    _Matricula = _associadosFirst.Matricula.ToString();

                    UltimaParcela ultimaParcela = new UltimaParcela();
                    ultimaParcela.Matricula = _Matricula;
                    ultimaParcela.Nome = _Nome;
                    ultimaParcela.Ano = _Ano;
                    ultimaParcela.Mes = _Mes;

                    listaUltimaParcela.Add(ultimaParcela);

                    _idAssociado = _parcela.idAssociado;
                    _Ano = _parcela.Ano;
                    _Mes = _parcela.Mes;
                }
                else
                {
                    if (_Ano < _parcela.Ano)
                    {
                        _Ano = _parcela.Ano;
                        _Mes = _parcela.Mes;
                    }
                    if (_Mes < _parcela.Mes && _Ano == _parcela.Ano)
                    {
                        _Mes = _parcela.Mes;
                    }
                }

            }
            // ................... INCLUI O ULTIMO
            var _associadoslast = from a in db.tblAssociado
                              where a.idAssociado == _idAssociado
                              select a;
             var _associadosFirstx = _associadoslast.FirstOrDefault();
            _Nome = _associadosFirstx.Nome;
            _Categoria = _associadosFirstx.categoria;

            UltimaParcela ultimaParcelax = new UltimaParcela();
            ultimaParcelax.Matricula = _Matricula;
            ultimaParcelax.Nome = _Nome;
            ultimaParcelax.Ano = _Ano;
            ultimaParcelax.Mes = _Mes;
            ultimaParcelax.Categoria = _Categoria;

            listaUltimaParcela.Add(ultimaParcelax);

            return View(listaUltimaParcela.OrderBy(x =>x.Nome));
        }



        [HttpGet]
        public ActionResult Fluxo()
        {
            ViewBag.Contas = new SelectList(db.tblConta, "idConta", "Descricao");
            IEnumerable<SelectListItem> items = db.tblConta.Select(c => new SelectListItem
            {
                Value = c.idConta.ToString(),
                Text = c.Descricao
            }
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fluxo(Relatorios fluxo)
        {
            return RedirectToAction("FluxoReport", "Relatorios", new { Conta = fluxo.Conta, DtInicio = fluxo.DtInicio });
        }




        public ActionResult FluxoReport(Int32 Conta, DateTime DtInicio)
        {
            // ------- executa o processamento do fluxo
            var fluxoReport = db.sp_fin_rep_fluxo(Conta, DtInicio);

            tblConta _Conta = new tblConta();
            _Conta = db.tblConta.Where(x => x.idConta == Conta).FirstOrDefault();

            ViewBag.Conta = _Conta.Descricao;
            ViewBag.DtInicio = DtInicio;
            // .............. nome das colunas

            tblFluxoColunas _Colunas = new tblFluxoColunas();
            _Colunas = db.tblFluxoColunas.FirstOrDefault();
            ViewBag.col01 = _Colunas.col01.ToString();
            ViewBag.col02 = _Colunas.col02.ToString();
            ViewBag.col03 = _Colunas.col03.ToString();
            ViewBag.col04 = _Colunas.col04.ToString();
            ViewBag.col05 = _Colunas.col05.ToString();
            ViewBag.col06 = _Colunas.col06.ToString();
            ViewBag.col07 = _Colunas.col07.ToString();
            ViewBag.col08 = _Colunas.col08.ToString();
            ViewBag.col09 = _Colunas.col09.ToString();
            ViewBag.col10 = _Colunas.col10.ToString();
            ViewBag.col11 = _Colunas.col11.ToString();
            ViewBag.col12 = _Colunas.col12.ToString();

            //var tblExtrato = db.tblExtrato.OrderBy(d => d.Data).OrderBy(d => d.Subconta).OrderBy(d => d.Descricao);
            var tblFluxo= db.tblFluxo;
            return View(tblFluxo.ToList());

        }




    }
}