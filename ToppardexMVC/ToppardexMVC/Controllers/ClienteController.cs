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
            if (!ModelState.IsValid)
                return View(cliente);

            await _repoCliente.AltaAsync(cliente);
            return RedirectToAction(nameof(Index));
        }
    }
}
