using System.Text.Json.Serialization;

namespace Topardex;

public class Producto
{
    [JsonIgnore]
    public int IdProducto { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public int IdMarca { get; set; }

    [JsonIgnore]
    public Marca? Marca { get; set; }

    public string? Descripcion { get; set; }
}
