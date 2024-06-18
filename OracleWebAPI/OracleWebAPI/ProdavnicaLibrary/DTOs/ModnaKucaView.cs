using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public  class ModnaKucaView
    {
        public  string Naziv { get; set; }
        public  string? ImeOsnivaca { get; set; }
        public  string? PrezimeOsnivaca { get; set; }
        public  string? Drzava { get; set; }
        public  string? Grad { get; set; }

        [JsonIgnore]
        public  OrganizatorView? OrganizatorID { get; set; }

        public virtual IList<ModniKreatorView> ModniKreatori { get; set; }

        public virtual IList<ImenaVlasnikaView> Vlasnici { get; set; }

        public ModnaKucaView()
        {

            ModniKreatori = new List<ModniKreatorView>();
            Vlasnici = new List<ImenaVlasnikaView>();
        }

        internal ModnaKucaView(ModnaKuca? m) : this()
        {
            Naziv = m.Naziv;
            ImeOsnivaca = m.ImeOsnivaca;
            PrezimeOsnivaca= m.PrezimeOsnivaca;
            Drzava = m.Drzava;
            Grad = m.Grad;

        }

        internal ModnaKucaView(ModnaKuca? m, Organizator? o) : this(m)
        {
            OrganizatorID = new OrganizatorView(o);
        }

    }
}
