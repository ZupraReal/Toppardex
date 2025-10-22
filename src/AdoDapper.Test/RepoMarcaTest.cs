using Microsoft.Data.SqlClient;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoMarcaTest
{
    private readonly string _connectionString = "Server=localhost;Database=TopardexDB;Trusted_Connection=True;TrustServerCertificate=True;";

    [Fact]
    public void AltaYObtenerMarcas_OK()
    {
        using var conexion = new SqlConnection(_connectionString);
        var repo = new RepoMarca(conexion);

        var marca = new Marca { Nombre = "Nike" };
        repo.Alta(marca);

        var marcas = repo.Obtener().ToList();

        Assert.Contains(marcas, m => m.Nombre == "Nike");
    }
}
