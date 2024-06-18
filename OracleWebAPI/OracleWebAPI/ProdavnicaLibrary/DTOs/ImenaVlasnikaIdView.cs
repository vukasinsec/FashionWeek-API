using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class ImenaVlasnikaIdView
    {
        public  ModnaKucaView ModnaKuca { get; set; }
        public  string ImenaVlasnika { get; set; }
        public ImenaVlasnikaIdView()
        {
        }

        internal ImenaVlasnikaIdView(ImenaVlasnikaId? iv)
        {
            if (iv != null)
            {
                ModnaKuca = new ModnaKucaView(iv.ModnaKuca);
                ImenaVlasnika = iv.ImenaVlasnika;
            }
        }
    }
}
