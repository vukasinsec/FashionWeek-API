using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
     internal class OsobaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.Osoba>
    {
        public OsobaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("OSOBA");
            UseUnionSubclassForInheritanceMapping();
            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.MBR, "MBR").GeneratedBy.Assigned().Column("MBR");//assigned za podatak koji neko unosi

            // DiscriminateSubClassesOnColumn("TIPOSOBE");
           // DiscriminateSubClassesOnColumn("TIPOSOBE").Not.Nullable();
            //ili key_column("MBR");
            // Sva ostala svojstva
            Map(p => p.Pol, "POL");
            Map(p => p.DatumRodjenja, "DATUMRODJENJA");
            Map(p => p.ZemljaPorekla, "ZEMLJAPOREKLA");
            Map(p => p.Ime, "IME");
            Map(p => p.Prezime, "PREZIME");
            //Map(p => p.TipOsobe, "TIPOSOBE");



        }
    }
}
