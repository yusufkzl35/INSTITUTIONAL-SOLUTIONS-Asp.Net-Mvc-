using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erenbook.Models
{
    public class Kullanici
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Adı")]
        public string adi { get; set; }


        [Display(Name = "Soyadı")]
        public string soyadi { get; set; }


        [Display(Name = "Kullanıcı Adı")]
        public string kadi { get; set; }

        [DataType(DataType.Password)]


        [Display(Name = "Şifre")]
        public string sifre { get; set; }
    }
}