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
        public void AltaPedido_DeberiaInsertarPedido_CuandoLosDatosSonValidos()
        {
            // Arrange: Aseguramos que exista un cliente de prueba
            var clienteExistente = Conexion.QueryFirstOrDefault<int>(
                "SELECT idCliente FROM Cliente WHERE idCliente = @IdCliente",
                new { IdCliente = 1 });

            if (clienteExistente == 0)
            {
                Conexion.Execute(
                    "INSERT INTO Cliente (idCliente, nombre, apellido, pais, fechaDeNacimiento) VALUES (@IdCliente, @Nombre, @Apellido, @Pais, @FechaNac)",
                    new
                    {
                        IdCliente = 1,
                        Nombre = "Cliente",
                        Apellido = "Prueba",
                        Pais = "Argentina",
                        FechaNac = new DateTime(1990, 1, 1)
                    });
            }

            // Creamos un pedido nuevo
            var pedido = new Pedido
            {
                idCliente = 1,
                FechaVenta = DateTime.Now
            };

            // Act: Llamamos al procedimiento AltaPedido
            var parametros = new DynamicParameters();
            parametros.Add("xidcliente", pedido.idCliente);
            parametros.Add("xfechaventa", pedido.FechaVenta);

            int idPedidoInsertado = Conexion.ExecuteScalar<int>(
                "AltaPedido",
                parametros,
                commandType: CommandType.StoredProcedure);

            // Assert: Comprobamos que se haya insertado correctamente
            Assert.True(idPedidoInsertado > 0, "No se insertó el pedido correctamente.");

            var pedidoDb = Conexion.QuerySingleOrDefault<Pedido>(
                "SELECT idPedido, idCliente, fechaVenta FROM Pedido WHERE idPedido = @IdPedido",
                new { IdPedido = idPedidoInsertado });

            Assert.NotNull(pedidoDb);
            Assert.Equal(pedido.idCliente, pedidoDb.idCliente);
            Assert.Equal(pedido.FechaVenta.ToString("yyyy-MM-dd"), pedidoDb.FechaVenta.ToString("yyyy-MM-dd"));
        }
    
}
