using System.Collections.Generic;
using System.Threading.Tasks;
using Topardex;

namespace Topardex.top.Persistencia
{
    public interface IRepoPedido
    {
        Task<Pedido> AltaPedidoAsync(Pedido pedido);
        Task<IEnumerable<Pedido>> ObtenerAsync();
        Task<Pedido?> DetalleAsync(int idPedido);

        Task<IEnumerable<Pedido>> ObtenerPorClienteAsync(int idCliente);
    }
}

