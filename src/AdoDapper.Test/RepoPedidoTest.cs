using Dapper;
using Xunit;
using System;
using System.Data;
using MySqlConnector; 
using Topardex.top.Persistencia;
using Topardex.Ado.Dapper.Test;
using Microsoft.Extensions.Configuration;


namespace Topardex.AdoDapper.Test;

public class RepoPedidoTest : TestBase
{
    [Fact]
    public void AltaPedido_DeberiaInsertarPedidoYActualizarTotal()
    {
        // Act
        var parametros = new DynamicParameters();
        parametros.Add("xidcliente", 1);
        parametros.Add("xfechaventa", DateTime.Now);

        int idPedidoInsertado = Conexion.ExecuteScalar<int>(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure);

        // Insertar productos
        Conexion.Execute(
            "INSERT INTO ProductoPedidos (idPedido, idProducto, precio, cantidad) VALUES (@Pedido, @Prod, @Precio, @Cant)",
            new[]
            {
                new { Pedido = idPedidoInsertado, Prod = 1, Precio = 100m, Cant = 2 },
                new { Pedido = idPedidoInsertado, Prod = 2, Precio = 200m, Cant = 1 }
            });

       
        System.Threading.Thread.Sleep(500); // 100 ms



        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("MySQL");

        using (var nuevaConexion = new MySqlConnection(connectionString))
        {
            var pedidoDb = nuevaConexion.QuerySingleOrDefault<Pedido>(
                "SELECT idPedido, idCliente, fechaVenta, total FROM Pedido WHERE idPedido = @IdPedido",
                new { IdPedido = idPedidoInsertado });

            var totalEsperado = (100m * 2) + (200m * 1);

            Console.WriteLine($"Total leído: {pedidoDb?.Total}");

            Assert.NotNull(pedidoDb);
            Assert.Equal(1, pedidoDb.idCliente);
            Assert.Equal(totalEsperado, pedidoDb.Total);
        }


    }

}
