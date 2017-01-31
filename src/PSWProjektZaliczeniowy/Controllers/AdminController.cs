using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PSWProjektZaliczeniowy.DAL;
using PSWProjektZaliczeniowy.Model;
using PSWProjektZaliczeniowy.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class AdminController : Controller
    {
        public readonly LeniwiecContext _context;

        public AdminController(LeniwiecContext context)
        {
            _context = context;
        }

        private void DodajKategorie()
        {
            _context.Podkategoria.ToList();
            var katList = new List<SelectListItem>();

            foreach (var k in _context.Kategoria.ToList())
            {                
                var item = new SelectListItem { Text = k.Nazwa, Value = k.KategoriaId.ToString() };
                katList.Add(item);                
            }

            ViewData["Kategorie"] = katList;
        }

        [Authorize(ActiveAuthenticationSchemes = "MyCookie", Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie", Roles = "Admin")]
        public IActionResult DodKat()
        {
            return View();
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie", Roles = "Admin")]
        public IActionResult DodKat(Kategoria kat)
        {
            if (ModelState.IsValid)
            {
                if (_context.Kategoria.Where(k => k.Nazwa == kat.Nazwa).Any())
                {
                    ModelState.AddModelError("NazwaError", "Wybrana kategoria już istnieje w bazie.");
                    return View(kat);
                }
                else
                {
                    _context.Kategoria.Add(kat);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("NazwaError", "Wpisz nazwę kategorii.");
                return View(kat);
            }            
        }

        [HttpGet]
        public IActionResult DodPodkat()
        {
            DodajKategorie();
            return View();
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie", Roles = "Admin")]
        public IActionResult DodPodkat(NowaPodkategoriaVM nowa)
        {
            if (ModelState.IsValid)
            {
                var katid = Convert.ToInt32(nowa.Kategoria);
                var kategoria = _context.Kategoria.Find(katid);
                _context.Podkategoria.Where(p => p.KategoriaId == katid).ToList();
                
                if (kategoria.Podkategoria.Where(p => p.Nazwa == nowa.Nazwa).Any())
                {
                    ModelState.AddModelError("KomunikatError", "Taka podkategoria już istnieje.");
                    return View(nowa);
                }

                _context.Podkategoria.Add(new Podkategoria {
                    Nazwa = nowa.Nazwa,
                    Kategoria = kategoria
                });

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("KomunikatError", "Nie pisano nazwy podkategorii lub nie wybrano kategorii.");
                return View(nowa);
            }
        }



        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie", Roles = "Admin")]
        public IActionResult UsunOgloszenie(int id)
        {
            try
            {
                var ogl = _context.Ogloszenie.Find(id);
                _context.Ogloszenie.Remove(ogl);
                _context.SaveChanges();
            }
            catch (Exception) { }

            return RedirectToAction("Index");
        }
    }
}
