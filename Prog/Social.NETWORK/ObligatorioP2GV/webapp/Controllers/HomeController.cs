using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            if (lid != null)
            {
                string lnom = HttpContext.Session.GetString("LogueadoEmail");
                string lrol = HttpContext.Session.GetString("LogueadoRol");
                if (lrol == "a")
                {
                    lrol = "Administrador";
                }
                else
                {
                    lrol = "Miembro";
                }
                ViewBag.MensajeBienvenida = "Gracias por iniciar sesion " + lnom + " de rol " + lrol;
            }
            else
            {
                ViewBag.MensajeBienvenida = "Debe iniciar sesion";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}