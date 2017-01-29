using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Kategoria
    {
        public Kategoria ()
        {
            Podkategoria = new List<Podkategoria>();
        }

        public int KategoriaId { get; set; }
        public string Nazwa { get; set; }

        public virtual List<Podkategoria> Podkategoria { get; set; }
    }
}
