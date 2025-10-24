using Microsoft.Data.SqlClient;
using Topardex.Ado.Dapper;
using Topardex.Ado.Dapper.Test;
using Topardex.top.Persistencia;
using Xunit;
using Dapper;
using System;
using System.Data;


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

        // Esperar un pequeño momento a que el trigger actualice el total (por seguridad)
        System.Threading.Thread.Sleep(500); // 100 ms

        // Reconsultar el pedido desde la base
        var pedidoDb = Conexion.QuerySingleOrDefault<Pedido>(
            "SELECT idPedido, idCliente, fechaVenta, total FROM Pedido WHERE idPedido = @IdPedido",
            new { IdPedido = idPedidoInsertado });

        // Assert
        var totalEsperado = (100m * 2) + (200m * 1);
        Assert.NotNull(pedidoDb);
        Assert.Equal(1, pedidoDb.idCliente);
        Assert.Equal(totalEsperado, pedidoDb.Total);
    }


}
