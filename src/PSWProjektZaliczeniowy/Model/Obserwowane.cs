using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Obserwowane
    {
        public int ObserwowaneId { get; set; }

        public Uzytkownik Uzytkownik { get; set; }

        public Ogloszenie Ogloszenie { get; set; }
    }
}
