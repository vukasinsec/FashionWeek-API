using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
    internal class SpecijalniGostMapiranja : ClassMap<FashionWeekLibrary.Entiteti.SpecijalniGost>
    {
        public SpecijalniGostMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("SPECIJALNIGOST");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            Id(p => p.IDGosta, "IDGOSTA").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi

            References(x => x.MBRModniKreator).Column("MBRMODNIKREATOR").LazyLoad();
            References(x => x.IDModneRevije).Column("IDMODNEREVIJE").LazyLoad();




        }
    }
}
