using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BezerraMenezesExpress.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            //Session["Perfil"] = "";
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Controle Financeiro";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}