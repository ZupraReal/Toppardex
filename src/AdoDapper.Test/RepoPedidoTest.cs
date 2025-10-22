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

        // IMPORTANTE: Asegúrate de que un cliente con ID = 1 exista
        // en la base de datos que usa este test.
        var idDeClienteValido = 1;

        var pedido = new Pedido
        {   
            // NO asignes IdPedido, la base de datos lo hace sola (es AUTO_INCREMENT).
            idCliente = idDeClienteValido, // <-- ESTA LÍNEA ES LA SOLUCIÓN
            FechaVenta = DateTime.Now
        };

        // Esto ahora enviará idCliente = 1 al stored procedure.
        repo.AltaPedido(pedido);

        var pedidos = repo.Obtener().ToList();

        Assert.NotEmpty(pedidos);
    }
}
