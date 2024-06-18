using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class NaziviZemaljaIdView
    {
        public  ModnaAgencijaView Agencija { get; set; }
        public  string Zemlje { get; set; }
        public NaziviZemaljaIdView()
        {
        }

        internal NaziviZemaljaIdView(NaziviZemaljaId? nz)
        {
            if (nz != null)
            {
                Agencija = new ModnaAgencijaView(nz.Agencija);
                Zemlje = nz.Zemlje;
            }
        }
    }
}
