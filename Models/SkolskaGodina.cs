using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Models
{
    public class SkolskaGodina
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public int Godina { get; set; }

        public override string ToString()
        {
            return Godina + " / " + (Godina + 1);
        }
    }
}
