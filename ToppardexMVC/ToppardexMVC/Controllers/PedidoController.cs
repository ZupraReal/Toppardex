using Microsoft.AspNetCore.Mvc;
using Topardex.Ado.Dapper;
using Topardex.top.Persistencia;

namespace Topardex.top.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IRepoPedido _repoPedido;
        private readonly IRepoProducto _repoProducto;

        public PedidoController(IRepoPedido repoPedido, IRepoProducto repoProducto)
        {
            _repoPedido = repoPedido;
            _repoProducto = repoProducto;
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

        public async Task<IActionResult> Crear()
        {
            ViewBag.Productos = await _repoProducto.ObtenerAsync();
            return View();
        }

        // POST: /Pedido/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Pedido pedido)
        {
            Console.WriteLine($"IdCliente: {pedido.IdCliente}");
            Console.WriteLine($"Productos count: {pedido.Productos?.Count ?? 0}");

            if (pedido.Productos == null || pedido.Productos.Count == 0)
            {
                ModelState.AddModelError("", "Debe agregar al menos un producto al pedido.");
                ViewBag.Productos = await _repoProducto.ObtenerAsync();
                return View(pedido);
            }

            foreach (var p in pedido.Productos)
            {
                Console.WriteLine($"Producto: {p.IdProducto}, Cantidad: {p.Cantidad}, Precio: {p.PrecioUnitario}");
            }

            var pedidoCreado = await _repoPedido.AltaPedidoAsync(pedido);

            return RedirectToAction(nameof(Detalle), new { id = pedidoCreado.IdPedido });
        }

        // GET: /Pedido/Buscar
        public IActionResult Buscar()
        {
            return View();
        }

        // POST: /Pedido/Buscar
        [HttpPost]
        public async Task<IActionResult> Buscar(int idCliente)
        {
            var pedidos = await _repoPedido.ObtenerPorClienteAsync(idCliente);

            if (!pedidos.Any())
            {
                ViewBag.Mensaje = "No se encontraron pedidos para este cliente.";
            }

            return View("ResultadoBusqueda", pedidos);
        }

        // GET: /Pedido/BuscarPorId
        public IActionResult BuscarPorId()
        {
            return View();
        }

        // POST: /Pedido/BuscarPorId
        [HttpPost]
        public async Task<IActionResult> BuscarPorId(int idPedido)
        {
            var pedido = await _repoPedido.DetalleAsync(idPedido);

            if (pedido == null)
            {
                TempData["MensajeError"] = "No se encontró ningún pedido con ese ID.";
                return View();
            }

            // Reutilizamos la vista Detalle
            return View("Detalle", pedido);
        }


    }
}
