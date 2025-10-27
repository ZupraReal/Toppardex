using Microsoft.AspNetCore.Mvc;
using Topardex;
using Topardex.top.Persistencia;

namespace TopardexMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepoProducto _repoProducto;
        private readonly IRepoMarca _repoMarca;

        public ProductoController(IRepoProducto repoProducto, IRepoMarca repoMarca)
        {
            _repoProducto = repoProducto;
            _repoMarca = repoMarca;
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
        [AdminOnly]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Marcas = await _repoMarca.ObtenerAsync(); // para el dropdown
            return View();
        }

        // POST: /Producto/Crear
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Marcas = await _repoMarca.ObtenerAsync();
                return View(producto);
            }

            await _repoProducto.AltaAsync(producto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Producto/Editar/5
        [AdminOnly]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null)
                return NotFound();

            ViewBag.Marcas = await _repoMarca.ObtenerAsync();
            return View(producto);
        }

        // POST: /Producto/Editar
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Marcas = await _repoMarca.ObtenerAsync();
                return View(producto);
            }

            await _repoProducto.ActualizarProductoAsync(producto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Producto/Eliminar/5
        [AdminOnly]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // POST: /Producto/EliminarConfirmado
        [AdminOnly]
        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int IdProducto)
        {
            await _repoProducto.EliminarProductoAsync(IdProducto);
            return RedirectToAction(nameof(Index));
        }
    }
}
