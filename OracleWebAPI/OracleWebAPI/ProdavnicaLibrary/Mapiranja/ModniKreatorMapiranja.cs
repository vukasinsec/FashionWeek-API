using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeek.Mapiranja
{
    internal class ModniKreatorMapiranja : SubclassMap<FashionWeekLibrary.Entiteti.ModniKreator>
    {
        public ModniKreatorMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("MODNIKREATOR");

            Abstract();

            // Primarni ključ koji se generiše korišćenjem trigger-a
            //  Id(p => p.MBR, "MBR").GeneratedBy.Assigned() ;//assigned za podatak koji neko unosi

            //  DiscriminatorValue("Kreator");
            KeyColumn("MBR");

           // DiscriminatorValue("MODNIKREATOR");
            Map(p => p.CenaUsluge, "CENAUSLUGE");


          
            References(x => x.NazivModneKuce).Column("NAZIVMODNEKUCE").LazyLoad();
            References(x => x.OrganizatorID).Column("ORGANIZATORID").LazyLoad();

            HasManyToMany(x => x.ModneRevijeMK)
                .Table("PREDSTAVLJA")
                .ParentKeyColumn("MBR")
                .ChildKeyColumn("IDMODNEREVIJE")
                .Cascade.All();
           


            // HasMany(x=>x.PredstavljaMR).KeyColumn("MBR").LazyLoad().Cascade.All().Inverse();
            

            HasMany(x => x.GostNaModnojReviji).KeyColumn("MBRMODNIKREATOR").LazyLoad().Cascade.All();

           /* HasManyToMany(x => x.GostNaModnojReviji)
               .Table("SPECIJALNIGOST")
               .ParentKeyColumn("MBRMODNIKREATOR")
               .ChildKeyColumn("IDMODNEREVIJE")
               .Cascade.All();
           */
        }
    }
}
