using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSWProjektZaliczeniowy.Model;
using PSWProjektZaliczeniowy.DAL;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PSWProjektZaliczeniowy.Controllers
{
    public class OsobyController : Controller
    {
        public readonly LeniwiecContext _context;

        public OsobyController(LeniwiecContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context);
        }
    }
}
