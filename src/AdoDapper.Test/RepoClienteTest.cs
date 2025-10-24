using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoClienteTest : TestBase
{
    [Fact]
    public async Task AltaYObtenerClientes_OK()
    {
        var repo = new RepoCliente(Conexion);

        var cliente = new Cliente
        {
            Nombre = "Ziad",
            Apellido = "Pin",
            Pais = "Argentina",
            FechaDeNacimiento = new DateTime(2006, 5, 21)
        };

        await repo.AltaAsync(cliente);

        var clientes = (await repo.ObtenerAsync()).ToList();

        Assert.Contains(clientes, c => c.Nombre == "Ziad");
    }
}
