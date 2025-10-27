using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Topardex;
using Topardex.top.Persistencia; // si tu repositorio está ahí (ajustá si cambia el namespace)

namespace Topardex.top.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IRepoMarca _repoMarca;

        public MarcaController(IRepoMarca repoMarca)
        {
            _repoMarca = repoMarca;
        }


        // GET: /Marca
        public async Task<IActionResult> Index()
        { 
            var marcas = await _repoMarca.ObtenerAsync();
            return View(marcas);
        }

        // GET: /Marca/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var marca = await _repoMarca.DetalleAsync(id);
            if (marca == null)
                return NotFound();

            return View(marca);
        }

        // GET: /Marca/Crear
        [AdminOnly]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Marca/Crear
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Marca marca)
        {
            if (!ModelState.IsValid)
                return View(marca);

            await _repoMarca.AltaAsync(marca);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Marca/Editar/5
        [AdminOnly]
        public async Task<IActionResult> Editar(int id)
        {
            var marca = await _repoMarca.DetalleAsync(id);
            if (marca == null)
                return NotFound();

            return View(marca);
        }

        // POST: /Marca/Editar
        [AdminOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Marca marca)
        {
            if (!ModelState.IsValid)
                return View(marca);

            await _repoMarca.ActualizarMarcaAsync(marca);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Marca/Eliminar/5
        [AdminOnly]
        public async Task<IActionResult> Eliminar(int id)
        {
            var marca = await _repoMarca.DetalleAsync(id);
            if (marca == null)
                
                return NotFound();

            return View(marca);
        }

        // POST: /Marca/EliminarConfirmado
        [AdminOnly]
        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int idMarca)
        {
            try
            {
                await _repoMarca.EliminarMarcaAsync(idMarca);
                TempData["MensajeExito"] = "Marca eliminada correctamente.";
            }
            catch (MySqlException ex)
            {
                // Si es una violación de clave foránea (productos asociados)
                if (ex.Message.Contains("productos asociados"))
                    TempData["MensajeError"] = "No se puede eliminar la marca porque tiene productos asociados.";
                else
                    TempData["MensajeError"] = "Ocurrió un error al intentar eliminar la marca.";
            }

            return RedirectToAction("Index");
        }
    }
}
