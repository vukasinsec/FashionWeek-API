using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class PredstavljaIdView
    {
        public   ModniKreatorView MKPredstavlja { get; set; }
        public   ModnaRevijaView NaModnojReviji { get; set; }

        public PredstavljaIdView()
        {
        }

        internal PredstavljaIdView(PredstavljaId? p)
        {
            if (p != null)
            {
                MKPredstavlja = new ModniKreatorView(p.MKPredstavlja);
                NaModnojReviji = new ModnaRevijaView(p.NaModnojReviji);
            }
        }
    }
}
