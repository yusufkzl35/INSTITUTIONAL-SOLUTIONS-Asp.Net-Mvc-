using Erenbook.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
namespace Erenbook.Models
{
    [LoginFilter]
    public class KestirimciBakim
    {
        public  int  id { get; set; }

        [Display(Name="Tezgah Kodu")]
        public string TezgahKodu { get; set; }

        [Display(Name ="Tezgah Adı")]
        public string TezgahAdi { get; set; }

        [Display(Name ="Ölçüm Noktası")]

        public string OlcumNoktasi { get; set; }

        [Display(Name ="Kestirimci Bakım Yöntemi")]

        public string Kesyontem { get; set; }


       
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public DateTime Periyot1 { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public DateTime Periyot2 { get; set; }

        [Display(Name ="Mart")]
        public bool Mart { get; set; }

        [Display(Name = "Haziran")]
        public bool Haziran { get; set; }

        [Display(Name = "Eylül")]
        public bool Eylul { get; set; }

        [Display(Name = "Aralık")]
        public bool Aralik { get; set; }

        [Display(Name ="Kodu")]
        public string Kodu { get; set; }


        [Display(Name ="Kontrol Eden")]

        public string KontrolEden { get; set; }

        [Display(Name ="Yapılan Müdahale")]

        public string Mudahale { get; set; }

        [Display(Name ="Açıklama")]

        public string Aciklama { get; set; }


        [Display(Name = "Bakım Türü")]

        public string BakimTuru { get; set; }

    }
}