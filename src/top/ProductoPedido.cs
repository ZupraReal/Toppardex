namespace Topardex;

public class ProductoPedido
{
    public int IdProductoPedido { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
