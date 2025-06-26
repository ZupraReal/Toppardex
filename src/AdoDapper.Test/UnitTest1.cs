using Topardex;

namespace AdoDapper.Test;

public class UnitTest1 : TestAdo
{
    [Fact]
    public void AltaMarcaOK()
    {
        Marca marca1 = new()
        {
            IdMarca = 9,
            Nombre = "Puma"
        };

        Ado.AltaMarca(marca1);

        var marcas = Ado.ObtenerMarcas();

        Assert.NotNull(marcas);
    }


    [Fact]
    public void AltaProductoOK()
    {
        Producto prod = new()
        {
            IdProducto = 10,
            Nombre = "zapas",
            Precio = 60000,
            Stock = 25
        };

        Ado.AltaProducto(prod);

        var prods = Ado.ObtenerProductos();

        Assert.NotNull(prods);
    }
}