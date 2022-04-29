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
    public class KritikController : Controller
    {
        // GET: Kritik
        EREN db = new EREN();
        public ActionResult Index(string search, int? i)
        {
            List<Kritik> listemp = db.Kritiks.ToList();
            return View(db.Kritiks.Where(x => x.MakineKodu.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 20));

        }

        public ActionResult ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ekle(Kritik kayit)
        {


            db.Kritiks.Add(kayit);
            db.SaveChanges();

            return RedirectToActionPermanent("Index");
        }


        public ActionResult Duzelt(int id = 0)
        {
            Kritik kayit = db.Kritiks.Find(id);
            return View(kayit);

        }

        [HttpPost]
        public ActionResult Duzelt(Kritik kayit)
        {
            db.Entry(kayit).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult sil(int id = 0)
        {
            Kritik kayit = db.Kritiks.Find(id);
            db.Kritiks.Remove(kayit);

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]

        public FileResult Excell2()
        {
            DataTable dt = new DataTable("kritik");


            dt.Columns.AddRange(new DataColumn[5]
                {

                     new DataColumn("Makine Kodu"),
                     new DataColumn("Makine Adı"),
                     new DataColumn("Kritik Yedek Parçalar"),

                     new DataColumn("Parça Stok Kodu"),
                   
                     new DataColumn("Kritik Stok Adedi")


                });

            var emps = from Kritik in db.Kritiks.ToList() select Kritik;

            foreach (var kritik in emps)
            {
                dt.Rows.Add(kritik.MakineKodu,
               kritik.MakineAdi,
             kritik.Kyedekparca,
             kritik.PStokKodu,
           
             kritik.Adet);

            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Kritik.xlsx");
                }
            }


        }
    }
}