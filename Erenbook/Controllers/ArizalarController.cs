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
    public class ArizalarController : Controller
    {

        EREN db = new EREN();

        public ActionResult Index(string aranan = null, int sayfa = 1)
        {
            var t = from x in db.Arizalars select x;
            if (!String.IsNullOrEmpty(aranan))
            {
                t = db.Arizalars.Where(kayit => kayit.MakineKodu.Contains(aranan));

            }
            return View(t.OrderByDescending(kayit => kayit.id).ThenBy(x => x.id).ToPagedList(sayfa, 20));
        }



        public ActionResult ekle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ekle(Arizalar kayit)
        {


            db.Arizalars.Add(kayit);
            db.SaveChanges();

            return RedirectToActionPermanent("Index");
        }

        public ActionResult resimler(int sayfaid = 0)
        {
            return View(db.ArizaResimleris.Where(kayit => kayit.sayfa_id == sayfaid).ToList());
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
                //string filePath = Path.Combine(Server.MapPath("~/Content/ArizaResimleri"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(resimadi.FileName)); resimadi.SaveAs(filePath);
                string filePath = Path.Combine(Server.MapPath("~/Content/ArizaResimleri"), Path.GetFileName(resimadi.FileName)); resimadi.SaveAs(filePath);

                ArizaResimleri sr = new ArizaResimleri();
                sr.sayfa_id = sayfaid;
                sr.resimadi = Path.GetFileName(filePath);
                db.ArizaResimleris.Add(sr);

                db.SaveChanges();
                // return Json("Seçili dosya Eklendi!");

            }

            return RedirectToActionPermanent("Index");

        }

        public ActionResult resimsil(int[] resim_id)
        {
            foreach (int resimID in resim_id)
            {
                ArizaResimleri kayit = db.ArizaResimleris.Find(resimID);
                db.ArizaResimleris.Remove(kayit);

                string fullPath = Request.MapPath("~/Content/ArizaResimleri/" + kayit.resimadi);

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
            Arizalar kayit = db.Arizalars.Find(id);
            return View(kayit);

        }

        [HttpPost]
        public ActionResult Duzelt(Arizalar kayit)
        {
            db.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]

        public FileResult Excell3()
        {
            DataTable dt = new DataTable("Arizalar");


            dt.Columns.AddRange(new DataColumn[16]
                {

                     new DataColumn("Makine Kodu"),
                     new DataColumn("Arıza Başlangıç Tarihi"),
                     new DataColumn("Arıza Başlangıç Saati"),
                     
                     new DataColumn("Arıza Müdahale Tarihi"),
                     new DataColumn("Bakımcını Geldiği Saat"),
                      new DataColumn("Arıza Açıklaması"),
                        new DataColumn("Arıza Nedeni"),
                         new DataColumn("Yapılan Müdahale"),
                           new DataColumn("Müdahale Açıklaması"),
                            new DataColumn("Kullanılan Malzeme"),
                             new DataColumn("Malzeme Bekleme Süresi"),
                       new DataColumn("Arıza Bitiş Tarihi"),
                         new DataColumn("Arıza Bitiş Saati"),
                             new DataColumn("Görevli"),
                             new DataColumn("Arıza Müdahale Süresi"),
                             new DataColumn("Arıza Toplam Geçen Süre")



                });

            var emps = from Ariza in db.Arizalars.ToList() select Ariza;

            foreach (var Ariza in emps)
            {
                dt.Rows.Add(Ariza.MakineKodu,

             Ariza.BaslangicTarihi.ToShortDateString(),
              Ariza.Ilk1,
           
              Ariza.ArizaMudahaleTarihi.ToShortDateString(),                
              Ariza.MudahaleSaati,
              Ariza.ArizaAciklama,
                 Ariza.ArizaNedeni,
                   Ariza.YapilanMudahale,
                    Ariza.MudahaleAciklama,
                    Ariza.KullanilanMalzeme,
                    Ariza.Malbeklemesuresi,
                Ariza.BitisTarihi.ToShortDateString(),
              Ariza.Son1,
              Ariza.BakimYapan,
               Ariza.MudahaleSuresi.TotalMinutes,
               Ariza.GecenSure.TotalMinutes

              );

            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MekanikArizaListesi.xlsx");
                }
            }


        }

    }
}