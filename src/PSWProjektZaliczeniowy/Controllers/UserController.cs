using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.Model;
using PSWProjektZaliczeniowy.DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PSWProjektZaliczeniowy.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class UserController : Controller
    {
        private readonly LeniwiecContext _context;

        public UserController(LeniwiecContext context)
        {
            _context = context;
        }

        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Index()
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");            
            var userName = authInfo.Principal.Identity.Name;

            if (userName == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewData["UserName"] = userName;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");

            if (authInfo.Principal != null && authInfo.Principal.IsInRole("User"))
            {
                return RedirectToAction("Index", "User");
            }

            if (authInfo.Principal != null && authInfo.Principal.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Uzytkownik user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Uzytkownik.Where(u => u.Login == user.Login && u.Haslo == user.Haslo).ToList().Any())
                {

                    if (user.Login == "admin")
                    {
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, user.Login),
                                        new Claim(ClaimTypes.Role, "Admin")},
                                            "Basic"));
                        await HttpContext.Authentication.SignInAsync("MyCookie", principal);
                    }
                    else
                    {
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, user.Login),
                                        new Claim(ClaimTypes.Role, "User")},
                                            "Basic"));
                        await HttpContext.Authentication.SignInAsync("MyCookie", principal);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Błędne dane logowania!");
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("MyCookie");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Uzytkownik user)
        {
            if (ModelState.IsValid)
            {
                bool loginWolny = true;
                bool emailWolny = true;

                if (_context.Uzytkownik.Where(u => u.Login == user.Login).ToList().Count > 0)
                {
                    loginWolny = false;
                    ModelState.AddModelError("LoginError", "Login zajęty!");
                }

                if (_context.Uzytkownik.Where(u => u.Email == user.Email).ToList().Count > 0)
                {
                    emailWolny = false;
                    ModelState.AddModelError("EmailError", "Email istnieje w bazie!");
                }

                if (loginWolny && emailWolny)
                {
                    _context.Uzytkownik.Add(user);
                    _context.SaveChanges();

                    var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, user.Login),
                                        new Claim(ClaimTypes.Role, "User")},
                                        "Basic"));
                    await HttpContext.Authentication.SignInAsync("MyCookie", principal);

                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> CzytajWiadomosc(int id)
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var wiad = _context.Wiadomosc.Find(id);
            var sender = _context.Uzytkownik.First(u => u.UzytkownikId == wiad.SenderId);
            var receiver = _context.Uzytkownik.First(u => u.UzytkownikId == wiad.ReceiverId);

            if (userName != sender.Login && userName != receiver.Login)
            {
                return RedirectToAction("Index", "Home");
            }

            if (receiver.Login == userName && (!wiad.Odczytana))
            {
                wiad.Odczytana = true;
                _context.SaveChanges();
                wiad = _context.Wiadomosc.Find(id);
            }

            return View(new CzytajWiadomoscVM {
                Wiadomosc = wiad,
                Sender = sender,
                Receiver = receiver
            });
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> MojeWiadomosci()
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var user = _context.Uzytkownik.First(u => u.Login == userName);            

            var wyslane = _context.Wiadomosc.Where(w => w.SenderId == user.UzytkownikId).ToList();
            var retwysl = new List<Tuple<Wiadomosc, Uzytkownik>>();

            foreach (var w in wyslane)
            {
                var u = _context.Uzytkownik.Find(w.ReceiverId);
                retwysl.Add(new Tuple<Wiadomosc, Uzytkownik>(w, u));
            }

            retwysl.Sort((t1, t2) => { return t1.Item1.Data.Ticks > t2.Item1.Data.Ticks ? -1 : 1; });

            var odebrane = _context.Wiadomosc.Where(w => w.ReceiverId == user.UzytkownikId).ToList();
            var retodr = new List<Tuple<Wiadomosc, Uzytkownik>>();

            foreach (var o in odebrane)
            {
                var u = _context.Uzytkownik.Find(o.SenderId);
                retodr.Add(new Tuple<Wiadomosc, Uzytkownik>(o, u));
            }

            retodr.Sort((t1, t2) => { return t1.Item1.Data.Ticks > t2.Item1.Data.Ticks ? -1 : 1; });

            return View(new MojeWiadomosciVM { Wyslane = retwysl, Odebrane = retodr });
        }

        [HttpGet]
        [Authorize(ActiveAuthenticationSchemes = "MyCookie")]
        public async Task<IActionResult> Obserwowane()
        {
            var authInfo = await HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookie");
            var userName = authInfo.Principal.Identity.Name;
            var user = _context.Uzytkownik.First(u => u.Login == userName);

            _context.Obserwowane.Where(ob => ob.UzytkownikId == user.UzytkownikId).ToList();

            foreach (var ob in user.Obserwowane)
            {
                _context.Ogloszenie.First(og => og.OgloszenieId == ob.OgloszenieId);
                _context.Podkategoria.Find(ob.Ogloszenie.PodkategoriaId);
                _context.Kategoria.Find(ob.Ogloszenie.Podkategoria.KategoriaId);
                _context.Uzytkownik.Find(ob.Ogloszenie.UzytkownikId);
            }

            return View(user.Obserwowane);
        }
    }
}
