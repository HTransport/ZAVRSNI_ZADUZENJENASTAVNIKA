using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZAVRSNI_ZADUZENJENASTAVNIKA.Models;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Services
{
    public class SkolskeGodineService
    {
        private readonly ApplicationDbContext _dbcont = new ApplicationDbContext();

        public List<string> GetList()
        {
            var list =  _dbcont.SkolskeGodine.ToList();
            List<string> strList = new List<string>();
            foreach (SkolskaGodina s in list)
            {
                strList.Add(s.ToString());
            }
            return strList;
        }

        public void Add(int godina)
        {
            var s =  _dbcont.SkolskeGodine.FirstOrDefault(S => S.Godina == godina);
            if (s != null)
                return;
            _dbcont.SkolskeGodine.Add(new SkolskaGodina() { Godina = godina});
             _dbcont.SaveChanges();
        }

        public void Delete(int godina)
        {
            var s =  _dbcont.SkolskeGodine.FirstOrDefault(S => S.Godina == godina);
            if (s is null)
                return;
            _dbcont.SkolskeGodine.Remove(s);
             _dbcont.SaveChanges();
        }

        public void Update(int godina, int godinaPrije)
        {
            var s =  _dbcont.SkolskeGodine.FirstOrDefault(S => S.Godina == godinaPrije);
            if (s is null)
                return;
            s.Godina = godina;
             _dbcont.SaveChanges();
        }

        public SkolskaGodina GetSingle(int godina)
        {
            var s =  _dbcont.SkolskeGodine.FirstOrDefault(S => S.Godina == godina);

            return s;
        }
    }
}
