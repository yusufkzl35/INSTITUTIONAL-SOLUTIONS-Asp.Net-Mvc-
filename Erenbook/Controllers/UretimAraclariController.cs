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
    public class UretimAraclariController : Controller
    {
        EREN db = new EREN();

        // GET: UretimAraclari

        //public ActionResult Index(string search, int? i)
        //{
        //    EREN db = new EREN();

        //    List<UretimAraclari> listemp = db.UretimAraclaris.ToList();
        //    return View(db.UretimAraclaris.Where(x => x.MakineKodu.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 35));
        //}

        public ActionResult Index(string aranan = null, int sayfa = 1)
        {
            var t = from x in db.UretimAraclaris select x;
            if (!String.IsNullOrEmpty(aranan))
            {
                t = db.UretimAraclaris.Where(kayit => kayit.MakineKodu.Contains(aranan)|| kayit.Ay.Contains(aranan)||kayit.Ay2.Contains(aranan));

            }
            return View(t.OrderByDescending(kayit => kayit.id).ThenBy(x => x.id).ToPagedList(sayfa, 35));
        }

        public ActionResult ekle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ekle(UretimAraclari kayit)
        {


            db.UretimAraclaris.Add(kayit);
            db.SaveChanges();

            return RedirectToActionPermanent("Index");
        }

        public ActionResult guncelle(int id = 0)
        {
            UretimAraclari kayit = db.UretimAraclaris.Find(id);
            return View(kayit);

        }


        [HttpPost]
        public ActionResult guncelle(UretimAraclari kayit)
        {
            db.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult sil(int id = 0)
        {
            UretimAraclari kayit = db.UretimAraclaris.Find(id);
            db.UretimAraclaris.Remove(kayit);

            db.SaveChanges();
            return RedirectToAction("Index");

        }


        [HttpPost]

        public FileResult Excell10()
        {
            DataTable dt = new DataTable("UretimAraclari");


            dt.Columns.AddRange(new DataColumn[12]
                {

                     new DataColumn("Makine Kodu Ve Makine Adı"),
                     new DataColumn("Bakım Türü"),
                     new DataColumn("İlk 6 Ay"),
                     new DataColumn("Son 6 Ay"),
                     new DataColumn("Başlangıç Tarihi"),
                       new DataColumn("Başlangıç Saati"),
                         new DataColumn("Bitiş Tarihi"),
                           new DataColumn("Bitiş Saati"),
                            new DataColumn("Yapılan Müdahale"),
                            new DataColumn("Bakımı Yapan"),
                             new DataColumn("Toplam Geçen Süre	"),
                             new DataColumn("Bakım Maaliyeti")
                           



                });

            var emps = from UretimAraclari in db.UretimAraclaris.ToList() select UretimAraclari;

            foreach (var UretimAraclari in emps)
            {
                dt.Rows.Add(UretimAraclari.MakineKodu,

             UretimAraclari.BakimTuru,
              UretimAraclari.Ay,
                UretimAraclari.Ay2,
              UretimAraclari.BaslangicTarihi,
                UretimAraclari.Ilk1,

             UretimAraclari.BitisTarihi,

                UretimAraclari.Son1,
             UretimAraclari.YapilanMudahale,
              

              UretimAraclari.BakimYapan,
              UretimAraclari.GecenSure,
             UretimAraclari.BakimMaaliyeti
              );

            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "F-30_Üretim_Araçları_Bakım_Planı.xlsx");
                }
            }


        }

    }
}