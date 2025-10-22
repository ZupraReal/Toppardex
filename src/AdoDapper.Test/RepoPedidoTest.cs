using Microsoft.Data.SqlClient;
using Topardex.Ado.Dapper;
using Topardex.Ado.Dapper.Test;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.AdoDapper.Test;

public class RepoPedidoTest : TestBase
{

    [Fact]
    public void AltaYObtenerPedidos_OK()
    {
        var repo = new RepoPedido(Conexion);

        var pedido = new Pedido
        {
            IdPedido = 1,
            FechaVenta = DateTime.Now
        };

        repo.Alta(pedido);

        var pedidos = repo.Obtener().ToList();

        Assert.NotEmpty(pedidos);
    }
}
