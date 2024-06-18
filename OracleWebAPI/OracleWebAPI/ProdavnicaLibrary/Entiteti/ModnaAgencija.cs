using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionWeekLibrary.Entiteti
{
    internal class ModnaAgencija
    {
        internal protected virtual  string PIB { get; set; }
        internal protected virtual string Naziv { get; set; }
        internal protected virtual DateTime DatumOsnivanja { get; set; }
        internal protected virtual string Drzava { get; set; }
        internal protected virtual string Grad { get; set; }

        internal protected virtual int PInternacionalna { get; set; }

        //  public virtual string Tip {  get; set; }
        internal protected virtual IList<NaziviZemalja> NaziviZemalja { get; set; }

        internal protected virtual IList<Maneken>? Manekeni { get; set; }

        internal ModnaAgencija()
        {

            Manekeni = new List<Maneken>();
            NaziviZemalja=new List<NaziviZemalja>();
        }
    }


 /*   public class InternacionalnaAgencija:ModnaAgencija
    {
        public IList<NaziviZemalja> NaziviZemalja { get; set; }

        public InternacionalnaAgencija()
        {
            NaziviZemalja=new List<NaziviZemalja>();
        }
    }

    public class DomacaAgencija:ModnaAgencija 
    { 
    
    
    
    }
 */
}
