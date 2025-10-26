using System.Collections.Generic;
using System.Threading.Tasks;
using Topardex;

namespace Topardex.top.Persistencia
{
    public interface IRepoProducto
    {
        Task AltaAsync(Producto producto);
        Task<IEnumerable<Producto>> ObtenerAsync();
        Task<Producto?> DetalleAsync(int id);
        Task<IEnumerable<Producto>> ObtenerPorMarcaAsync(int idMarca);
        Task<IEnumerable<Producto>> ObtenerPorPrecioAsync(decimal precio);

        Task ActualizarProductoAsync(Producto producto);
        Task EliminarProductoAsync(int id);
    }
}
