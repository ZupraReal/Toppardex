using Microsoft.AspNetCore.Mvc;
using Topardex.Ado.Dapper;
using Topardex.top.Persistencia;
using Topardex; 

namespace Topardex.top.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepoProducto _repoProducto;
        private readonly IRepoMarca _repoMarca; 

        // Constructor
        public ProductoController(IRepoProducto repoProducto, IRepoMarca repoMarca)
        {
            _repoProducto = repoProducto;
            _repoMarca = repoMarca; 
        }

        // GET: /Producto
        public async Task<IActionResult> Index()
        {
            var productos = await _repoProducto.ObtenerAsync();
            var marcas = await _repoMarca.ObtenerAsync();

            // Diccionario ID -> Nombre de Marca
            ViewBag.NombresMarcas = marcas.ToDictionary(k => k.IdMarca, v => v.Nombre);

            return View(productos);
        }

        // GET: /Producto/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null) return NotFound();
            
            var marca = await _repoMarca.DetalleAsync(producto.IdMarca);
            ViewBag.NombreMarca = marca != null ? marca.Nombre : "Desconocida";
            
            return View(producto);
        }

        // GET: /Producto/Crear
        public async Task<IActionResult> Crear()
        {
            ViewBag.Marcas = await _repoMarca.ObtenerAsync();
            return View();
        }

        // POST: /Producto/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                await _repoProducto.AltaAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Marcas = await _repoMarca.ObtenerAsync();
            return View(producto);
        }

        // GET: /Producto/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _repoProducto.DetalleAsync(id);
            if (producto == null) return NotFound();
            ViewBag.Marcas = await _repoMarca.ObtenerAsync();
            return View(producto);
        }
        
        public async Task<IActionResult> Eliminar(int id)
        {
             // Lógica para eliminar (puedes agregarla después)
            return RedirectToAction(nameof(Index));
        }
    }
}