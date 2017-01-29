using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.DAL;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class OgloszeniaController : Controller
    {
        public readonly LeniwiecContext _context;

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
        public IActionResult Dodaj()
        {
            /*var lista = _context.Kategoria.ToList();
            var list = _context.Podkategoria.ToList();

            foreach (var k in lista)
            {                
                var query = list.Where(p => p.Kategoria.KategoriaId == k.KategoriaId);

                foreach (var p in query.ToList())
                {
                    k.Podkategoria.Add(p);
                }
            }

            return View(lista);*/
            var pdk = _context.Podkategoria.First();
            _context.Kategoria.Include("Podkategoria");
            return View(_context.Kategoria.ToList());
        }
    }
}
