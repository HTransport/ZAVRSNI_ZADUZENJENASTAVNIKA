using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Models
{
    public class Zamjena
    {
        public int Id { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public int NastavnikId { get; set; }
        public int PredmetId { get; set; }

        public Nastavnik Nastavnik { get; set; }
        public Predmet Predmet { get; set; }
    }
}
