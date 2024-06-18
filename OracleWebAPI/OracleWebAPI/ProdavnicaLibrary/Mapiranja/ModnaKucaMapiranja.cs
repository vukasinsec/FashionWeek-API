using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
     internal class ModnaKucaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.ModnaKuca>
    {
        public ModnaKucaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("MODNAKUCA");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.Naziv, "NAZIV").GeneratedBy.Assigned();//assigned za podatak koji neko unosi

           

            Map(p => p.ImeOsnivaca, "IME");
            Map(p => p.PrezimeOsnivaca, "PREZIME");
            Map(p => p.Drzava, "DRZAVA");
            Map(p => p.Grad, "GRAD");

            HasMany(x => x.ModniKreatori).KeyColumn("NAZIVMODNEKUCE").LazyLoad().Cascade.All().Inverse();

            HasMany(x => x.Vlasnici).KeyColumn("MODNAKUCA").Cascade.All().Inverse();

            References(x => x.OrganizatorID).Column("ORGANIZATORID").LazyLoad();


        }

    }
}
