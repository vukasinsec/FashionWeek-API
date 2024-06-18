using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class ManekenView : OsobaView
    {
        public string BojaKose { get; set; }
        public int Visina { get; set; }
        public string BojaOciju { get; set; }
        public float Tezina { get; set; }
        public string KonfekcijskiBroj { get; set; }

        [JsonIgnore]
        public ModnaAgencijaView? PIBModneAgencije { get; set; } 
       
      
       // public string NazivAgencije { get; set; }
        public virtual IList<ModnaRevijaView> ModneRevijeManekeni { get; set; }
        public virtual IList<NastupaView> NastupaManeken { get; set; }

        public virtual IList<CasopisiView> UcasopisimaManeken { get; set; }
        public ManekenView() {
        
               ModneRevijeManekeni=new List<ModnaRevijaView>();
               NastupaManeken=new List<NastupaView>();
               UcasopisimaManeken=new List<CasopisiView>();    
        
        }

        internal ManekenView(Maneken? m) : base(m)
        {
            BojaKose = m.BojaKose;
            BojaOciju = m.BojaOciju;
            Visina = m.Visina;
             Tezina = m.Tezina;
            KonfekcijskiBroj=m.KonfekcijskiBroj;
          //  NazivAgencije = m.PIBModneAgencije.Naziv;

         //  PIBModneAgencije = new ModnaAgencijaView(m.PIBModneAgencije);

        }

        internal ManekenView(Maneken? m,ModnaAgencija? ma) : this(m)
        {
            PIBModneAgencije=new ModnaAgencijaView(ma);
        }
      
    }

    
}
