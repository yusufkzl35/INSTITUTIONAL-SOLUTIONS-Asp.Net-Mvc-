using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Erenbook.Models
{
    public class UretimAraclari
    {
        public int id { get; set; }

       

        [Display(Name = "Makine Kodu Ve Makine Adı")]
        public string MakineKodu { get; set; }

        [Display(Name = "Makine Adı")]
        public string MakineAdi { get; set; }


        [Display(Name = "Planlı Bakım")]
        public bool PlanliBakim { get; set; }

        [Display(Name = "İlk 6 Ay")]
        public string Ay { get; set; }

        [Display(Name = "Son 6 Ay")]
        public string Ay2 { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [Display(Name = "Başlangıç Saati")]
        public TimeSpan Ilk1 { get; set; }


        [DataType(DataType.Date)]

        [Display(Name = "Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }


        [Display(Name = "Bitiş Saati")]

        public TimeSpan Son1 { get; set; }

        [Display(Name = "Yapılan Müdahale")]
        public string YapilanMudahale { get; set; }

       


        [Display(Name = "Bakımı Yapan")]
        public string BakimYapan { get; set; }

        [DisplayName("Toplam Geçen Süre")]

        [NotMapped]
        public TimeSpan GecenSure

        {
            get
            {
                return this.Son1 - this.Ilk1;
            }
        }



        [Display(Name = "Bakım Türü")]
        public string BakimTuru { get; set; }

        [Display(Name = "Bakım Maaliyeti")]
        public string BakimMaaliyeti { get; set; }

    }
}