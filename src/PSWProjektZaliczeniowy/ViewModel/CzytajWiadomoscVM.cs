using PSWProjektZaliczeniowy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.ViewModel
{
    public class CzytajWiadomoscVM
    {
        public Wiadomosc Wiadomosc { get; set; }
        public Uzytkownik Sender { get; set; }
        public Uzytkownik Receiver { get; set; }
    }
}
