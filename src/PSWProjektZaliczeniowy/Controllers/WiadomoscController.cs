using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.DAL;
using Microsoft.AspNetCore.Authorization;
using PSWProjektZaliczeniowy.ViewModel;
using PSWProjektZaliczeniowy.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class WiadomoscController : Controller
    {
        public readonly LeniwiecContext _context;

        public WiadomoscController(LeniwiecContext context)
        {
            _context = context;
        }

        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Wyslij(int id)
        {            
            var ogloszenie = _context.Ogloszenie.Find(id);
            _context.Uzytkownik.Where(u => u.UzytkownikId == ogloszenie.UzytkownikId).First();

            var odbiorcaVM = ogloszenie.Uzytkownik.Login;
            var tytulwiadVM = "Pytanie do ogłoszenia \"" + ogloszenie.Tytul + "\"";

            return View(new WiadomoscVM { Odbiorca = odbiorcaVM, Tytul = tytulwiadVM });
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Wyslij(WiadomoscVM wiad)
        {
            if (ModelState.IsValid)
            {
                var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
                var senderName = authInfo.Principal.Identity.Name;
                var sender = _context.Uzytkownik.First(u => u.Login == senderName);                
                var receiver = _context.Uzytkownik.First(u => u.Login == wiad.Odbiorca);
                
                _context.Wiadomosc.Add(new Wiadomosc {
                    Tekst = wiad.Tekst,
                    Data = DateTime.Now,
                    SenderId = sender.UzytkownikId,
                    ReceiverId = receiver.UzytkownikId,
                    Odczytana = false,
                    Tytul = wiad.Tytul
                });

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("KomunikatError", "Wysyłana wiadomość musi mieć jakąś treść, tytuł i odbiorcę!");

            return View(wiad);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Odpowiedz(int id, string ty)
        {
            var wiadomosc = _context.Wiadomosc.Find(id);
            var odbiorcaVM = _context.Uzytkownik.First(u => u.UzytkownikId == wiadomosc.SenderId);
            var tytulwiadVM = string.Format("Odp - {0}", ty);

            return View("Wyslij", new WiadomoscVM { Odbiorca = odbiorcaVM.Login, Tytul = tytulwiadVM });
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Odpowiedz(WiadomoscVM wiad)
        {
            return await Wyslij(wiad);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public IActionResult Admin()
        {
            var odbiorcaVM = _context.Uzytkownik.First(u => u.Login == "admin");
            var tytulwiadVM = "Problem";

            return View("Wyslij", new WiadomoscVM { Odbiorca = odbiorcaVM.Login, Tytul = tytulwiadVM });
        }

        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Admin(WiadomoscVM wiad)
        {
            return await Wyslij(wiad);
        }
    }
}
