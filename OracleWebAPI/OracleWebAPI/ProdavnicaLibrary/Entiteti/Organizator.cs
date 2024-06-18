using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class Organizator
    {
        internal protected virtual  int OrganizatorID { get; set; }//provera gde treba required!!!!!
        internal protected virtual string? Kolekcija { get; set; }
        internal protected virtual char PrvaModnaRevija { get; set; }

        internal protected virtual IList<ModnaRevija> ModneRevije { get; set; }
        internal protected virtual IList<ModnaKuca> ModneKuce { get; set; }
        internal protected virtual IList<ModniKreator> ModniKreatori { get; set; }


        internal Organizator()
        {

            ModneRevije = new List<ModnaRevija>();
            ModneKuce = new List<ModnaKuca>();
            ModniKreatori = new List<ModniKreator>();
        }
    }
}
