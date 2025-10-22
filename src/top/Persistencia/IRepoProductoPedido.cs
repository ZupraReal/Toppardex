namespace Topardex.top.Persistencia;

public class IRepoProductoPedido
{
    public ushort IdProductoPedido { get; set; }
    public ushort IdPedido { get; set; }
    public ushort IdProducto { get; set; }

    public ushort Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
