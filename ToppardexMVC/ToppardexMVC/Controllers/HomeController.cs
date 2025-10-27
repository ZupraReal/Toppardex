using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Security.Cryptography;
using System.Text;
using ToppardexMVC.Models;
using System.Diagnostics;

namespace ToppardexMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = "Server=localhost;Database=5to_Toppardex;User ID=root;Password=root;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Muestra formulario de login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Procesa el login
        [HttpPost]
        public IActionResult Index(string email, string pass)
        {
            string hash = Encriptar(pass);

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var cmd = new MySqlCommand(
                "SELECT idUsuario, rol FROM Usuario WHERE email=@e AND pass=@p", con);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@p", hash);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int idUsuario = reader.GetInt32("idUsuario");
                string rol = reader.GetString("rol");

                HttpContext.Session.SetInt32("idUsuario", idUsuario);
                HttpContext.Session.SetString("Rol", rol);

                // Redirige según el rol
                if (rol == "Admin")
                    return RedirectToAction("Index", "Producto");
                else
                    return RedirectToAction("Index", "Pedido");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        private string Encriptar(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder();
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
