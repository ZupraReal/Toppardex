using Microsoft.Data.SqlClient;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class TestRepoProducto
{
    private readonly string _connectionString = "Server=localhost;Database=TopardexDB;Trusted_Connection=True;TrustServerCertificate=True;";

    [Fact]
    public void AltaYObtenerProductos_OK()
    {
        using var conexion = new SqlConnection(_connectionString);
        var repo = new RepoProducto(conexion);

        var producto = new Producto
        {
            Nombre = "Air Max 90",
            Precio = 120000,
            Stock = 10,
            IdMarca = 1
        };

        repo.Alta(producto);

        var productos = repo.Obtener().ToList();

        Assert.Contains(productos, p => p.Nombre == "Air Max 90");
    }
}
