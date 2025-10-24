using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class RepoMarcaTest : TestBase
{
    [Fact]
    public async Task AltaYObtenerMarcas_OK()
    {
        var repo = new RepoMarca(Conexion);

        var marca = new Marca { Nombre = "Nike" };
        await repo.AltaAsync(marca);

        var marcas = (await repo.ObtenerAsync()).ToList();

        Assert.Contains(marcas, m => m.Nombre == "Nike");
    }
}
