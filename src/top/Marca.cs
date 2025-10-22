namespace Topardex;

public class Marca
{
    public int IdMarca { get; set; }
    public required string Nombre { get; set; }

    public List<Producto> Productos { get; set; } = new();
}
