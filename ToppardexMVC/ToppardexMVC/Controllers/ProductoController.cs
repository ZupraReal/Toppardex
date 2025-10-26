using Microsoft.AspNetCore.Mvc;
using Topardex;
using Topardex.top.Persistencia;


namespace ToppardexMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepoProducto _repoProducto;

        public ProductoController(IRepoProducto repoProducto)
        {
            _repoProducto = repoProducto;
        }

        // 🔹 GET: /Producto
        public async Task<IActionResult> Index()
        {
            var productos = await _repoProducto.ObtenerAsync();
            return View(productos);
        }

        // 🔹 GET: /Producto/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // 🔹 GET: /Producto/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // 🔹 POST: /Producto/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            await _repoProducto.AltaAsync(producto);
            return RedirectToAction(nameof(Index));
        }

        // 🔹 GET: /Producto/PorMarca/3
        public async Task<IActionResult> PorMarca(int idMarca)
        {
            var productos = await _repoProducto.ObtenerPorMarcaAsync(idMarca);
            return View("Index", productos);
        }

        // 🔹 GET: /Producto/PorPrecio?precio=100
        public async Task<IActionResult> PorPrecio(decimal precio)
        {
            var productos = await _repoProducto.ObtenerPorPrecioAsync(precio);
            return View("Index", productos);
        }
    }
}
