namespace Topardex.top.Persistencia;

public class IRepoPedido
{
    public ushort IdPedido { get; set; }
    public ushort IdCliente { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.Now;
    public string Estado { get; set; } = "Pendiente";
}
