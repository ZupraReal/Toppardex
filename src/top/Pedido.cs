namespace Topardex;

public class Pedido
{
    public int IdPedido { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;

    public Cliente Cliente { get; set; }

    public List<ProductoPedido> Productos { get; set; } = new();

    public decimal Total => Productos.Sum(p => p.Subtotal);
    public string Estado { get; set; } = "Pendiente";
}
