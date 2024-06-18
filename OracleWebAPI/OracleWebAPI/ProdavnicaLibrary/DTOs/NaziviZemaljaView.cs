using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class NaziviZemaljaView
    {
        public NaziviZemaljaIdView Id { get; set; }

        internal NaziviZemaljaView(NaziviZemalja? nz)
        {
            if (nz != null)
            {
                Id = new NaziviZemaljaIdView(nz.Id);

            }
        }
    }
}
