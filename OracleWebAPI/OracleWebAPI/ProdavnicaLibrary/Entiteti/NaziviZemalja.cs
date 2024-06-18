using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class NaziviZemalja
    {
        internal protected virtual NaziviZemaljaId Id { get; set; }

        internal NaziviZemalja()
        {

            Id = new NaziviZemaljaId();
        }


    }
}
