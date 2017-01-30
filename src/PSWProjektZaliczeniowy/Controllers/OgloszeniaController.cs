using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PSWProjektZaliczeniowy.ViewModel;
using Microsoft.AspNetCore.Http;
using PSWProjektZaliczeniowy.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class OgloszeniaController : Controller
    {
        public readonly LeniwiecContext _context;

        private void DodajKategorie()
        {
            _context.Podkategoria.ToList();
            _context.Kategoria.Include("Podkategoria");
            var katList = new List<SelectListItem>();

            foreach (var k in _context.Kategoria.ToList())
            {
                var group = new SelectListGroup { Name = k.Nazwa };

                foreach (var p in k.Podkategoria)
                {
                    var item = new SelectListItem { Group = group, Text = p.Nazwa, Value = p.PodkategoriaId.ToString() };
                    katList.Add(item);
                }
            }

            ViewData["Podkategorie"] = katList;
        }

        public OgloszeniaController(LeniwiecContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var katLista = _context.Kategoria.ToList();
            _context.Podkategoria.ToList();

            return View(katLista);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Dodaj()
        {
            DodajKategorie();
            return View();
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Dodaj(NoweOgloszenie nowe)
        {
            if (ModelState.IsValid)
            {
                var filename = await DodajZdjecie(nowe.Zdjecie);
                var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
                var userName = authInfo.Principal.Identity.Name;
                var user = _context.Uzytkownik.Where(u => u.Login == userName).First();
                var pid = Convert.ToInt32(nowe.Podkategoria);

                var ogloszenie = new Ogloszenie
                {
                    Zdjecia = filename,
                    Uzytkownik = user,
                    Stan = "Aktualne",
                    Cena = nowe.Cena,
                    Zamiana = nowe.Cena == 0 ? true : false,
                    Podkategoria = _context.Podkategoria.Find(pid),
                    Opis = nowe.Opis,
                    Tytul = nowe.Tytul
                };

                _context.Ogloszenie.Add(ogloszenie);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                DodajKategorie();

                if (nowe.Tytul == null)
                {
                    ModelState.AddModelError("TytulError", "Musisz wprowadzić tytuł ogłoszenia!");
                }

                if (nowe.Opis == null)
                {
                    ModelState.AddModelError("OpisError", "Brakuje opisu przedmiotu!");
                }

                if (nowe.Podkategoria == null)
                {
                    ModelState.AddModelError("PodkategoriaError", "Musisz wybrać kategorię!");
                }

                return View(nowe);
            }
        }

        private async Task<string> DodajZdjecie(IFormFile zdjecie)
        {
            if (zdjecie != null)
            {                
                if (!System.IO.Directory.Exists("wwwroot\\images\\upload"))
                {
                    System.IO.Directory.CreateDirectory("wwwroot\\images\\upload");
                }               

                var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
                var userName = authInfo.Principal.Identity.Name;

                if (!System.IO.Directory.Exists("wwwroot\\images\\upload\\" + userName))
                {
                    System.IO.Directory.CreateDirectory("wwwroot\\images\\upload\\" + userName);
                }

                var ret = "images\\upload\\" + userName + "\\" + zdjecie.FileName;

                using (var stream = new System.IO.FileStream("wwwroot\\" + ret, System.IO.FileMode.Create))
                {
                    await zdjecie.CopyToAsync(stream);
                }

                return ret;
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public IActionResult Pokaz(int id)
        {
            var ogl = _context.Ogloszenie.Find(id);
            _context.Uzytkownik.Find(ogl.UzytkownikId);
            _context.Podkategoria.Find(ogl.PodkategoriaId);
            _context.Kategoria.Find(ogl.Podkategoria.KategoriaId);
            return View(_context.Ogloszenie.Find(id));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Uzytkownik()
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var user = _context.Uzytkownik.First(u => u.Login == userName);
            var listaOgloszen = _context.Ogloszenie.Where(o => o.UzytkownikId == user.UzytkownikId).ToList();

            foreach (var og in listaOgloszen)
            {
                var podk = _context.Podkategoria.First(p => p.PodkategoriaId == og.PodkategoriaId);
                _context.Kategoria.First(k => k.KategoriaId == podk.KategoriaId);
            }

            return View(listaOgloszen);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Usun(int id)
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var ogloszenie = _context.Ogloszenie.First(o => o.OgloszenieId == id);
            var wlasciciel = _context.Uzytkownik.First(u => u.UzytkownikId == ogloszenie.UzytkownikId);

            if (wlasciciel.Login != userName)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    System.IO.File.Delete("wwwroot\\" + ogloszenie.Zdjecia);
                }
                catch (Exception) { }

                _context.Ogloszenie.Remove(ogloszenie);
                _context.SaveChanges();

                return RedirectToAction("Uzytkownik");
            }
        }

        public IActionResult Kategoria(int id)
        {
            var podkat = _context.Podkategoria.Find(id);
            _context.Kategoria.Find(podkat.KategoriaId);
            _context.Ogloszenie.Where(o => o.PodkategoriaId == podkat.PodkategoriaId).ToList();

            return View(podkat);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Edycja(int id)
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var ogloszenie = _context.Ogloszenie.Find(id);
            var wlasciciel = _context.Uzytkownik.First(u => u.UzytkownikId == ogloszenie.UzytkownikId);

            if (wlasciciel.Login != userName)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(ogloszenie);
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Edycja(Ogloszenie ogl)
        {
            if (!ModelState.IsValid)
            {
                if (ogl.Tytul == null)
                {
                    ModelState.AddModelError("TytulError", "Musisz wprowadzić tytuł ogłoszenia!");
                }

                if (ogl.Opis == null)
                {
                    ModelState.AddModelError("OpisError", "Brakuje opisu przedmiotu!");
                }

                return View(ogl);
            }

            var refka = _context.Ogloszenie.Find(ogl.OgloszenieId);
            refka.Tytul = ogl.Tytul;
            refka.Opis = ogl.Opis;
            refka.Cena = ogl.Cena;
            _context.SaveChanges();

            return RedirectToAction("Uzytkownik");
        }
    }
}
