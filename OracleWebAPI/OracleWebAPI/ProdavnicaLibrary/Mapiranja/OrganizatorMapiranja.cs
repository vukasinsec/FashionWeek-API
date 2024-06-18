using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
    internal class OrganizatorMapiranja : ClassMap<FashionWeekLibrary.Entiteti.Organizator>
    {
        public OrganizatorMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("ORGANIZATOR");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.OrganizatorID, "ORGANIZATORID").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi

            // Sva ostala svojstva
            Map(p => p.Kolekcija, "KOLEKCIJA");
            Map(p => p.PrvaModnaRevija, "PRVAMODNAREVIJA");


           HasMany(x => x.ModneRevije).KeyColumn("OrganizatorID").Cascade.All();
            HasMany(x => x.ModniKreatori).KeyColumn("ORGANIZATORID").Cascade.All();
            HasMany(x => x.ModneKuce).KeyColumn("ORGANIZATORID").Cascade.All();


        }

    }
}
