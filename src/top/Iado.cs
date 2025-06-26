namespace Topardex;
public interface IAdo
{
    void AltaMarca(Marca marca);
    List<Marca> ObtenerMarcas();
    void AltaProducto(Producto producto);
    List<Producto> ObtenerProductos();
    Producto? ObtenerProducto(short id);

}