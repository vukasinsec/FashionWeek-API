using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal abstract class Osoba
    {

        internal protected virtual string MBR { get; set; }
        internal protected virtual char Pol { get; set; }
        internal protected virtual DateTime DatumRodjenja { get; set; }
        internal protected virtual string? ZemljaPorekla { get; set; }
        internal protected virtual string? Ime { get; set; }
        internal protected virtual string? Prezime { get; set; }
       // public virtual string? TipOsobe { get; set; }

       

    }
}
