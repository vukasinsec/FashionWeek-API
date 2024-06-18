using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class ModnaAgencijaView
    {
        public  string PIB { get; set; }
        public  string Naziv { get; set; }
        public  DateTime DatumOsnivanja { get; set; }
        public  string Drzava { get; set; }
        public  string Grad { get; set; }

        public  int PInternacionalna { get; set; }

        //  public virtual string Tip {  get; set; }
        public virtual IList<NaziviZemaljaView> NaziviZemalja { get; set; }

        public virtual IList<ManekenView>? Manekeni { get; set; }

        public ModnaAgencijaView()
        {

            Manekeni = new List<ManekenView>();
            NaziviZemalja = new List<NaziviZemaljaView>();
        }

        internal ModnaAgencijaView(ModnaAgencija? m) : this()
        {
            PIB = m.PIB;
            Naziv=m.Naziv;
            DatumOsnivanja = m.DatumOsnivanja;
            Drzava = m.Drzava;
            Grad = m.Grad;
            PInternacionalna = m.PInternacionalna;

        }

    }
}

