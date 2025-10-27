using Microsoft.AspNetCore.Mvc;
using ToppardexMVC.Models;
using System.Diagnostics;

namespace ToppardexMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Página principal (solo accesible si hay sesión)
        public IActionResult Bienvenida()
        {
            if (HttpContext.Session.GetInt32("idUsuario") == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public IActionResult Contacto()
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
