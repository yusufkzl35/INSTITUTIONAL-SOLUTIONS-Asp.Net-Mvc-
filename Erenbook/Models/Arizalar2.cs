using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Erenbook.Models
{
    public class Arizalar2
    {
        public int id { get; set; }



        [Display(Name = "Makine Kodu Ve Makine Adı")]
        public string MakineKodu { get; set; }

        [Display(Name = "Makine Adı")]
        public string MakineAdi { get; set; }

        [Display(Name = "Müdahale Tipi")]
        public string MudahaleTipi { get; set; }


        [DataType(DataType.Date)]

        [Display(Name = "Arıza Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Arıza Müdahale Tarihi")]
        public DateTime ArizaMudahaleTarihi { get; set; }


        [Display(Name = "Arıza Başlangıç Saati")]

        public TimeSpan Ilk1 { get; set; }

        [Display(Name = "Arıza Müdahale Saati")]

        public TimeSpan MudahaleSaati { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Arıza Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }


        [Display(Name = "Arıza Bitiş Saati")]

        public TimeSpan Son1 { get; set; }

        [Display(Name = "Müdahale Sınıfı")]
        public string MudahaleSinifi { get; set; }


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

      public virtual ICollection<ArizaResimleri2> ArizaResimleris2 { get; set; }


    }
}