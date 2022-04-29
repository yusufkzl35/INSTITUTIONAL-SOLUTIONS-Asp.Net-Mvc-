using ClosedXML.Excel;
using Erenbook.Login;
using Erenbook.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Erenbook.Controllers
{
    [LoginFilter]

    public class KestirimciController : Controller
    {
        // GET: Kestirimci
        EREN db = new EREN();

        

        public ActionResult Index(string aranan = null, int sayfa = 1)
        {
            var t = from x in db.KestirimciBakims select x;

            if (!String.IsNullOrEmpty(aranan))
            {
                t = db.KestirimciBakims.Where(kayit => kayit.TezgahKodu.Contains(aranan) || kayit.TezgahAdi.Contains(aranan));

            }
            return View(t.OrderByDescending(kayit => kayit.id).ThenBy(x => x.id).ToPagedList(sayfa, 25));
        }

        public ActionResult ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ekle(KestirimciBakim kayit)
        {


            db.KestirimciBakims.Add(kayit);
            db.SaveChanges();

            return RedirectToActionPermanent("Index");
        }



        public ActionResult Duzelt(int id = 0)
        {
            KestirimciBakim kayit = db.KestirimciBakims.Find(id);
            return View(kayit);

        }

        [HttpPost]
        public ActionResult Duzelt(KestirimciBakim kayit)
        {
            db.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult sil(int id=0)
        {
            KestirimciBakim kayit = db.KestirimciBakims.Find(id);
            db.KestirimciBakims.Remove(kayit);

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]

        public FileResult Excell1()
        {
            DataTable dt = new DataTable("kestirimci");


            dt.Columns.AddRange(new DataColumn[10]
                {

                     new DataColumn("Tezgah Kodu"),
                     new DataColumn("Tezgah Adı"),
                      new DataColumn("Bakım Türü"),
                     new DataColumn("Ölçüm Noktası"),
                    
                     new DataColumn("1.Periyot Bakım"),
                     new DataColumn("2.Periyot Bakım"),
                     new DataColumn("Kodu"),
                      new DataColumn("Kontrol Eden"),
                     new DataColumn("Yapılan Müdahale"),
                     new DataColumn("Açıklama")

                });

            var emps = from KestirimciBakim in db.KestirimciBakims.ToList() select KestirimciBakim;

            foreach (var kestirimciBakim in emps)
            {
                dt.Rows.Add(kestirimciBakim.TezgahKodu,
               kestirimciBakim.TezgahAdi,
               kestirimciBakim.BakimTuru,
             kestirimciBakim.OlcumNoktasi,
             kestirimciBakim.Periyot1,
             kestirimciBakim.Periyot2,
             kestirimciBakim.Kodu,
             kestirimciBakim.KontrolEden,
             kestirimciBakim.Mudahale,

               kestirimciBakim.Aciklama);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KestirimciBakim.xlsx");
                }
            }



        }

    }
}