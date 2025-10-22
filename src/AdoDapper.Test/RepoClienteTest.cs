using Microsoft.Data.SqlClient;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoClienteTest
{
    private readonly string _connectionString = "Server=localhost;Database=TopardexDB;Trusted_Connection=True;TrustServerCertificate=True;";

    [Fact]
    public void AltaYObtenerClientes_OK()
    {
        using var conexion = new SqlConnection(_connectionString);
        var repo = new RepoCliente(conexion);

        var cliente = new Cliente
        {
            Nombre = "Judas",
            Apellido = "PIn",
            Pais = "Argentina",
            FechaDeNacimiento = new DateTime(2006, 5, 21)
        };

        repo.Alta(cliente);

        var clientes = repo.Obtener().ToList();

        Assert.Contains(clientes, c => c.Nombre == "Judas");
    }
}
