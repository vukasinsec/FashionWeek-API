using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class OrganizatorView
    {
        public  int OrganizatorID { get; set; }//provera gde treba required!!!!!
        public  string? Kolekcija { get; set; }
        public  char PrvaModnaRevija { get; set; }

        public virtual IList<ModnaRevijaView> ModneRevije { get; set; }
        public virtual IList<ModnaKucaView> ModneKuce { get; set; }
        public virtual IList<ModniKreatorView> ModniKreatori { get; set; }


        public OrganizatorView()
        {

            ModneRevije = new List<ModnaRevijaView>();
            ModneKuce = new List<ModnaKucaView>();
            ModniKreatori = new List<ModniKreatorView>();
        }

        internal OrganizatorView(Organizator? o) : this()
        {
            OrganizatorID = o.OrganizatorID;
            Kolekcija = o.Kolekcija;
            PrvaModnaRevija= o.PrvaModnaRevija;

        }
    }
}
