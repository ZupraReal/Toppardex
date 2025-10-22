namespace Topardex.top.Persistencia;

public class IRepoProductoPedido
{
    public int IdProductoPedido { get; set; }
    public int IdPedido { get; set; }
    public int IdProducto { get; set; }

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
