using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.Model
{
    public class Wiadomosc
    {
        public int WiadomoscId { get; set; }
        public string Tytul { get; set; }
        public string Tekst { get; set; }
        public DateTime Data { get; set; }
        public bool Odczytana { get; set; }

        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}
