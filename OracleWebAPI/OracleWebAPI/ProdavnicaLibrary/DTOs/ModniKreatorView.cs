using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class ModniKreatorView:OsobaView
    {
        public  int CenaUsluge { get; set; }

        [JsonIgnore]
        public  OrganizatorView? OrganizatorID { get; set; }
        [JsonIgnore]
        public  ModnaKucaView? NazivModneKuce { get; set; }
      //  public string NazivKuce {  get; set; }
        public  virtual IList<SpecijalniGostView>? GostNaModnojReviji { get; set; }

        public virtual IList<ModnaRevijaView> ModneRevijeMK { get; set; }

        public virtual IList<PredstavljaView> PredstavljaMK { get; set; }


        public ModniKreatorView() { 
        
            GostNaModnojReviji=new List<SpecijalniGostView>();
            ModneRevijeMK=new List<ModnaRevijaView>();
            PredstavljaMK=new List<PredstavljaView>();
        
        }

        internal ModniKreatorView(ModniKreator? m) : base(m)
        {
            CenaUsluge = m.CenaUsluge;
           // NazivKuce = m.NazivModneKuce.Naziv;
        }

        internal ModniKreatorView(ModniKreator? m, ModnaKuca? mk) : this(m)
        {
            NazivModneKuce = new ModnaKucaView(mk);
        }

        internal ModniKreatorView(ModniKreator? m, Organizator? o) : this(m)
        {
            OrganizatorID = new OrganizatorView(o);
        }
    }
}
