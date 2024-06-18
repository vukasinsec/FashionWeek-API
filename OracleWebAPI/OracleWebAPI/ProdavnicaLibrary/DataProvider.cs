using FashionWeekLibrary.Entiteti;
using NHibernate.Linq;
using System.Runtime.InteropServices;

namespace FashionWeekLibrary;

public static class DataProvider
{

    public async static Task<Result<string, ErrorMessage>> SacuvajModnogKreatora(ModniKreatorView m, int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator o = await s.Query<Organizator>()
                                     .FirstOrDefaultAsync(mk => mk.OrganizatorID == id);

            ModniKreator mk = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                CenaUsluge = m.CenaUsluge,
                OrganizatorID = o


            };

            await s.SaveAsync(mk);
            await s.FlushAsync();
            m.MBR = mk.MBR;
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati modnu kucu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.MBR;
    }


    #region ModnaRevija
    public static Result<List<ModnaRevijaView>, ErrorMessage> VratiSveModneRevije()
    {
        ISession? s = null;

        List<ModnaRevijaView> revije = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<ModnaRevija> sveRevije = from o in s.Query<ModnaRevija>()
                                                    select o;

            foreach (ModnaRevija m in sveRevije)
            {
                revije.Add(new ModnaRevijaView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve modne revije.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return revije;
    }

    public async static Task<Result<string, ErrorMessage>> SacuvajModnuReviju(ModnaRevijaView m, int idOrg)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator o = await s.LoadAsync<Organizator>(idOrg);

            ModnaRevija mr = new()
            {
                IdModneRevije = m.IdModneRevije,
                RedniBroj = m.RedniBroj,
                Naziv = m.Naziv,
                DatumOdrzavanja = m.DatumOdrzavanja,
                VremeOdrzavanja = m.VremeOdrzavanja,
                ImeJavneLicnosti = m.ImeJavneLicnosti,
                PrezimeJavneLicnosti = m.PrezimeJavneLicnosti,
                ZanimanjeJL = m.ZanimanjeJL,
                OrganizatorID=o
            };
            m.IdModneRevije = mr.IdModneRevije;
            await s.SaveAsync(mr);
            await s.FlushAsync();
           
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati reviju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.Naziv;
    }




    public async static Task<Result<bool, ErrorMessage>> DodajModnuRevijuAsync(ModnaRevijaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaRevija mr = new()
            {
                IdModneRevije = m.IdModneRevije,
                RedniBroj = m.RedniBroj,
                Naziv=m.Naziv,  
                DatumOdrzavanja=m.DatumOdrzavanja,
                VremeOdrzavanja = m.VremeOdrzavanja,
                ImeJavneLicnosti=m.ImeJavneLicnosti,
                PrezimeJavneLicnosti = m.PrezimeJavneLicnosti,
                ZanimanjeJL=m.ZanimanjeJL
            };

            await s.SaveOrUpdateAsync(mr);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati modnu reviju.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<int, ErrorMessage>> SacuvajModnuRevijuBezOrganizatora(ModnaRevijaView m)
    {
        ISession? s = null;
        int id = default;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaRevija mr = new()
            {
                IdModneRevije = m.IdModneRevije,
                RedniBroj = m.RedniBroj,
                Naziv = m.Naziv,
                DatumOdrzavanja = m.DatumOdrzavanja,
                VremeOdrzavanja = m.VremeOdrzavanja,
                ImeJavneLicnosti = m.ImeJavneLicnosti,
                PrezimeJavneLicnosti = m.PrezimeJavneLicnosti,
                ZanimanjeJL = m.ZanimanjeJL
            };

            await s.SaveAsync(mr);
            await s.FlushAsync();
            id = mr.IdModneRevije;
           
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati reviju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public async static Task<Result<bool, ErrorMessage>> PoveziRevijuIOrganizatora(int modnarevijaID, int orgID)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaRevija mr = await s.LoadAsync<ModnaRevija>(modnarevijaID);
            Organizator o = await s.LoadAsync<Organizator>(orgID);

            mr.OrganizatorID = o;

            await s.UpdateAsync(mr);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće povezati reviju sa org.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }


    public async static Task<Result<ModnaRevijaView, ErrorMessage>> AzurirajModnuRevijuAsync(ModnaRevijaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaRevija mr = s.Load<ModnaRevija>(m.IdModneRevije);
            mr.IdModneRevije = m.IdModneRevije;
            mr.RedniBroj = m.RedniBroj;
            mr.Naziv = m.Naziv;
            mr.DatumOdrzavanja = m.DatumOdrzavanja;
            mr.VremeOdrzavanja = m.VremeOdrzavanja;
            mr.ImeJavneLicnosti = m.ImeJavneLicnosti;
            mr.PrezimeJavneLicnosti = m.PrezimeJavneLicnosti;

            await s.UpdateAsync(mr);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati prodavnicu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }

    public async static Task<Result<bool, ErrorMessage>> ObrisiModnuRevijuAsync(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            //ModnaRevija o = await s.LoadAsync<ModnaRevija>(id);
            ModnaRevija modnaKuca = await s.Query<ModnaRevija>()
                                    .FirstOrDefaultAsync(mk => mk.IdModneRevije == id);

            await s.DeleteAsync(modnaKuca);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati reviju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<ModnaRevijaView, ErrorMessage>> VratiModnuRevijuAsync(int id)
    {
        ISession? s = null;

        ModnaRevijaView revijaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaRevija mr = await s.LoadAsync<ModnaRevija>(id);
            revijaView = new ModnaRevijaView(mr);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti modnu reviju sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return revijaView;
    }

    public static Result<List<ModniKreatorView>, ErrorMessage> SpecijalniGostiNaModnojReviji(int id)
    {
        ISession? s = null;

        List<ModniKreatorView> gosti = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<SpecijalniGost> sviGosti = from o in s.Query<SpecijalniGost>()
                                               where o.IDModneRevije != null && o.IDModneRevije.IdModneRevije == id
                                               select o;

            foreach (SpecijalniGost r in sviGosti)
            {
                gosti.Add(new ModniKreatorView(r.MBRModniKreator));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve specijalne goste koji nastupaju na modnoj reviji sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return gosti;
    }

    public static Result<List<ModnaRevijaView>, ErrorMessage> VratiRevijeManekena(string mbr)
    {
        ISession? s = null;

        List<ModnaRevijaView> revije = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Nastupa> sveRevije = from o in s.Query<Nastupa>()
                                               where o.Id != null && o.Id.ManekenNaReviji != null && o.Id.ManekenNaReviji.MBR == mbr
                                               select o;

            foreach (Nastupa r in sveRevije)
            {
                revije.Add(new ModnaRevijaView(r.Id?.NaReviji));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve revije na kojima nastupa maneken.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return revije;
    }

   

    #endregion

    #region ModnaAgencija

    public static Result<List<ModnaAgencijaView>, ErrorMessage> VratiSveModneAgencije()
    {
        ISession? s = null;

        List<ModnaAgencijaView> agencije = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<ModnaAgencija> sveAgencije = from o in s.Query<ModnaAgencija>()
                                                 select o;

            foreach (ModnaAgencija m in sveAgencije)
            {
                agencije.Add(new ModnaAgencijaView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve modne revije.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return agencije;
    }


    public async static Task<Result<bool, ErrorMessage>> DodajModnuAgencijuAsync(ModnaAgencijaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija ma = new()
            {
              PIB = m.PIB,
            Naziv = m.Naziv,
            DatumOsnivanja = m.DatumOsnivanja,
            Drzava = m.Drzava,
            Grad = m.Grad,
            PInternacionalna = m.PInternacionalna
        };

            await s.SaveOrUpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati modnu agenciju.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<ModnaAgencijaView, ErrorMessage>> AzurirajModnuAgenciju(ModnaAgencijaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija ma = s.Load<ModnaAgencija>(m.PIB);
            ma.PIB = m.PIB;
            ma.Naziv = m.Naziv;
            ma.DatumOsnivanja = m.DatumOsnivanja;
            ma.Drzava = m.Drzava;
            ma.Grad = m.Grad;
            ma.PInternacionalna = m.PInternacionalna;



            await s.UpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati agenciju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }

    public async static Task<Result<bool, ErrorMessage>> ObrisiModnuAgencijuAsync(string pib)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija o = await s.LoadAsync<ModnaAgencija>(pib);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati agenciju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }


    public async static Task<Result<ModnaAgencijaView, ErrorMessage>> VratiModnuAgenciju(string pib)
    {
        ISession? s = null;

        ModnaAgencijaView agencijaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija mr = await s.LoadAsync<ModnaAgencija>(pib);
            agencijaView = new ModnaAgencijaView(mr);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti modnu agenciju sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return agencijaView;
    }

    public  async static Task<Result<List<string>, ErrorMessage>> VratiNaziveZemalja(string pib)
    {
        ISession? s = null;
        List<string> zemlje = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija mr =  await s.LoadAsync<ModnaAgencija>(pib);
            ModnaAgencijaView agencijaView = new ModnaAgencijaView(mr);

            if(mr.PInternacionalna==0)
            {
                return "Modna Agencija nije internacionalna".ToError(400);
            }
            IEnumerable<NaziviZemalja> sveZemlje = from o in s.Query<NaziviZemalja>()
                                                       where o.Id != null && o.Id.Agencija != null && o.Id.Agencija.PIB == pib
                                                       select o;
          foreach (NaziviZemalja r in sveZemlje)
          {
                    zemlje.Add(r.Id.Zemlje);
           }
           
        }
        catch (Exception)
        {
            return "Nemoguće vratiti modnu agenciju sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return zemlje;
    }

    public static async Task<Result<bool, ErrorMessage>> DodajZemljeAgencije(string pib, string nazivZemlje)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija ag = await s.Query<ModnaAgencija>().FirstOrDefaultAsync(ag => ag.PIB == pib);
            if (ag == null)
            {
                return "agencija nije pronađena.".ToError(400);
            }

            NaziviZemalja novaZemlja = new NaziviZemalja
            {
                Id = new NaziviZemaljaId
                {
                     Agencija= ag,
                    Zemlje = nazivZemlje
                }
            };

            await s.SaveAsync(novaZemlja);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće dodati zemlju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static async Task<Result<bool, ErrorMessage>> ObrisiZemljuAgencije(string pib, string nazivZemlje)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            NaziviZemalja nz = await s.Query<NaziviZemalja>()
                                 .Where(c => c.Id.Agencija.PIB == pib && c.Id.Zemlje == nazivZemlje)
                                 .FirstOrDefaultAsync();

            if (nz == null)
            {
                return "Zemlja nije pronađen.".ToError(400);
            }

            await s.DeleteAsync(nz);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati zemlju.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    #endregion

    #region Maneken

    public static Result<List<ManekenView>, ErrorMessage> VratiSveManekene()
    {
        ISession? s = null;

        List<ManekenView> manekeni = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Maneken> sviManekeni = from o in s.Query<Maneken>()
                                                     select o;

           

            foreach (Maneken m in sviManekeni)
            {
                manekeni.Add(new ManekenView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve manekene.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return manekeni;
    }


    public async static Task<Result<string, ErrorMessage>> SacuvajManekena(ManekenView m)
    {
        ISession? s = null;

        string id = default;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken mm = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                BojaKose = m.BojaKose,
                BojaOciju = m.BojaOciju,
            Visina = m.Visina,
                Tezina = m.Tezina,
                KonfekcijskiBroj = m.KonfekcijskiBroj
            };

            id = (string)await s.SaveAsync(mm);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati manekena.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return id;
    }

    public async static Task<Result<string, ErrorMessage>> SacuvajManekena(ManekenView m, string pib)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija p = await s.LoadAsync<ModnaAgencija>(pib);

            Maneken o = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                BojaKose = m.BojaKose,
                BojaOciju = m.BojaOciju,
                Visina = m.Visina,
                Tezina = m.Tezina,
                KonfekcijskiBroj = m.KonfekcijskiBroj,
                PIBModneAgencije=p
                
            };

            await s.SaveAsync(o);
            await s.FlushAsync();
            m.MBR = o.MBR;
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati manekena.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.MBR;
    }

    public async static Task<Result<ManekenView, ErrorMessage>> VratiManekena(string mbr)
    {
        ISession? s = null;

        ManekenView manekenView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken sl = await s.LoadAsync<Maneken>(mbr);
            manekenView = new ManekenView(sl);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti manekena sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return manekenView;
    }

   /* public async static Task<Result<ModnaAgencijaView, ErrorMessage>> VratiModnuAgenciju(string pib)
    {
        ISession? s = null;

        ModnaAgencijaView modnaAgencijaView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaAgencija o = await s.LoadAsync<ModnaAgencija>(pib);
            modnaAgencijaView = new ModnaAgencijaView(o);

            s.Close();
        }
        catch (Exception)
        {
            return "Nemoguće vratiti modnu agenciju sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return modnaAgencijaView;
    }
   */

    public async static Task<Result<bool, ErrorMessage>> DodajManekena(ManekenView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }
          //  ModnaAgencija p = await s.LoadAsync<ModnaAgencija>(m.PIBModneAgencije.PIB);
            Maneken ma = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
            DatumRodjenja = m.DatumRodjenja,
            ZemljaPorekla = m.ZemljaPorekla,
            Ime = m.Ime,
            Prezime = m.Prezime,
                BojaKose = m.BojaKose,
                BojaOciju = m.BojaOciju,
            Visina = m.Visina,
            Tezina = m.Tezina,
            KonfekcijskiBroj = m.KonfekcijskiBroj,
           //PIBModneAgencije=p
            
        };

            await s.SaveOrUpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati manekena.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<ManekenView, ErrorMessage>> AzurirajManekena(ManekenView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken ma = s.Load<Maneken>(m.MBR);
            ma.MBR = m.MBR;
            ma.Pol = m.Pol;
            ma.DatumRodjenja = m.DatumRodjenja;
            ma.ZemljaPorekla = m.ZemljaPorekla;
            ma.Ime = m.Ime;
            ma.Prezime = m.Prezime;
            ma.BojaKose = m.BojaKose;
            ma.BojaOciju = m.BojaOciju;
            ma.Visina = m.Visina;
            ma.Tezina = m.Tezina;
            ma.KonfekcijskiBroj = m.KonfekcijskiBroj;



            await s.UpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati manekena.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }


    public async static Task<Result<bool, ErrorMessage>> ObrisiManekena(string mbr)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken o = await s.LoadAsync<Maneken>(mbr);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati manekena.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
        return true;
    }


    public static Result<List<ManekenView>, ErrorMessage> VratiSveManekeneModneRevije(int id)
    {
        ISession? s = null;

        List<ManekenView> manekeni = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Nastupa> sviManekeni = from o in s.Query<Nastupa>()
                                            where o.Id != null && o.Id.NaReviji != null && o.Id.NaReviji.IdModneRevije == id
                                            select o;

            foreach (Nastupa r in sviManekeni)
            {
                manekeni.Add(new ManekenView(r.Id?.ManekenNaReviji));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve manekene koji nastupaju na modnoj reviji sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return manekeni;
    }

    public static async Task<Result<List<ManekenView>, ErrorMessage>> VratiSveManekeneModneAgencije(string pib)
    {
        List<ManekenView> data = new();

        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            data = (await s.QueryOver<Maneken>().ListAsync())
                           .Where(p => p.PIBModneAgencije?.PIB == pib)
                           .Select(p => new ManekenView(p)).ToList();
        }
        catch (Exception)
        {
            return "Došlo je do greške prilikom prikupljanja informacija o manekenima.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return data;
    }

    public async static Task<Result<bool, ErrorMessage>> PoveziManekenaiModnuAgenciju(string mbr , string pib)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken m = await s.LoadAsync<Maneken>(mbr);
            ModnaAgencija a = await s.LoadAsync<ModnaAgencija>(pib);

            m.PIBModneAgencije = a;

            await s.UpdateAsync(m);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće povezati manekena sa modnom agencijom.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<bool, ErrorMessage>> DodajNastup(NastupaView nastupa)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Nastupa r = new()
            {
                Id = new NastupaId
                {
                    NaReviji = await s.LoadAsync<ModnaRevija>(nastupa.Id?.NaReviji?.IdModneRevije),
                    ManekenNaReviji = await s.LoadAsync<Maneken>(nastupa.Id?.ManekenNaReviji?.MBR)
                },
               
            };

            await s.SaveOrUpdateAsync(r);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće dodati nastup.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static Result<List<string>, ErrorMessage> VratiCasopiseManekena(string mbr)
    {
        ISession? s = null;

        List<string> casopisi = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Casopisi> sviCasopisi = from o in s.Query<Casopisi>()
                                               where o.Id != null && o.Id.Maneken != null && o.Id.Maneken.MBR == mbr
                                               select o;
            foreach (Casopisi r in sviCasopisi)
            {
                casopisi.Add(r.Id.NaziviCasopisa);
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve manekene koji nastupaju na modnoj reviji sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return casopisi;
    }

    public static async Task<Result<bool, ErrorMessage>> DodajCasopisManekenu(string mbr, string nazivCasopisa)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Maneken maneken = await s.LoadAsync<Maneken>(mbr);
            if (maneken == null)
            {
                return "Maneken nije pronađen.".ToError(400);
            }

            Casopisi noviCasopis = new Casopisi
            {
                Id = new CasopisiId
                {
                    Maneken = maneken,
                    NaziviCasopisa = nazivCasopisa
                }
            };

            await s.SaveAsync(noviCasopis);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće dodati časopis manekenu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static async Task<Result<bool, ErrorMessage>> ObrisiCasopisManekena(string mbr, string nazivCasopisa)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Casopisi casopis = await s.Query<Casopisi>()
                                 .Where(c => c.Id.Maneken.MBR == mbr && c.Id.NaziviCasopisa == nazivCasopisa)
                                 .FirstOrDefaultAsync();

            if (casopis == null)
            {
                return "Časopis nije pronađen.".ToError(400);
            }

            await s.DeleteAsync(casopis);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati časopis manekena.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static async Task<Result<bool, ErrorMessage>> PoveziManekenaSaAgencijom(ManekenView maneken, ModnaAgencijaView agencija)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            maneken.PIBModneAgencije = agencija;

            await s.SaveOrUpdateAsync(maneken);
            await s.FlushAsync();
        }
        catch (Exception ex)
        {
            return $"Nemoguće povezati manekena sa agencijom. Greška: {ex.Message}".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    



    #endregion

    #region ModniKreator

    public static Result<List<ModniKreatorView>, ErrorMessage> VratiSveKreatore()
    {
        ISession? s = null;

        List<ModniKreatorView> kreatori = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<ModniKreator> sviKreatori = from o in s.Query<ModniKreator>()
                                               select o;

            foreach (ModniKreator m in sviKreatori)
            {
                kreatori.Add(new ModniKreatorView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve kreatore.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kreatori;
    }

    public async static Task<Result<bool, ErrorMessage>> DodajKreatora(ModniKreatorView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModniKreator mk = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                CenaUsluge = m.CenaUsluge,
                
            };

            await s.SaveOrUpdateAsync(mk);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati kreatora.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<ModniKreatorView, ErrorMessage>> AzurirajKreatora(ModniKreatorView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModniKreator ma = s.Load<ModniKreator>(m.MBR);
            ma.MBR = m.MBR;
            ma.Pol = m.Pol;
            ma.DatumRodjenja = m.DatumRodjenja;
            ma.ZemljaPorekla = m.ZemljaPorekla;
            ma.Ime = m.Ime;
            ma.Prezime = m.Prezime;
           ma.CenaUsluge=m.CenaUsluge;



            await s.UpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati kreatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }

    public async static Task<Result<bool, ErrorMessage>> ObrisiKreatora(string mbr)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModniKreator o = await s.LoadAsync<ModniKreator>(mbr);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati kreatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static Result<List<ModniKreatorView>, ErrorMessage> VratiSveModneKreatoreRevije(int id)
    {
        ISession? s = null;

        List<ModniKreatorView> kreatori = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Predstavlja> sviKreatori = from o in s.Query<Predstavlja>()
                                               where o.Id != null && o.Id.NaModnojReviji != null && o.Id.NaModnojReviji.IdModneRevije == id
                                               select o;

            foreach (Predstavlja r in sviKreatori)
            {
                kreatori.Add(new ModniKreatorView(r.Id?.MKPredstavlja));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve kreatore koji predstavljaju na modnoj reviji sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kreatori;
    }

    public static async Task<Result<List<ModniKreatorView>, ErrorMessage>> VratiSveModneKreatoreModneKuce(string naziv)
    {
        List<ModniKreatorView> data = new();

        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            data = (await s.QueryOver<ModniKreator>().ListAsync())
                           .Where(p => p.NazivModneKuce?.Naziv == naziv)
                           .Select(p => new ModniKreatorView(p)).ToList();
        }
        catch (Exception)
        {
            return "Došlo je do greške prilikom prikupljanja informacija o modnim kreatorima.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return data;
    }

    public async static Task<Result<ModniKreatorView, ErrorMessage>> VratiModnogKreatora(string mbr)
    {
        ISession? s = null;

        ModniKreatorView kreatorView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModniKreator sl = await s.LoadAsync<ModniKreator>(mbr);
            kreatorView = new ModniKreatorView(sl);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti kreatora sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kreatorView;
    }

    public async static Task<Result<bool, ErrorMessage>> DodajPredstavljanje(PredstavljaView predstavlja)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Predstavlja r = new()
            {
                Id = new PredstavljaId
                {
                    NaModnojReviji = await s.LoadAsync<ModnaRevija>(predstavlja.Id?.NaModnojReviji?.IdModneRevije),
                    MKPredstavlja = await s.LoadAsync<ModniKreator>(predstavlja.Id?.MKPredstavlja?.MBR)
                },

            };

            await s.SaveOrUpdateAsync(r);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće dodati predstavljanje.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public async static Task<Result<string, ErrorMessage>> SacuvajModnogKreatoraOrganizatora(ModniKreatorView m, int id,string naziv)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator o = await s.LoadAsync<Organizator>(id);
            ModnaKuca mm = await s.LoadAsync<ModnaKuca>(naziv);

            ModniKreator mk = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                CenaUsluge = m.CenaUsluge,
                OrganizatorID=o,
                NazivModneKuce=mm

            };

            await s.SaveAsync(mk);
            await s.FlushAsync();
            m.Ime = mk.Ime;
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati modnog kreatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.MBR;
    }

    public async static Task<Result<string, ErrorMessage>> SacuvajModnogKreatora(ModniKreatorView m, string naziv)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }


            //ModnaKuca mm = await s.LoadAsync<ModnaKuca>(naziv);
            ModnaKuca kuca = await s.Query<ModnaKuca>()
                                     .FirstOrDefaultAsync(mk => mk.Naziv == naziv);

            ModniKreator mk = new()
            {
                MBR = m.MBR,
                Pol = m.Pol,
                DatumRodjenja = m.DatumRodjenja,
                ZemljaPorekla = m.ZemljaPorekla,
                Ime = m.Ime,
                Prezime = m.Prezime,
                CenaUsluge = m.CenaUsluge,
               
                NazivModneKuce = kuca

            };

            await s.SaveAsync(mk);
            await s.FlushAsync();
            //m.Ime = mk.Ime;
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati modnog kreatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.MBR;
    }



    #endregion

    #region ModnaKuca

    public static Result<List<ModnaKucaView>, ErrorMessage> VratiSveModneKuce()
    {
        ISession? s = null;

        List<ModnaKucaView> kuce = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<ModnaKuca> sveKuce = from o in s.Query<ModnaKuca>()
                                                     select o;

            foreach (ModnaKuca m in sveKuce)
            {
                kuce.Add(new ModnaKucaView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve modne kuce.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kuce;
    }

    public async static Task<Result<bool, ErrorMessage>> DodajModnuKucu(ModnaKucaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaKuca mk = new()
            {
                Naziv = m.Naziv,
                ImeOsnivaca = m.ImeOsnivaca,
                PrezimeOsnivaca = m.PrezimeOsnivaca,
                Drzava = m.Drzava,
                Grad = m.Grad,

            };

            await s.SaveOrUpdateAsync(mk);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati modnu kucu.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }


    public async static Task<Result<ModnaKucaView, ErrorMessage>> AzurirajModnuKucu(ModnaKucaView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModnaKuca ma = s.Load<ModnaKuca>(m.Naziv);
            ma.Naziv = m.Naziv;
            ma.ImeOsnivaca = m.ImeOsnivaca;
            ma.PrezimeOsnivaca = m.PrezimeOsnivaca;
            ma.Drzava = m.Drzava;
            ma.Grad = m.Grad;



            await s.UpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati kucu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }

    public async static Task<Result<bool, ErrorMessage>> ObrisiModnuKucu(string n)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            //ModnaKuca o = await s.LoadAsync<ModnaKuca>(n);
            ModnaKuca modnaKuca = await s.Query<ModnaKuca>()
                                     .FirstOrDefaultAsync(mk => mk.Naziv == n);

            await s.DeleteAsync(modnaKuca);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati modnu kucu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }


    public static Result<List<string>, ErrorMessage> VratiImenaVlasnikaModneKuce(string n)
    {
        ISession? s = null;

        List<string> vlasnici = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<ImenaVlasnika> sviVlasnici = from o in s.Query<ImenaVlasnika>()
                                                where o.Id != null && o.Id.ModnaKuca != null && o.Id.ModnaKuca.Naziv == n
                                                select o;
            foreach (ImenaVlasnika r in sviVlasnici)
            {
                vlasnici.Add(r.Id.ImenaVlasnika);
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve vlasnike modne kuce.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return vlasnici;
    }

    
    public async static Task<Result<string, ErrorMessage>> SacuvajModnuKucu(ModnaKucaView m, int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator o = await s.Query<Organizator>()
                                     .FirstOrDefaultAsync(mk => mk.OrganizatorID == id);

            ModnaKuca mk = new()
            {
                Naziv=m.Naziv,
                ImeOsnivaca=m.ImeOsnivaca,
                PrezimeOsnivaca=m.PrezimeOsnivaca,
                Drzava=m.Drzava,
                Grad=m.Grad,
                OrganizatorID=o

            };

            await s.SaveAsync(mk);
            await s.FlushAsync();
            m.Naziv = mk.Naziv;
        }
        catch (Exception)
        {
            return "Nemoguće sačuvati modnu kucu.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m.Naziv;
    }


    #endregion

    #region Organizator
    public static Result<List<OrganizatorView>, ErrorMessage> VratiSveOrganizatore()
    {
        ISession? s = null;

        List<OrganizatorView> organizatori = new();

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            IEnumerable<Organizator> sviOrganizatori = from o in s.Query<Organizator>()
                                             select o;

            foreach (Organizator m in sviOrganizatori)
            {
                organizatori.Add(new OrganizatorView(m));
            }
        }
        catch (Exception)
        {
            return "Nemoguće vratiti sve organizatore.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return organizatori;
    }

    public async static Task<Result<bool, ErrorMessage>> DodajOrganizatora(OrganizatorView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator mk = new()
            {
                Kolekcija=m.Kolekcija,
                PrvaModnaRevija=m.PrvaModnaRevija
            };

            await s.SaveOrUpdateAsync(mk);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return GetError("Nemoguće dodati organizatora.", 404);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }


    public async static Task<Result<OrganizatorView, ErrorMessage>> AzurirajOrganizatora(OrganizatorView m)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator ma = s.Load<Organizator>(m.OrganizatorID);
            ma.Kolekcija = m.Kolekcija;
            ma.PrvaModnaRevija = m.PrvaModnaRevija;
          



            await s.UpdateAsync(ma);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće ažurirati organizatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return m;
    }

    public async static Task<Result<bool, ErrorMessage>> ObrisiOrganizatora(int id)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            Organizator o = await s.LoadAsync<Organizator>(id);

            await s.DeleteAsync(o);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati organizatora.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static async Task<Result<List<ModnaRevijaView>, ErrorMessage>> VratiRevijeOrganizatora(int id)
    {
        List<ModnaRevijaView> data = new();

        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            data = (await s.QueryOver<ModnaRevija>().ListAsync())
                           .Where(p => p.OrganizatorID?.OrganizatorID == id)
                           .Select(p => new ModnaRevijaView(p)).ToList();
        }
        catch (Exception)
        {
            return "Došlo je do greške prilikom prikupljanja informacija o revijama.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return data;
    }

    #endregion

    #region SpecijalniGost

    public async static Task<Result<SpecijalniGostView, ErrorMessage>> VratiSpecijalnogGosta(int id)
    {
        ISession? s = null;

        SpecijalniGostView specView = default!;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            SpecijalniGost sl = await s.LoadAsync<SpecijalniGost>(id);
            specView = new SpecijalniGostView(sl);
        }
        catch (Exception)
        {
            return "Nemoguće vratiti gosta sa zadatim ID-jem.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return specView;
    }
    public static async Task<Result<bool, ErrorMessage>> ObrisiSpecijalnogGosta(int idrevije, string mbrKreatora)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            SpecijalniGost gost = await s.Query<SpecijalniGost>()
                                 .Where(c => c.IDModneRevije.IdModneRevije == idrevije && c.MBRModniKreator.MBR == mbrKreatora)
                                 .FirstOrDefaultAsync();

            if (gost == null)
            {
                return "Gost nije pronađen.".ToError(400);
            }

            await s.DeleteAsync(gost);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće obrisati gosta revije.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }

    public static async Task<Result<bool, ErrorMessage>> AzurirajSpecijalnogGosta(SpecijalniGostView specijalniGost)
    {
        ISession? session = null;

        try
        {
            session = DataLayer.GetSession();

            if (!(session?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            await session.UpdateAsync(specijalniGost);
            await session.FlushAsync();

            return true;
        }
        catch (Exception ex)
        {
            return "Greška prilikom ažuriranja specijalnog gosta: {ex.Message}".ToError(403);
        }
        finally
        {
            session?.Close();
            session?.Dispose();
        }
    }

    public static async Task<Result<bool, ErrorMessage>> DodajGosta(int idRevije, string mbr)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();

            if (!(s?.IsConnected ?? false))
            {
                return "Nemoguće otvoriti sesiju.".ToError(403);
            }

            ModniKreator mk = await s.LoadAsync<ModniKreator>(mbr);
            ModnaRevija mr = await s.LoadAsync<ModnaRevija>(idRevije);
            if (mk == null)
            {
                return "Kreator nije pronađen.".ToError(400);
            }

            SpecijalniGost noviGost = new SpecijalniGost
            {

                MBRModniKreator = mk,
                IDModneRevije = mr

            };

            await s.SaveAsync(noviGost);
            await s.FlushAsync();
        }
        catch (Exception)
        {
            return "Nemoguće dodati gosta reviji.".ToError(400);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return true;
    }



    #endregion
}
