using Microsoft.Data.SqlClient;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.AdoDapper.Test;

public class RepoPedidoTest
{
    private readonly string _connectionString = "Server=localhost;Database=TopardexDB;Trusted_Connection=True;TrustServerCertificate=True;";

    [Fact]
    public void AltaYObtenerPedidos_OK()
    {
        using var conexion = new SqlConnection(_connectionString);
        var repo = new RepoPedido(conexion);

        var pedido = new Pedido
        {
            IdCliente = 1,
            FechaVenta = DateTime.Now
        };

        repo.Alta(pedido);

        var pedidos = repo.Obtener().ToList();

        Assert.NotEmpty(pedidos);
    }
}
