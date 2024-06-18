using FluentNHibernate.Automapping.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class Maneken:Osoba
    {
        //  public virtual string MBR { get; set; }//fk
        internal protected virtual string BojaKose { get; set; }
        internal protected virtual int Visina { get; set; }
        internal protected virtual string BojaOciju { get; set; }
        internal protected virtual float Tezina { get; set; }
        internal protected virtual string KonfekcijskiBroj { get; set; }
        internal protected virtual ModnaAgencija PIBModneAgencije { get; set; }//fk

        internal protected virtual IList<ModnaRevija> ModneRevijeManekeni { get; set; }
        internal protected virtual IList<Nastupa> NastupaManeken { get; set; }

        internal protected virtual IList<Casopisi> UcasopisimaManeken { get; set; }

        internal Maneken()
        {
            ModneRevijeManekeni = new List<ModnaRevija>();
            NastupaManeken = new List<Nastupa>();
            UcasopisimaManeken = new List<Casopisi>();
        }

    }
}
