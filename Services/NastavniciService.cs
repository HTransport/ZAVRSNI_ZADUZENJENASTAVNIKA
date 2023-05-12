using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Services
{
    public class NastavniciService
    {
        private ApplicationDbContext _dbcont = new ApplicationDbContext();

        public List<Nastavnik> GetList()
        {
            _dbcont = new ApplicationDbContext();
            var list = _dbcont.Nastavnici.ToList();
            return list;
        }

        public void Add(string ime, string prezime, string oib,  string kategorija)
        {
            var s =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oib);
            if (s != null)
                return;
            _dbcont.Nastavnici.Add(new Nastavnik() { Ime = ime, Prezime = prezime, Kategorija = kategorija, OIB = oib});
             _dbcont.SaveChanges();
        }

        public void Delete(string oib)
        {
            var s =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oib);
            if (s is null)
                return;
            _dbcont.Nastavnici.Remove(s);
             _dbcont.SaveChanges();
        }

        public void Update(string ime, string prezime, string oib, string oibPrije, string kategorija)
        {
            var s =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oibPrije);
            if (s is null)
                return;
            s.Ime = ime;
            s.Prezime = prezime;
            s.Kategorija = kategorija;
            s.OIB = oib;
             _dbcont.SaveChanges();
        }

        public Nastavnik GetSingle(string oib)
        {
            var s =  _dbcont.Nastavnici.FirstOrDefault(S => S.OIB == oib);

            return s;
        }
    }
}
