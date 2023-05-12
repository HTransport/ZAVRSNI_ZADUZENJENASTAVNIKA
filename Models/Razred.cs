using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Models
{
    public class Razred
    {
        public int Id { get; set; }
        public int Razina { get; set; }
        public string Oznaka { get; set; }
        public int SkolskaGodinaId { get; set; }

        public SkolskaGodina SkolskaGodina { get; set; }

        public override string ToString()
        {
            return Razina + "." + Oznaka;
        }
    }
}
