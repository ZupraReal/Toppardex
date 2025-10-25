using System.Text.Json.Serialization;

namespace Topardex;

public class Pedido
{
   [JsonIgnore]
    public int IdPedido { get; set; }

    public int IdCliente { get; set; }

   [JsonIgnore]
    public DateTime? FechaVenta { get; set; } = DateTime.Now;

    [JsonIgnore]
    public Cliente Cliente { get; set; }

    public List<ProductoPedido> Productos { get; set; } = new();

    [JsonIgnore]
    public decimal Total { get; set; } 

    [JsonIgnore]
    public string Estado { get; set; } = "Pendiente";
}
