using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class NastupaIdView
    {
        public  ManekenView? ManekenNaReviji { get; set; }
        public  ModnaRevijaView? NaReviji { get; set; }
        public NastupaIdView()
        {
        }

        internal NastupaIdView(NastupaId? n)
        {
            if (n != null)
            {
                ManekenNaReviji = new ManekenView(n.ManekenNaReviji);
                NaReviji = new ModnaRevijaView(n.NaReviji);
            }
        }

    }
}
