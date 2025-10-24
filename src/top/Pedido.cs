namespace Topardex;

public class Pedido
{
    public int IdPedido { get; set; }

    public int idCliente { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;

    public Cliente Cliente { get; set; }

    public List<ProductoPedido> Productos { get; set; } = new();

    public decimal Total { get; set; } 
    public string Estado { get; set; } = "Pendiente";
}
