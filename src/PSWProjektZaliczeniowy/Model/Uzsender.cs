using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Uzsender
    {
        [ForeignKey("Uzytkownik")]
        public int UzsenderId { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; }
        public virtual List<Wiadomosc> Wiadomosc { get; set; }
    }
}
