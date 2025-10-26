using System.Collections.Generic;
using System.Threading.Tasks;
using Topardex;

namespace Topardex.top.Persistencia
{
    public interface IRepoMarca
    {
        Task AltaAsync(Marca marca);
        Task<IEnumerable<Marca>> ObtenerAsync();
        Task<Marca?> DetalleAsync(int id);

        Task ActualizarMarcaAsync(Marca marca);
        Task EliminarMarcaAsync(int id);
    }
}
