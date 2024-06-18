using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class NastupaId
    {
        internal protected virtual Maneken? ManekenNaReviji { get; set; }
        internal protected virtual ModnaRevija? NaReviji { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(NastupaId))
            {
                return false;
            }

            NastupaId compare = (NastupaId)obj;

            return ManekenNaReviji.MBR == compare.ManekenNaReviji.MBR &&
                   NaReviji.Naziv == compare.NaReviji.Naziv;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
