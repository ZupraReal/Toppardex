namespace Topardex.top.Persistencia;

public class IRepoProducto
{
    public int IdProducto { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public int IdMarca { get; set; }
}
