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
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Dodaj()
        {
            DodajKategorie();
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Dodaj(NoweOgloszenie nowe)
        {
            if (ModelState.IsValid)
            {
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
    }
}
