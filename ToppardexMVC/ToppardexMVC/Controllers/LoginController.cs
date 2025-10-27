using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Security.Cryptography;
using System.Text;

namespace ToppardexMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _connectionString = "Server=localhost;Database=5to_Toppardex;User ID=root;Password=root;";

        [HttpGet]
        public IActionResult Index()
        {
            // Si ya hay sesión iniciada, va directo a Bienvenida
            if (HttpContext.Session.GetInt32("idUsuario") != null)
                return RedirectToAction("Bienvenida", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string pass)
        {
            string hash = Encriptar(pass);

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var cmd = new MySqlCommand("SELECT idUsuario, rol FROM Usuario WHERE email=@e AND pass=@p", con);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@p", hash);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int idUsuario = reader.GetInt32("idUsuario");
                string rol = reader.GetString("rol");

                HttpContext.Session.SetInt32("idUsuario", idUsuario);
                HttpContext.Session.SetString("Rol", rol);

                return RedirectToAction("Bienvenida", "Home");
            }

            TempData["MensajeError"] = "Usuario o contraseña incorrectos.";
            return View();
        }

        // Cerrar sesión
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
    }
}
