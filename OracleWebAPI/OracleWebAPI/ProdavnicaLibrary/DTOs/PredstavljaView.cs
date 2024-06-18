using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class PredstavljaView
    {
        public PredstavljaIdView Id { get; set; }

        public PredstavljaView() { }
        internal PredstavljaView(Predstavlja? p)
        {
            if (p != null)
            {
                Id = new PredstavljaIdView(p.Id);

            }
        }
    }
}
