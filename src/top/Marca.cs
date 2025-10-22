namespace Topardex;

public class Marca
{
    public ushort IdMarca { get; set; }
    public required string Nombre { get; set; }

    public List<Producto> Productos { get; set; } = new();
}
