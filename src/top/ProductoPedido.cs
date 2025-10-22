namespace Topardex;

public class ProductoPedido
{
    public ushort IdProductoPedido { get; set; }
    public Producto Producto { get; set; }

    public ushort Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
