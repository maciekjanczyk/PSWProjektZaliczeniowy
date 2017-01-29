using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Wiadomosc
    {
        public int WiadomoscId { get; set; }
        public string Tekst { get; set; }
        public DateTime Data { get; set; }

        public Uzsender Uzsender { get; set; }
        public Uzreceiver Uzreceiver { get; set; }
    }
}
