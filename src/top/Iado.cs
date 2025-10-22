namespace Topardex;

public interface IAdo
{
    // Marcas
    void AgregarMarca(Marca marca);
    List<Marca> ListarMarcas();

    // Productos
    void AgregarProducto(Producto producto);
    List<Producto> ListarProductos();
    Producto? ObtenerProductoPorId(int id);

    // Clientes
    void AgregarCliente(Cliente cliente);
    List<Cliente> ListarClientes();

    // Pedidos
    void AgregarPedido(Pedido pedido);
    List<Pedido> ListarPedidos();
}
