using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Mapiranja
{
     internal class ImenaVlasnikaMapiranja : ClassMap<FashionWeekLibrary.Entiteti.ImenaVlasnika>
    {
        public ImenaVlasnikaMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("IMENAVLASNIKA");

            CompositeId(x => x.Id)
                     .KeyReference(x => x.ModnaKuca, "MODNAKUCA")
                        .KeyProperty(x => x.ImenaVlasnika, "IMENAVLASNIKA");
           
           



        }
    }
}
