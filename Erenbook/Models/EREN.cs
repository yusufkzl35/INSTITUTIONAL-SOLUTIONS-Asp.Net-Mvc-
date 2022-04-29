using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Erenbook.Models
{
    public class EREN:DbContext
    {
        
      
        public DbSet<Kullanici> kullanicis { get; set; }

        public DbSet<KestirimciBakim> KestirimciBakims { get; set; }

        public DbSet<UretimAraclari> UretimAraclaris { get; set; }

        public DbSet<Kritik> Kritiks { get; set; }   
        public DbSet<Arizalar> Arizalars { get; set; }

        public DbSet<ArizaResimleri> ArizaResimleris { get; set; }

        public DbSet<Arizalar2> Arizalars2 { get; set; }

        public DbSet<ArizaResimleri2> ArizaResimleris2 { get; set; }

    }
}

