using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Models
{
    public class Predmet
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Kategorija { get; set; }
        public int NastavnikId { get; set; }
        public int RazredId { get; set; }

        public Nastavnik Nastavnik { get; set; }
        public Razred Razred { get; set; }
    }
}
