using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class SpecijalniGostView
    {
        public int IDGosta { get; set; }
        public ModniKreatorView MBRModniKreator { get; set; }

        public ModnaRevijaView IDModneRevije { get; set; }


        public SpecijalniGostView()
        {

        }

        internal SpecijalniGostView(SpecijalniGost? sg)
        {
            if (sg != null)
            {
                IDGosta=sg.IDGosta;
                MBRModniKreator = new ModniKreatorView(sg.MBRModniKreator);
                IDModneRevije = new ModnaRevijaView(sg.IDModneRevije);

            }
        }
    }
}
