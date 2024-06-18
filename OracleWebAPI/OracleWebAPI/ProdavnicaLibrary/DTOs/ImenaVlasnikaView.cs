using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class ImenaVlasnikaView
    {
        public ImenaVlasnikaIdView Id { get; set; }

        internal ImenaVlasnikaView(ImenaVlasnika? iv)
        {
            if (iv != null)
            {
                Id = new ImenaVlasnikaIdView(iv.Id);

            }
        }
    }
}
