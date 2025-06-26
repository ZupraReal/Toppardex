namespace Topardex;
public class ProductoPedido
{
    public ushort IdPedido { get; set; }
    public required Producto Producto { get; set; }
    public byte NumeroTalle { get; set; } 
    public decimal Precio { get; set; }
    public ushort Cantidad { get; set; }
    public ushort IdProducto { get; set; }        
    
}