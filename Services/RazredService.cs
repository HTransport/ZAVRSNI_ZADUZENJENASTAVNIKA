using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Services
{
    public class RazredService
    {
        private readonly ApplicationDbContext _dbcont = new ApplicationDbContext();

        public List<string> GetList(int godina)
        {
            var list =  _dbcont.Razredi.Include(f => f.SkolskaGodina).Where(s => s.SkolskaGodina.Godina == godina).ToList();
            List<string> strList = new List<string>();
            foreach (Razred r in list)
            {
                strList.Add(r.ToString());
            }
            return strList;
        }

        public void Add(int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Razredi.Include(f => f.SkolskaGodina).FirstOrDefault(S => godina == S.SkolskaGodina.Godina && S.Razina == razina && S.Oznaka == oznaka);
            if (s != null)
                return;
            var g =  _dbcont.SkolskeGodine.FirstOrDefault(G => G.Godina == godina);
            _dbcont.Razredi.Add(new Razred() { Razina = razina, Oznaka = oznaka, SkolskaGodinaId = g.Id});
             _dbcont.SaveChanges();
        }

        public void Delete(int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Razredi.Include(f => f.SkolskaGodina).FirstOrDefault(S => S.SkolskaGodina.Godina == godina && S.Razina == razina && S.Oznaka == oznaka);
            if (s is null)
                return;
            _dbcont.Razredi.Remove(s);
             _dbcont.SaveChanges();
        }

        public void Update(int razinaPrije, string oznakaPrije, int godinaPrije, int razina, string oznaka)
        {
            var s =  _dbcont.Razredi.Include(f => f.SkolskaGodina).FirstOrDefault(S => S.SkolskaGodina.Godina == godinaPrije && S.Razina == razinaPrije && S.Oznaka == oznakaPrije);
            if (s is null)
                return;
            s.Razina = razina;
            s.Oznaka = oznaka;
            s.SkolskaGodinaId = s.SkolskaGodina.Id;
             _dbcont.SaveChanges();
        }

        public Razred GetSingle(int razina, string oznaka, int godina)
        {
            var s =  _dbcont.Razredi.Include(f => f.SkolskaGodina).FirstOrDefault(S => S.SkolskaGodina.Godina == godina && S.Razina == razina && S.Oznaka == oznaka);

            return s;
        }
    }
}
