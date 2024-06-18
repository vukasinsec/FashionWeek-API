using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class CasopisiIdView
    {
        public  ManekenView Maneken { get; set; }
        public  string NaziviCasopisa { get; set; }
        public CasopisiIdView()
        {
        }

        internal CasopisiIdView(CasopisiId? c)
        {
            if (c != null)
            {
                Maneken = new ManekenView(c.Maneken);
                NaziviCasopisa = c.NaziviCasopisa;
            }
        }
    }
}
