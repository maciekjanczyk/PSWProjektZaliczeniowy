using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Ogloszenie
    {
        public Ogloszenie()
        {
            Obserwowane = new List<Model.Obserwowane>();
        }

        public int OgloszenieId { get; set; }
        public string Tytul { get; set; }
        public string Opis { get; set; }
        public string Zdjecia { get; set; }
        public double Cena { get; set; }
        public bool Zamiana { get; set; }
        public string Stan { get; set; }
        
        public int UzytkownikId { get; set; }
        public virtual Uzytkownik Uzytkownik { get; set; }

        public int PodkategoriaId { get; set; }
        public virtual Podkategoria Podkategoria { get; set; }

        public virtual List<Obserwowane> Obserwowane { get; set; }
    }
}
