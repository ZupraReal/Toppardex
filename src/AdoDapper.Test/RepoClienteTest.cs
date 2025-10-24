using MySqlConnector;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoClienteTest : TestBase
{

    [Fact]
    public void AltaYObtenerClientes_OK()
    {
        var repo = new RepoCliente(Conexion);

        var cliente = new Cliente
        {
            Nombre = "Ziad",
            Apellido = "PIn",
            Pais = "Argentina",
            FechaDeNacimiento = new DateTime(2006, 5, 21)
        };

        repo.Alta(cliente);

        var clientes = repo.Obtener().ToList();

        Assert.Contains(clientes, c => c.Nombre == "Judas");
    }
}
