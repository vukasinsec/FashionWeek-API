using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FashionWeekLibrary.Mapiranja
{
     internal class ManekenMapiranja: SubclassMap<FashionWeekLibrary.Entiteti.Maneken>//moglo je i u using!!!, da li treba subclass??
    {
        public ManekenMapiranja()
        {
            // Naziv tabele u bazi podataka
            Table("MANEKEN");
            Abstract();

            //  DiscriminatorValue("Maneken");
            // Primarni ključ koji se generiše korišćenjem trigger-a
            // Id(p => p.MBR, "MBR").GeneratedBy.TriggerIdentity();//assigned za podatak koji neko unosi

            KeyColumn("MBR");
         //   DiscriminatorValue("MANEKEN");
            // Sva ostala svojstva
            Map(p => p.BojaKose, "BOJAKOSE");
            Map(p => p.Visina, "VISINA");
            Map(p => p.BojaOciju, "BOJAOCIJU");
            Map(p => p.Tezina, "TEZINA");
            Map(p => p.KonfekcijskiBroj, "KONFEKCIJSKIBROJ");

            References(x => x.PIBModneAgencije).Column("PIBMODNEAGENCIJE").LazyLoad();

            HasManyToMany(x => x.ModneRevijeManekeni)
                .Table("NASTUPA")
                .ParentKeyColumn("MANEKENMBR")
                .ChildKeyColumn("MODNAREVIJA")
                .Cascade.All();

            //  HasMany(x => x.NastupaManeken).KeyColumn("MANEKENMBR").LazyLoad().Cascade.All().Inverse();//???

            //HasMany(x => x.UcasopisimaManeken).KeyColumn("MANEKEN").LazyLoad().Cascade.All().Inverse();

            HasMany(x => x.UcasopisimaManeken).KeyColumn("MANEKEN").LazyLoad().Cascade.All().Inverse() ;
        }
    }
}
