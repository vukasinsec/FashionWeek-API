using FashionWeekLibrary.Entiteti;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeek.Mapiranja
{
     class ModnaAgencijaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.ModnaAgencija>
    {
        public ModnaAgencijaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("MODNAAGENCIJA");

         //   DiscriminateSubClassesOnColumn("PINTERNACIONALNA");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.PIB, "PIB").GeneratedBy.Assigned();//assigned za podatak koji neko unosi

            // Sva ostala svojstva
            Map(p => p.Naziv, "NAZIV");
            Map(p => p.DatumOsnivanja, "DATUMOSNIVANJA");
            Map(p => p.Drzava, "DRZAVA");
            Map(p => p.Grad, "GRAD");
         //Map(p => p.PDomaca, "PDOMACA");
            Map(p => p.PInternacionalna, "PINTERNACIONALNA");

            HasMany(x => x.Manekeni).KeyColumn("PIBMODNEAGENCIJE").LazyLoad().Cascade.All().Inverse();

            HasMany(x => x.NaziviZemalja).KeyColumn("MODNAAGENCIJA").LazyLoad().Cascade.All().Inverse();


        }
    }

 /*  class InternacionalnaMapiranja:SubclassMap<InternacionalnaAgencija>
    {
        public InternacionalnaMapiranja()
        {
            DiscriminatorValue(1);
            HasMany(x => x.NaziviZemalja).KeyColumn("MODNAAGENCIJA").LazyLoad().Cascade.All().Inverse();
        }
    }

    class DomacaMapiranja : SubclassMap<DomacaAgencija>
    {
        public DomacaMapiranja()
        {
            DiscriminatorValue(0);
            
        }
    }
 */
  
}
