namespace Topardex;
public class Pedido
{
    public ushort IdPedido { get; set; }
    public ushort IdCliente { get; set; }
    public DateTime FechaVenta { get; set; }
    public IEnumerable<ProductoPedido> Productos { get; set; } = new List<ProductoPedido>();
    public Cliente Cliente { get; set; }   
}