namespace Topardex.top.Persistencia;

public class PedidoDb
{
    public int IdPedido { get; set; }
    public int IdCliente { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;
    public string Estado { get; set; } = "Pendiente";
}
