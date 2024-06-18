using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace FashionWeekLibrary.Entiteti
{
    internal class ModnaKuca
    {
        internal protected virtual  string Naziv { get; set; }
        internal protected virtual string? ImeOsnivaca { get; set; }
        internal protected virtual string? PrezimeOsnivaca { get; set; }
        internal protected virtual string? Drzava { get; set; }
        internal protected virtual string? Grad { get; set; }
        //  public virtual IList<String> ImenaVlasnika { get; set; }
        internal protected virtual Organizator? OrganizatorID { get; set; }

        internal protected virtual IList<ModniKreator> ModniKreatori { get; set; }

        internal protected virtual IList<ImenaVlasnika> Vlasnici { get; set; }

        internal ModnaKuca() { 
        
        ModniKreatori = new List<ModniKreator>();
          Vlasnici= new List<ImenaVlasnika>();
        }  
    }
}
