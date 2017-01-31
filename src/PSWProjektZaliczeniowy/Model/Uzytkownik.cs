using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Uzytkownik
    {
        public Uzytkownik()
        {
            Ogloszenie = new List<Ogloszenie>();
            Obserwowane = new List<Obserwowane>();
        }

        public int UzytkownikId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Haslo { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Nrtel { get; set; }
        public string Adres { get; set; }

        //public virtual List<Wiadomosc> Wiadomosc { get; set; }

        public virtual List<Ogloszenie> Ogloszenie { get; set; }
        public virtual List<Obserwowane> Obserwowane { get; set; }
    }
}
