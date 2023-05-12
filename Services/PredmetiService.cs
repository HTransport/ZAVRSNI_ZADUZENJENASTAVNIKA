using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Services
{
    public class PredmetiService
    {
        private ApplicationDbContext _dbcont = new ApplicationDbContext();

        public List<Predmet> GetList()
        {
            _dbcont = new ApplicationDbContext();
            var list =  _dbcont.Predmeti.Include(f => f.Razred).Include(f => f.Razred.SkolskaGodina).Include(f => f.Nastavnik).ToList();
            return list;
        }

        public List<Predmet> GetList(int razina, string oznaka, int godina)
        {
            _dbcont = new ApplicationDbContext();
            var list = _dbcont.Predmeti.Include(f => f.Razred).Include(f => f.Razred.SkolskaGodina).Include(f => f.Nastavnik).Where(p => p.Razred.Razina == razina && p.Razred.Oznaka == oznaka && p.Razred.SkolskaGodina.Godina == godina).ToList();
            return list;
        }

        public void Add(string naziv, string kategorija, string oib, int razina, string oznaka, int godina)
        {
            var r =  _dbcont.Razredi.Include(f => f.SkolskaGodina).FirstOrDefault(S => S.SkolskaGodina.Godina == godina && S.Razina == razina && S.Oznaka == oznaka);
            var n =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oib);
            var s =  _dbcont.Predmeti.FirstOrDefault(S => S.Naziv == naziv && S.NastavnikId == n.Id && S.RazredId == r.Id);
            if (s != null)
                return;
            _dbcont.Predmeti.Add(new Predmet() { Naziv = naziv, Kategorija = kategorija, NastavnikId = n.Id, RazredId = r.Id });
             _dbcont.SaveChanges();
        }

        public void Delete(string naziv, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Predmeti.Include(f => f.Razred).Include(f => f.Nastavnik).FirstOrDefault(S => S.Naziv == naziv && S.Nastavnik.OIB == oib && S.Razred.Razina == razina && S.Razred.Oznaka == oznaka && S.Razred.SkolskaGodina.Godina == godina);
            if (s is null)
                return;
            _dbcont.Predmeti.Remove(s);
             _dbcont.SaveChanges();
        }

        public void Update(string nazivPrije, string naziv, string kategorija, string oibPrije, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Predmeti.Include(f => f.Nastavnik).Include(f => f.Razred).FirstOrDefault(S => S.Naziv == nazivPrije && S.Nastavnik.OIB == oibPrije && S.Razred.Razina == razina && S.Razred.Oznaka == oznaka && S.Razred.SkolskaGodina.Godina == godina);
            if (s is null)
                return;
            s.Naziv = naziv;
            s.Kategorija = kategorija;
            var n = _dbcont.Nastavnici.FirstOrDefault(N => N.OIB == oib);
            s.Nastavnik = n;
            
             _dbcont.SaveChanges();
        }

        public Predmet GetSingle(string naziv, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Predmeti.Include(f => f.Nastavnik).Include(f => f.Razred).FirstOrDefault(S => S.Naziv == naziv && S.Nastavnik.OIB == oib && S.Razred.Razina == razina && S.Razred.Oznaka == oznaka && S.Razred.SkolskaGodina.Godina == godina);

            return s;
        }

        public Predmet GetSingle(string naziv, string kategorija, string oib, int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Predmeti.Include(f => f.Nastavnik).Include(f => f.Razred).FirstOrDefault(S => S.Naziv == naziv && S.Kategorija == kategorija && S.Nastavnik.OIB == oib && S.Razred.Razina == razina && S.Razred.Oznaka == oznaka && S.Razred.SkolskaGodina.Godina == godina);
            
            return s;
        }
    }
}
