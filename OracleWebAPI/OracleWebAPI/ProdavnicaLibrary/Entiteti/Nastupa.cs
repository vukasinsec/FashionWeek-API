using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class Nastupa
    {
        internal protected virtual NastupaId Id { get; set; }
       

        internal Nastupa()
        {

            Id = new NastupaId();
        }
    }
}
