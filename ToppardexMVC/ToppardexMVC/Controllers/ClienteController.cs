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
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Cliente/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            // Validación de números
            if (ContieneNumeros(cliente.Nombre) || ContieneNumeros(cliente.Apellido) || ContieneNumeros(cliente.Pais))
            {
                TempData["MensajeError"] = "Nombre, Apellido y País no deben contener números.";
                return RedirectToAction("Crear"); // Redirige a la vista de creación con mensaje
            }

            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _repoCliente.AltaAsync(cliente);
                TempData["MensajeExito"] = "Cliente creado correctamente.";
            }
            catch
            {
                TempData["MensajeError"] = "Ocurrió un error al crear el cliente.";
            }

            return RedirectToAction("Index", "Cliente");
        }


        // 🔹 GET: /Cliente/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _repoCliente.DetalleAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // 🔹 POST: /Cliente/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            if (ContieneNumeros(cliente.Nombre) || ContieneNumeros(cliente.Apellido) || ContieneNumeros(cliente.Pais))
            {
                TempData["MensajeError"] = "Nombre, Apellido y País no deben contener números.";
                return View(cliente);
            }

            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _repoCliente.ActualizarAsync(cliente);
                TempData["MensajeExito"] = "Cliente actualizado correctamente.";
            }
            catch
            {
                TempData["MensajeError"] = "Ocurrió un error al actualizar el cliente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 GET: /Cliente/Eliminar/5
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _repoCliente.DetalleAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // 🔹 POST: /Cliente/EliminarConfirmado
        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int idCliente)
        {
            try
            {
                await _repoCliente.EliminarAsync(idCliente);
                TempData["MensajeExito"] = "Cliente eliminado correctamente.";
            }
            catch
            {
                TempData["MensajeError"] = "No se pudo eliminar el cliente. Verifique dependencias.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContieneNumeros(string texto)
        {
            return !string.IsNullOrEmpty(texto) && texto.Any(char.IsDigit);
        }
    }
}
