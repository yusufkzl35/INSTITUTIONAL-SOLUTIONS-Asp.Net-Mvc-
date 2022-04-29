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
    public class Arizalar2Controller : Controller
    {

        EREN db = new EREN();

        public ActionResult Index(string aranan = null, int sayfa = 1)
        {
            var t = from x in db.Arizalars2 select x;
            if (!String.IsNullOrEmpty(aranan))
            {
                t = db.Arizalars2.Where(kayit => kayit.MakineKodu.Contains(aranan)|| kayit.MudahaleSinifi.Contains(aranan));

            }
            return View(t.OrderByDescending(kayit => kayit.id).ThenBy(x => x.id).ToPagedList(sayfa, 35));
        }



        public ActionResult ekle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ekle(Arizalar2 kayit)
        {


            db.Arizalars2.Add(kayit);
            db.SaveChanges();

            return RedirectToActionPermanent("Index");
        }

        public ActionResult resimler(int sayfaid = 0)
        {
            return View(db.ArizaResimleris2.Where(kayit => kayit.sayfa_id == sayfaid).ToList());
        }
        public ActionResult resimekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resimekle(HttpPostedFileBase resimadi, int sayfaid)
        {
            if (resimadi.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Content/ElektrikArizaFotolari"), Path.GetFileName(resimadi.FileName)); resimadi.SaveAs(filePath);

                ArizaResimleri2 sr = new ArizaResimleri2();
                sr.sayfa_id = sayfaid;
                sr.resimadi = Path.GetFileName(filePath);
                db.ArizaResimleris2.Add(sr);

                db.SaveChanges();
               

            }
           // return Json("Seçili dosyalar eklendi", "Index");
            return RedirectToActionPermanent("Index");

        }

        public ActionResult resimsil(int[] resim_id)
        {
            foreach (int resimID in resim_id)
            {
                ArizaResimleri2 kayit = db.ArizaResimleris2.Find(resimID);
                db.ArizaResimleris2.Remove(kayit);

                string fullPath = Request.MapPath("~/Content/ElektrikArizaFotolari/" + kayit.resimadi);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }
            db.SaveChanges();
            return Json("Seçili dosyalar silindi!");
        }

        public ActionResult Duzelt(int id = 0)
        {
            Arizalar2 kayit = db.Arizalars2.Find(id);
            return View(kayit);                                                                                                                                  

        }

        [HttpPost]
        public ActionResult Duzelt(Arizalar2 kayit)
        {
            db.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult sil(int id = 0)
        //{
        //    Arizalar kayit = db.Arizalars.Find(id);

        //    db.Arizalars.Remove(kayit);

        //   

        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        [HttpPost]

        public FileResult Excell4()
        {
            DataTable dt = new DataTable("Arizalar");


            dt.Columns.AddRange(new DataColumn[13]
                 {

                     new DataColumn("Makine Kodu"),
                     new DataColumn("Arıza Başlangıç Tarihi"),
                     new DataColumn("Arıza Başlangıç Saati"),
                     new DataColumn("Arıza Müdahale Tarihi"),
                     new DataColumn("Arıza Müdahale Saati"),
                       new DataColumn("Arıza Bitiş Tarihi"),
                         new DataColumn("Arıza Bitiş Saati"),
                           new DataColumn("Arıza Nedeni"),
                            new DataColumn("Yapılan Müdahale"),
                            new DataColumn("Müdahale Açıklaması"),
                              new DataColumn("Görevli"),
                             new DataColumn("Arıza Müdahale Süresi"),
                             new DataColumn("Arıza Toplam Geçen Süre")
                   


                 });

            var emps = from Ariza in db.Arizalars2.ToList() select Ariza;

            foreach (var Ariza in emps)
            {
                dt.Rows.Add(Ariza.MakineKodu,

             Ariza.BaslangicTarihi.Date.ToString("dd MM yyyy"),
              Ariza.Ilk1,
                Ariza.ArizaMudahaleTarihi.ToShortDateString(),
              Ariza.MudahaleSaati,
                Ariza.BitisTarihi,
              Ariza.Son1,

                Ariza.MudahaleTipi,
              Ariza.MudahaleSinifi,
               Ariza.MudahaleAciklama,

              Ariza.BakimYapan,
              Ariza.GecenSure.TotalMinutes,
              Ariza.MudahaleSuresi.TotalMinutes

              );

            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ElektrikArizaListesi.xlsx");
                }
            }


        }

    }
}