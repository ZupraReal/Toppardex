using Microsoft.AspNetCore.Mvc;
using Topardex.top.Persistencia;

namespace Topardex.top.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IRepoPedido _repoPedido;

        public PedidoController(IRepoPedido repoPedido)
        {
            _repoPedido = repoPedido;
        }

        // GET: /Pedido
        public async Task<IActionResult> Index()
        {
            var pedidos = await _repoPedido.ObtenerAsync();
            return View(pedidos);
        }

        // GET: /Pedido/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var pedido = await _repoPedido.DetalleAsync(id);
            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        // GET: /Pedido/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Pedido/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Pedido pedido)
        {
            if (!ModelState.IsValid)
                return View(pedido);

            await _repoPedido.AltaPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }
    }
}
