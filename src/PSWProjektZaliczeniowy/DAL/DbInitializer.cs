using PSWProjektZaliczeniowy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSWProjektZaliczeniowy.DAL
{
    public class DbInitializer
    {
        public static void DodajKategorieIPodaktegorie(LeniwiecContext context)
        {
            context.Database.EnsureCreated();

            if (context.Kategoria.Any() || context.Podkategoria.Any())
            {
                return;
            }

            var kat_elektronika = new Kategoria { Nazwa = "Elektronika" };
            var kat_muzyka = new Kategoria { Nazwa = "Muzyka" };
            var kat_sport = new Kategoria { Nazwa = "Sport" };
            var kat_moda = new Kategoria { Nazwa = "Moda" };
            
            context.Kategoria.Add(kat_elektronika);
            context.Kategoria.Add(kat_muzyka);
            context.Kategoria.Add(kat_sport);
            context.Kategoria.Add(kat_moda);
            context.SaveChanges();

            /*kat_elektronika = context.Kategoria.Where(c => c.Nazwa == "Elektronika").ToList().First();
            kat_muzyka = context.Kategoria.Where(c => c.Nazwa == "Muzyka").ToList().First();
            kat_sport = context.Kategoria.Where(c => c.Nazwa == "Sport").ToList().First();
            kat_moda = context.Kategoria.Where(c => c.Nazwa == "Moda").ToList().First();*/

            var pod_el_komputery = new Podkategoria { Nazwa = "Komputery", Kategoria = kat_elektronika };
            var pod_el_grykonsole = new Podkategoria { Nazwa = "Gry i konsole", Kategoria = kat_elektronika };

            var pod_mu_instrumenty = new Podkategoria { Nazwa = "Instrumenty", Kategoria = kat_muzyka };
            var pod_mu_muzykacd = new Podkategoria { Nazwa = "Muzyka CD", Kategoria = kat_muzyka };

            var pod_sp_rowery = new Podkategoria { Nazwa = "Rowery", Kategoria = kat_sport };
            var pod_sp_obuwie = new Podkategoria { Nazwa = "Obuwie sportowe", Kategoria = kat_sport };

            var pod_mo_zegarki = new Podkategoria { Nazwa = "Zegarki", Kategoria = kat_moda };
            var pod_mo_ubrania = new Podkategoria { Nazwa = "Ubrania", Kategoria = kat_moda };

            context.Podkategoria.Add(pod_el_grykonsole);
            context.Podkategoria.Add(pod_el_komputery);
            context.Podkategoria.Add(pod_mu_instrumenty);
            context.Podkategoria.Add(pod_mu_muzykacd);
            context.Podkategoria.Add(pod_sp_obuwie);
            context.Podkategoria.Add(pod_sp_rowery);
            context.Podkategoria.Add(pod_mo_ubrania);
            context.Podkategoria.Add(pod_mo_zegarki);

            context.SaveChanges();
        }
    }
}
