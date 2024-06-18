using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class ImenaVlasnika
    {
        internal protected virtual ImenaVlasnikaId Id { get; set; }

        internal ImenaVlasnika()
        {
            Id = new ImenaVlasnikaId();
        }

    }
}
