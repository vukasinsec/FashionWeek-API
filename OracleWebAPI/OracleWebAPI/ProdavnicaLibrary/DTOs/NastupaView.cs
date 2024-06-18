using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class NastupaView
    {
        public  NastupaIdView Id { get; set; }

        internal NastupaView(Nastupa? n)
        {
            if (n != null)
            {
                Id = new NastupaIdView(n.Id);
               
            }
        }

        public NastupaView()
        {
        }

    }
}
