using System;
using Erenbook.Login;
using Erenbook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using CsQuery.Engine.PseudoClassSelectors;
using com.sun.org.apache.xpath.@internal.operations;
using NPOI.SS.Formula.Functions;

namespace Erenbook.Controllers
{
   
    public class HomeController : Controller
    {
        EREN db = new EREN();
        [LoginFilter]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult login() { return View(); }


        [HttpPost]

        public ActionResult login(Kullanici kul)
        {
            EREN db = new EREN();

            Login.Login.aktifKul = db.kullanicis.FirstOrDefault(x => x.kadi == kul.kadi && x.sifre == kul.sifre);
            if (Login.Login.aktifVarmi)
            {
                return RedirectToAction("Index", "UretimAraclari");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Bulunamadı."; 
                return View();
            }
        }

        public ActionResult logout()
        {
            Login.Login.cikisYap();
            return RedirectToAction("login", "Home");
        }


    }
}