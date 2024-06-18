using FashionWeekLibrary.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.DTOs
{
    public class CasopisiView
    {
        public CasopisiIdView Id { get; set; }

        internal CasopisiView(Casopisi? c)
        {
            if (c != null)
            {
                Id = new CasopisiIdView(c.Id);

            }
        }
    }
}
