using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class OsobaView
    {

        public  string? MBR { get; set; }
        public  char Pol { get; set; }
        public  DateTime DatumRodjenja { get; set; }
        public  string? ZemljaPorekla { get; set; }
        public  string? Ime { get; set; }
        public  string? Prezime { get; set; }


        internal OsobaView(Osoba? o) {
            if (o != null)
            {
                MBR=o.MBR; Pol=o.Pol;
                DatumRodjenja=o.DatumRodjenja;
                ZemljaPorekla=o.ZemljaPorekla;
                Ime=o.Ime;
                Prezime=o.Prezime;
            }
        }  

        public OsobaView() { }
    }
}
