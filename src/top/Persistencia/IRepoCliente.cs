using System.Collections.Generic;
using System.Threading.Tasks;
using Topardex;

namespace Topardex.top.Persistencia;

public interface IRepoCliente
{
    /// <summary>
    /// Operaciones de persistencia para Clientes.
    /// </summary>
    public interface IRepoCliente
    {
        /// <summary>
        /// Inserta un cliente (usa el Stored Procedure AltaCliente en tu BD).
        /// </summary>
        Task AltaAsync(Cliente cliente);

        /// <summary>
        /// Devuelve todos los clientes.
        /// </summary>
        Task<IEnumerable<Cliente>> ObtenerAsync();

        /// <summary>
        /// Devuelve un cliente por id o null si no existe.
        /// </summary>
        Task<Cliente?> DetalleAsync(int id);
    }
}
