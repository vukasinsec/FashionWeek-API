using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
     internal class CasopisiMapiranja : ClassMap<FashionWeekLibrary.Entiteti.Casopisi>
    {
        public CasopisiMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("CASOPISI");


            CompositeId(x => x.Id)
                     .KeyReference(x => x.Maneken, "MANEKEN")
                        .KeyProperty(x => x.NaziviCasopisa, "NAZIVICASOPISA");


          //  Map(x => x.Id.NaziviCasopisa).Column("NAZIVICASOPISA");
          //  Map(x => x.Id.Maneken.MBR).Column("MANEKEN");


        }
    }
}
