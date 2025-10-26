using Microsoft.AspNetCore.Mvc;
using Topardex.top.Persistencia;

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
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Marca/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Marca marca)
        {
            if (!ModelState.IsValid)
                return View(marca);

            await _repoMarca.AltaAsync(marca);
            return RedirectToAction(nameof(Index));
        }
    }
}
