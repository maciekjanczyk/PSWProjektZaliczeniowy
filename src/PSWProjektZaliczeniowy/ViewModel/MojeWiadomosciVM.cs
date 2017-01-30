using PSWProjektZaliczeniowy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.ViewModel
{
    public class MojeWiadomosciVM
    {
        public List<Tuple<Wiadomosc, Uzytkownik>> Wyslane { get; set; }
        public List<Tuple<Wiadomosc, Uzytkownik>> Odebrane { get; set; }
    }
}
