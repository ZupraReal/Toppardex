using Dapper;
using Xunit;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using Topardex.top.Persistencia;
using System.Data;

namespace Topardex.Ado.Dapper.Test;

public class RepoPedidoTest : TestBase
{
    [Fact]
    public async Task AltaPedido_DeberiaInsertarPedidoYActualizarTotal()
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidcliente", 1);
        parametros.Add("xfechaventa", DateTime.Now);
        parametros.Add("xidPedido", dbType: DbType.Int32, direction: ParameterDirection.Output);

        // Ejecuta el SP con parámetro OUT
        await Conexion.ExecuteAsync(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        int idPedidoInsertado = parametros.Get<int>("xidPedido");
        Assert.True(idPedidoInsertado > 0, "No se obtuvo idPedido desde el SP");

        // Insertar productos
        await Conexion.ExecuteAsync(
            "INSERT INTO ProductoPedidos (idPedido, idProducto, precio, cantidad) VALUES (@Pedido, @Prod, @Precio, @Cant)",
            new[]
            {
                new { Pedido = idPedidoInsertado, Prod = 1, Precio = 100m, Cant = 2 },
                new { Pedido = idPedidoInsertado, Prod = 2, Precio = 200m, Cant = 1 }
            });

        // Espera que el trigger actualice el total
        await Task.Delay(500);

        // Recuperar el pedido desde BD
        var pedidoDb = await Conexion.QuerySingleOrDefaultAsync<Pedido>(
            "SELECT idPedido, idCliente, fechaVenta, total FROM Pedido WHERE idPedido = @IdPedido",
            new { IdPedido = idPedidoInsertado });

        var totalEsperado = (100m * 2) + (200m * 1);

        Assert.NotNull(pedidoDb);
        Assert.Equal(1, pedidoDb.IdCliente);
        Assert.Equal(totalEsperado, pedidoDb.Total);
    }

}
