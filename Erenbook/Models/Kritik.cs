using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erenbook.Models
{
    public class Kritik
    {
        public int id { get; set; }

        [Display(Name = "Makine Kodu")]
        public string MakineKodu { get; set; }

        [Display(Name = "Makine Adı")]
        public string MakineAdi { get; set; }

        [Display(Name = "Kritik Yedek Parçalar")]
        public string Kyedekparca { get; set; }

        [Display(Name = "Parça Stok Kodu")]
        public string PStokKodu { get; set; }

        [Display(Name = "Sipariş Edilecek Miktar")]
        public string SipMiktar { get; set; }

        [Display(Name = "Satın Alma Adedi")]
        public string Adet { get; set; }
    }
}