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

        int idPedidoInsertado = await Conexion.ExecuteScalarAsync<int>(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure);

        await Conexion.ExecuteAsync(
            "INSERT INTO ProductoPedidos (idPedido, idProducto, precio, cantidad) VALUES (@Pedido, @Prod, @Precio, @Cant)",
            new[]
            {
                new { Pedido = idPedidoInsertado, Prod = 1, Precio = 100m, Cant = 2 },
                new { Pedido = idPedidoInsertado, Prod = 2, Precio = 200m, Cant = 1 }
            });

        await Task.Delay(500); // Espera al trigger

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("MySQL");

        await using var nuevaConexion = new MySqlConnection(connectionString);
        await nuevaConexion.OpenAsync();

        var pedidoDb = await nuevaConexion.QuerySingleOrDefaultAsync<Pedido>(
            "SELECT idPedido, idCliente, fechaVenta, total FROM Pedido WHERE idPedido = @IdPedido",
            new { IdPedido = idPedidoInsertado });

        var totalEsperado = (100m * 2) + (200m * 1);

        Assert.NotNull(pedidoDb);
        Assert.Equal(1, pedidoDb.idCliente);
        Assert.Equal(totalEsperado, pedidoDb.Total);
    }
}
