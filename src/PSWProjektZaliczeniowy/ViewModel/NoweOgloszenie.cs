using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.ViewModel
{
    public class NoweOgloszenie
    {
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Opis { get; set; }
        public IFormFile Zdjecie { get; set; }
        public double Cena { get; set; }
        public bool Zamiana { get; set; }
        public string Stan { get; set; }
        [Required]
        public string Podkategoria { get; set; }
    }
}
