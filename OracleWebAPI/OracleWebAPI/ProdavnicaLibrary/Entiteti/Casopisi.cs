using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FashionWeekLibrary.Entiteti.Casopisi;

namespace FashionWeekLibrary.Entiteti
{
    internal class Casopisi
    {
        internal protected virtual CasopisiId Id { get; set; }

        internal Casopisi() {
             Id=new CasopisiId();
         }
        
     
    }
}
