using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using FashionWeekLibrary.Entiteti;

namespace FashionWeek.Mapiranja
{
    internal class ModnaRevijaMapiranja:ClassMap<FashionWeekLibrary.Entiteti.ModnaRevija>
    {
        public ModnaRevijaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("MODNAREVIJA");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.IdModneRevije, "IDMODNEREVIJE").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi

            // Sva ostala svojstva
            Map(p => p.RedniBroj, "REDNIBROJ");
            Map(p => p.Naziv, "NAZIV");
            Map(p => p.Grad, "GRAD");
            Map(p => p.DatumOdrzavanja, "DATUMODRZAVANJA");
            Map(p => p.VremeOdrzavanja, "VREMEODRZAVANJA");
            Map(p => p.ImeJavneLicnosti, "IMEJAVNELICNOSTI");
            Map(p => p.PrezimeJavneLicnosti, "PREZIMEJAVNELICNOSTI");
            Map(p => p.ZanimanjeJL, "ZANIMANJEJL");

            //
             References(x => x.OrganizatorID).Column("ORGANIZATORID").LazyLoad();

            HasManyToMany(x => x.Manekeni)
                .Table("NASTUPA")
                .ParentKeyColumn("MODNAREVIJA")
                .ChildKeyColumn("MANEKENMBR")
                .Cascade.All()
                .Inverse();

            HasManyToMany(x => x.ModniKreatori)
                .Table("PREDSTAVLJA")
                .ParentKeyColumn("IDMODNEREVIJE")
                .ChildKeyColumn("MBR")
                .Cascade.All()
                  .Inverse();

           //   HasMany(x => x.PredstavljajuMK).KeyColumn("IDMODNEREVIJE").LazyLoad().Cascade.All().Inverse();

          //    HasMany(x => x.NastupajuManekeni).KeyColumn("IDMODNEREVIJE").LazyLoad().Cascade.All().Inverse();
            

            HasMany(x => x.GostiNaModnojReviji).KeyColumn("IDMODNEREVIJE").LazyLoad().Cascade.All().Inverse();
        }

    }
}
