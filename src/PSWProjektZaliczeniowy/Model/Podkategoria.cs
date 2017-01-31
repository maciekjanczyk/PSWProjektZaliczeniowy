using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Podkategoria
    {
        public Podkategoria()
        {
            Ogloszenie = new List<Ogloszenie>();
        }

        public int PodkategoriaId { get; set; }
        public string Nazwa { get; set; }
        
        public int KategoriaId { get; set; }
        public virtual Kategoria Kategoria { get; set; }

        public virtual List<Ogloszenie> Ogloszenie { get; set; }
    }
}
