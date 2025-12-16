using Microsoft.AspNetCore.Mvc;
using Topardex.Ado.Dapper;
using Topardex.top.Persistencia;
using Topardex; 

namespace Topardex.top.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IRepoPedido _repoPedido;
        private readonly IRepoProducto _repoProducto;
        private readonly IRepoCliente _repoCliente; 

        public PedidoController(IRepoPedido repoPedido, IRepoProducto repoProducto, IRepoCliente repoCliente)
        {
            _repoPedido = repoPedido;
            _repoProducto = repoProducto;
            _repoCliente = repoCliente; 
        }

        // GET: /Pedido
        public async Task<IActionResult> Index()
        {
            var pedidos = await _repoPedido.ObtenerAsync();
            var clientes = await _repoCliente.ObtenerAsync();

            ViewBag.NombresClientes = clientes.ToDictionary(
                k => k.IdCliente, 
                v => $"{v.Nombre} {v.Apellido}"
            );

            return View(pedidos);
        }

        // GET: /Pedido/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var pedido = await _repoPedido.DetalleAsync(id);
            if (pedido == null) return NotFound();

            var cliente = await _repoCliente.DetalleAsync(pedido.IdCliente);
            ViewBag.NombreCliente = cliente != null ? $"{cliente.Nombre} {cliente.Apellido}" : $"ID: {pedido.IdCliente}";

            return View(pedido);
        }

        // GET: /Pedido/Crear
        public async Task<IActionResult> Crear()
        {
            ViewBag.Productos = await _repoProducto.ObtenerAsync();
            ViewBag.Clientes = await _repoCliente.ObtenerAsync(); 
            return View();
        }

        // POST: /Pedido/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Pedido pedido)
        {
            // 1. Validar lista vacía
            if (pedido.Productos == null || pedido.Productos.Count == 0)
            {
                TempData["MensajeError"] = "⚠ El pedido debe tener al menos un producto.";
                
                ViewBag.Productos = await _repoProducto.ObtenerAsync();
                ViewBag.Clientes = await _repoCliente.ObtenerAsync(); 
                return View(pedido);
            }

            // 2. VALIDACIÓN DE STOCK
            foreach (var item in pedido.Productos)
            {
                var productoEnBd = await _repoProducto.DetalleAsync(item.IdProducto);

                if (item.Cantidad > productoEnBd.Stock)
                {
                    // Usamos TempData para asegurar que la alerta se vea
                    TempData["MensajeError"] = $"⛔ STOCK INSUFICIENTE: '{productoEnBd.Nombre}' tiene {productoEnBd.Stock} u., pediste {item.Cantidad}.";
                    
                    // Recargamos listas
                    ViewBag.Productos = await _repoProducto.ObtenerAsync();
                    ViewBag.Clientes = await _repoCliente.ObtenerAsync(); 
                    
                    // Devolvemos lo que el usuario escribió para no borrarle todo
                    return View(pedido);
                }
            }

            // 3. Guardar
            var pedidoCreado = await _repoPedido.AltaPedidoAsync(pedido);
            return RedirectToAction(nameof(Detalle), new { id = pedidoCreado.IdPedido });
        }

        public IActionResult Buscar() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Buscar(int idCliente)
        {
            var pedidos = await _repoPedido.ObtenerPorClienteAsync(idCliente);
            if (!pedidos.Any()) ViewBag.Mensaje = "No se encontraron pedidos.";
            return View("ResultadoBusqueda", pedidos);
        }

        public IActionResult BuscarPorId() { return View(); }

        [HttpPost]
        public async Task<IActionResult> BuscarPorId(int idPedido)
        {
            var pedido = await _repoPedido.DetalleAsync(idPedido);
            if (pedido == null) 
            { 
                TempData["MensajeError"] = "No encontrado."; 
                return View(); 
            }

            var cliente = await _repoCliente.DetalleAsync(pedido.IdCliente);
            ViewBag.NombreCliente = cliente != null ? $"{cliente.Nombre} {cliente.Apellido}" : $"ID: {pedido.IdCliente}";
            
            return View("Detalle", pedido);
        }
    }
}