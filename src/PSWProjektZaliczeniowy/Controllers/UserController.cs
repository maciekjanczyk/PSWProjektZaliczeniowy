using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.Model;
using PSWProjektZaliczeniowy.DAL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            ViewData["UserName"] = authInfo.Principal.Identity.Name;

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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Uzytkownik user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Uzytkownik.Where(u => u.Login == user.Login && u.Haslo == user.Haslo).ToList().Any())
                {
                    var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, user.Login),
                                        new Claim(ClaimTypes.Role, "User")},
                                        "Basic"));
                    await HttpContext.Authentication.SignInAsync("MyCookie", principal);                

                    return RedirectToAction("Index", "Home");
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

                    return RedirectToAction("Login", "User", user);
                }
            }

            return View(user);
        }
    }
}
