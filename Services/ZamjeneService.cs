using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Services
{
    public class ZamjeneService
    {
        private ApplicationDbContext _dbcont = new ApplicationDbContext();

        public List<Zamjena> GetList()
        {
            _dbcont = new ApplicationDbContext();
            var list = _dbcont.Zamjene.Include(f => f.Nastavnik).ToList();
            return list;
        }

        public List<Zamjena> GetList(int razina, string oznaka, int godina)
        {
            _dbcont = new ApplicationDbContext();
            var list = _dbcont.Zamjene.Include(f => f.Nastavnik).Include(f => f.Predmet).Include(f => f.Predmet.Nastavnik).Include(f => f.Predmet.Razred).Include(f => f.Predmet.Razred.SkolskaGodina).Where(z => z.Predmet.Razred.Razina == razina && z.Predmet.Razred.Oznaka == oznaka && z.Predmet.Razred.SkolskaGodina.Godina == godina).ToList();
            return list;
        }

        public List<Zamjena> GetList(string oib, string naziv, int razina, string oznaka, int godina)
        {
            _dbcont = new ApplicationDbContext();
            var list = _dbcont.Zamjene.Include(f => f.Nastavnik).Include(f => f.Predmet).Include(f => f.Predmet.Razred).Include(f => f.Predmet.Razred.SkolskaGodina).Where(z => z.Nastavnik.OIB == oib && z.Predmet.Naziv == naziv && z.Predmet.Razred.Razina == razina && z.Predmet.Razred.Oznaka == oznaka && z.Predmet.Razred.SkolskaGodina.Godina == godina).ToList();
            return list;
        }

        public void Add(string oib, DateTime datum_od, DateTime datum_do, string naziv, int razina, string oznaka, int godina)
        {
            var n =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oib);
            var p =  _dbcont.Predmeti.Include(f => f.Nastavnik).Include(f => f.Razred).FirstOrDefault(S => S.Naziv == naziv && S.Razred.Razina == razina && S.Razred.Oznaka == oznaka && S.Razred.SkolskaGodina.Godina == godina);
            var s =  _dbcont.Zamjene.FirstOrDefault(S => S.NastavnikId == n.Id && S.PredmetId == p.Id);
            if (s != null)
                return;
            _dbcont.Zamjene.Add(new Zamjena() { DatumOd = datum_od, DatumDo = datum_do, NastavnikId = n.Id, PredmetId = p.Id});
             _dbcont.SaveChanges();
        }

        public void Delete(string naziv, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Zamjene.Include(f => f.Nastavnik).Include(f => f.Predmet).Include(f => f.Predmet.Razred).Include(f => f.Predmet.Razred.SkolskaGodina).FirstOrDefault(S => S.Predmet.Naziv == naziv && S.Nastavnik.OIB == oib && S.Predmet.Razred.Razina == razina && S.Predmet.Razred.Oznaka == oznaka && S.Predmet.Razred.SkolskaGodina.Godina == godina);
            if (s is null)
                return;
            _dbcont.Zamjene.Remove(s);
             _dbcont.SaveChanges();
        }

        public void Update(DateTime datum_od, DateTime datum_do, string naziv, string nazivPrije, string oib, string oibPrije, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Zamjene.Include(f => f.Nastavnik).Include(f => f.Predmet).Include(f => f.Predmet.Razred).Include(f => f.Predmet.Razred.SkolskaGodina).FirstOrDefault(S => S.Predmet.Naziv == nazivPrije && S.Nastavnik.OIB == oibPrije && S.Predmet.Razred.Razina == razina && S.Predmet.Razred.Oznaka == oznaka && S.Predmet.Razred.SkolskaGodina.Godina == godina);
            if (s is null)
                return;
            s.DatumOd = datum_od;
            s.DatumDo = datum_do;
            s.Predmet = _dbcont.Predmeti.FirstOrDefault(p => p.Naziv == naziv);
            s.Nastavnik = _dbcont.Nastavnici.FirstOrDefault(n => n.OIB == oib);
             _dbcont.SaveChanges();
        }

        public Zamjena GetSingle(string naziv, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Zamjene.Include(f => f.Nastavnik).Include(f => f.Predmet).Include(f => f.Predmet.Razred).Include(f => f.Predmet.Razred.SkolskaGodina).FirstOrDefault(S => S.Predmet.Naziv == naziv && S.Nastavnik.OIB == oib && S.Predmet.Razred.Razina == razina && S.Predmet.Razred.Oznaka == oznaka && S.Predmet.Razred.SkolskaGodina.Godina == godina);

            return s;
        }
    }
}
