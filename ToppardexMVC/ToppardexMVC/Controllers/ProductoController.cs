using Microsoft.AspNetCore.Mvc;
using Topardex.top.Persistencia;

namespace Topardex.top.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepoProducto _repoProducto;

        public ProductoController(IRepoProducto repoProducto)
        {
            _repoProducto = repoProducto;
        }

        // GET: /Producto
        public async Task<IActionResult> Index()
        {
            var productos = await _repoProducto.ObtenerAsync();
            return View(productos);
        }

        // GET: /Producto/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // GET: /Producto/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Producto/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            await _repoProducto.AltaAsync(producto);
            return RedirectToAction(nameof(Index));
        }
    }
}
