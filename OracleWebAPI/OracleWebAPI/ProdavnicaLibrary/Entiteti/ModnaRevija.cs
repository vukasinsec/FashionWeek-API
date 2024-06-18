using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class ModnaRevija
    {
        internal protected virtual int IdModneRevije { get; set; }
        internal protected virtual int RedniBroj { get; set; }
        internal protected virtual string? Naziv { get; set; }
        internal protected virtual string? Grad { get; set; }
        internal protected virtual DateTime DatumOdrzavanja { get; set; }
        internal protected virtual int VremeOdrzavanja { get; set; }
        internal protected virtual Organizator OrganizatorID { get; set; }
        internal protected virtual string? ImeJavneLicnosti { get; set; }
        internal protected virtual string? PrezimeJavneLicnosti { get; set; }
        internal protected virtual string? ZanimanjeJL { get; set; }


        internal protected virtual IList<Maneken> Manekeni { get; set; }
        internal protected virtual IList<ModniKreator> ModniKreatori { get; set; }

        internal protected virtual IList<Predstavlja> PredstavljajuMK { get; set; }

        internal protected virtual IList<Nastupa> NastupajuManekeni { get; set; }
        internal protected virtual IList<SpecijalniGost>? GostiNaModnojReviji { get; set; } 
        internal ModnaRevija()
        {
            Manekeni = new List<Maneken>();
            ModniKreatori = new List<ModniKreator>();
            PredstavljajuMK = new List<Predstavlja>();
            NastupajuManekeni = new List<Nastupa>();
            GostiNaModnojReviji=new List<SpecijalniGost>();
        }
    }
}
