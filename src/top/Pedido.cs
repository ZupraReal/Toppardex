using System.Text.Json.Serialization;

namespace Topardex;

public class Pedido
{

    public int IdPedido { get; set; }

    public int IdCliente { get; set; }


    public DateTime? FechaVenta { get; set; } = DateTime.Now;


    public Cliente Cliente { get; set; }

    public List<ProductoPedido> Productos { get; set; } = new();

  
    public decimal Total { get; set; } 

}
