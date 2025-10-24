using Topardex.top.Persistencia;
using Xunit;

namespace Topardex.Ado.Dapper.Test;

public class TestRepoProducto : TestBase
{
    [Fact]
    public async Task AltaYObtenerProductos_OK()
    {
        var repo = new RepoProducto(Conexion);

        var producto = new Producto
        {
            Nombre = "Air Max 90",
            Precio = 120000,
            Stock = 10,
            IdMarca = 1
        };

        await repo.AltaAsync(producto);

        var productos = (await repo.ObtenerAsync()).ToList();

        Assert.Contains(productos, p => p.Nombre == "Air Max 90");
    }
}
