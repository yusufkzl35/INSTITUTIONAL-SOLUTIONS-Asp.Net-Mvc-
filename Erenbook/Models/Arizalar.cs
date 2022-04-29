using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Erenbook.Models
{
    public class Arizalar
    {
        public int id { get; set; }



        [Display(Name = "Makine Kodu Ve Makine Adı")]
        public string MakineKodu { get; set; }

        [Display(Name = "Makine Adı")]
        public string MakineAdi { get; set; }

        [Display(Name = "Arıza Nedeni")]
        public string ArizaNedeni { get; set; }


        [DataType(DataType.Date)]

        [Display(Name = "Arıza Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Arıza Müdahale Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ArizaMudahaleTarihi { get; set; }


        [Display(Name = "Arıza Başlangıç Saati")]

        public TimeSpan Ilk1 { get; set; }
       

        [Display(Name = "Bakımcının Geldiği Saat")]

        public TimeSpan MudahaleSaati { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Arıza Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }


        [Display(Name = "Arıza Bitiş Saati")]

        public TimeSpan Son1 { get; set; }

        [Display(Name = "Yapılan Müdahale")]
        public string YapilanMudahale { get; set; }


        [Display(Name = "Müdahale Açıklaması")]
        public string MudahaleAciklama { get; set; }

        [Display(Name = "Görevli")]
        public string BakimYapan { get; set; }

        [DisplayName("Arıza Toplam Geçen Süre")]

        [NotMapped]
        public TimeSpan GecenSure

        {
            get
            {
                return this.Son1 - this.Ilk1;
            }
        }

        [DisplayName("Arıza Müdahale Süresi")]

        [NotMapped]
        public TimeSpan MudahaleSuresi

        {
            get
            {
                return this.Son1 - this.MudahaleSaati;
            }
        }

        [DisplayName("Arızayı Bildiren Kişi")]
        public string ArizaBildiren { get; set; }

        [DisplayName("Arıza Açıklaması")]
        public  string ArizaAciklama { get; set; }

        [DisplayName("Kullanılan Malzeme")]
        public string KullanilanMalzeme { get; set; }

        [DisplayName("Malzeme Bekleme Süresi")]
        public string Malbeklemesuresi { get; set; }

        public virtual ICollection<ArizaResimleri> ArizaResimleris { get; set; }
        

    }
}