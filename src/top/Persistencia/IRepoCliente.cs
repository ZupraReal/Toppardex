using System.Collections.Generic;
using System.Threading.Tasks;
using Topardex;

namespace Topardex.top.Persistencia;

    public interface IRepoCliente
    {
        Task AltaAsync(Cliente cliente);

        Task<IEnumerable<Cliente>> ObtenerAsync();

         Task<Cliente?> DetalleAsync(int id);

        Task ActualizarAsync(Cliente cliente);
        
        Task EliminarAsync(int id);
    }

