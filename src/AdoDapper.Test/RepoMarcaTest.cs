using MySqlConnector;
using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoMarcaTest : TestBase
{

    [Fact]
    public void AltaYObtenerMarcas_OK()
    {
        var repo = new RepoMarca(Conexion);

        var marca = new Marca { Nombre = "Nike" };
        repo.Alta(marca);

        var marcas = repo.Obtener().ToList();

        Assert.Contains(marcas, m => m.Nombre == "Nike");
    }
}
