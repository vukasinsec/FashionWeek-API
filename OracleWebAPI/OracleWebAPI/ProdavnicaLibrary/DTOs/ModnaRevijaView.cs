using FashionWeekLibrary.Entiteti;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace FashionWeekLibrary.DTOs
{
    public class ModnaRevijaView
    {
        public int IdModneRevije { get; set; }
        public  int RedniBroj { get; set; }
        public  string? Naziv { get; set; }
        public  string? Grad { get; set; }
        public  DateTime DatumOdrzavanja { get; set; }
        public  int VremeOdrzavanja { get; set; }

        [JsonIgnore]
        public  OrganizatorView? OrganizatorID { get; set; }
        public  string? ImeJavneLicnosti { get; set; }
        public  string? PrezimeJavneLicnosti { get; set; }
        public  string? ZanimanjeJL { get; set; }


        public virtual IList<ManekenView> Manekeni { get; set; }
        public virtual IList<ModniKreatorView> ModniKreatori { get; set; }

        public virtual IList<PredstavljaView> PredstavljajuMK { get; set; }

        public virtual IList<NastupaView> NastupajuManekeni { get; set; }
        public virtual IList<SpecijalniGostView>? GostiNaModnojReviji { get; set; }


        public ModnaRevijaView() {  
        
            Manekeni = new List<ManekenView>();
            ModniKreatori= new List<ModniKreatorView>();
            PredstavljajuMK = new List<PredstavljaView>();
            NastupajuManekeni=new List<NastupaView>();
            GostiNaModnojReviji=new List<SpecijalniGostView>();
        
        
        }

        internal ModnaRevijaView(ModnaRevija? m)
        {
            if (m != null)
            {
                IdModneRevije = m.IdModneRevije;
                RedniBroj = m.RedniBroj;
                Naziv=m.Naziv;  
                DatumOdrzavanja=m.DatumOdrzavanja;
                VremeOdrzavanja = m.VremeOdrzavanja;
                ImeJavneLicnosti=m.ImeJavneLicnosti;
                PrezimeJavneLicnosti = m.PrezimeJavneLicnosti;
                ZanimanjeJL=m.ZanimanjeJL;
            }
        }
        internal ModnaRevijaView(ModnaRevija? m, Organizator? o) : this(m)
        {
            OrganizatorID = new OrganizatorView(o);
        }



    }
}
