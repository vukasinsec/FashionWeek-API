using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
    internal class PredstavljaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.Predstavlja>
    {
        public PredstavljaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("PREDSTAVLJA");

            // Primarni ključ koji se generiše korišćenjem trigger-a
            // Id(p => p.MBR, "MBR").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi


            CompositeId(p => p.Id)
                .KeyReference(p => p.MKPredstavlja, "MBR")
                .KeyReference(p => p.NaModnojReviji, "IDMODNEREVIJE");




        }
    }
}
