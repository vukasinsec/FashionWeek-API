using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FashionWeekLibrary.Entiteti
{
    internal class ModniKreator:Osoba
    {
        //  public virtual string MBR { get; set; }
        internal protected virtual int CenaUsluge { get; set; }

        internal protected virtual Organizator? OrganizatorID { get; set; }
        internal protected virtual ModnaKuca NazivModneKuce { get; set; }

        internal protected virtual IList<SpecijalniGost>? GostNaModnojReviji { get; set; }

        internal protected virtual IList<ModnaRevija> ModneRevijeMK { get; set; }

        internal protected virtual IList<Predstavlja> PredstavljaMK { get; set; }
        internal ModniKreator() {
        
           ModneRevijeMK = new List<ModnaRevija>();
           PredstavljaMK = new List<Predstavlja>();
            GostNaModnojReviji=new List<SpecijalniGost>();


        }


    }
}
