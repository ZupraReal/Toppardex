namespace Topardex;
public class Producto
{
    public ushort IdProducto { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public ushort Stock { get; set; }
    public ushort IdMarca { get; set; }
    public List<Talle> Talles { get; set; } = [];
}



