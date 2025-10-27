using Microsoft.AspNetCore.Mvc;
using Topardex.top.Persistencia;

namespace Topardex.top.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepoCliente _repoCliente;

        public ClienteController(IRepoCliente repoCliente)
        {
            _repoCliente = repoCliente;
        }

        // GET: /Cliente
        public async Task<IActionResult> Index()
        {
            var clientes = await _repoCliente.ObtenerAsync();
            return View(clientes);
        }

        // GET: /Cliente/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var cliente = await _repoCliente.DetalleAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // GET: /Cliente/Crear
        [AdminOnly]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Cliente/Crear
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            // Validar campos de texto
            if (ContieneNumeros(cliente.Nombre) || ContieneNumeros(cliente.Apellido) || ContieneNumeros(cliente.Pais))
            {
                ViewBag.Error = "Nombre, Apellido y País no deben contener números.";
                return View(cliente); // Vuelve a la vista con el mensaje
            }

            // Validar modelo
            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _repoCliente.AltaAsync(cliente);
                TempData["MensajeExito"] = "Cliente creado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error al crear el cliente.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContieneNumeros(string texto)
        {
            return !string.IsNullOrEmpty(texto) && texto.Any(char.IsDigit);
        }

    }
}
