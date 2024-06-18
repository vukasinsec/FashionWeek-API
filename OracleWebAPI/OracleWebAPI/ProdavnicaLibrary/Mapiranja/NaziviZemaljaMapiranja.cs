using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
     internal class NaziviZemaljaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.NaziviZemalja>
    {
        public NaziviZemaljaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("NAZIVIZEMALJA");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            //  Id(p => p.IdModneRevije, "IDMODNEREVIJE").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi
            CompositeId(p => p.Id)
                .KeyReference(p => p.Agencija, "MODNAAGENCIJA")
                .KeyProperty(p => p.Zemlje, "ZEMLJE");

          




        }
    }
}
