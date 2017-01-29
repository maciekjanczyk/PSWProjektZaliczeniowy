using Microsoft.EntityFrameworkCore;
using PSWProjektZaliczeniowy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.DAL
{
    public class LeniwiecContext : DbContext
    {
        public LeniwiecContext(DbContextOptions<LeniwiecContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategoria>().ToTable("Kategoria");
            modelBuilder.Entity<Podkategoria>().ToTable("Podkategoria");                       
        }

        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<Obserwowane> Obserwowane { get; set; }
        public DbSet<Ogloszenie> Ogloszenie { get; set; }
        public DbSet<Podkategoria> Podkategoria { get; set; }
        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Wiadomosc> Wiadomosc { get; set; }
    }
}
