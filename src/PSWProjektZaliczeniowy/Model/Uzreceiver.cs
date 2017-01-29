using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Uzreceiver
    {
        [ForeignKey("Uzytkownik")]
        public int UzreceiverId { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; }
        public List<Wiadomosc> Wiadomosc { get; set; }
    }
}
