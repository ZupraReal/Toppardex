using System.Text.Json.Serialization;

namespace Topardex;

public class ProductoPedido
{
    [JsonIgnore]
    public int IdProductoPedido { get; set; }

    [JsonIgnore]
    public Producto? Producto { get; set; }

     public int IdProducto { get; set; } 
    public int Cantidad { get; set; }   
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
