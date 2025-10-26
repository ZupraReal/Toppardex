using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoPedido : RepoGenerico, IRepoPedido
{
    private readonly IDbConnection _connection;

    public RepoPedido(IDbConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public async Task<Pedido> AltaPedidoAsync(Pedido pedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidcliente", pedido.IdCliente);
        parametros.Add("xfechaventa", DateTime.Now);
        parametros.Add("xidPedido", dbType: DbType.Int32, direction: ParameterDirection.Output);

        // 1️⃣ Ejecutar SP AltaPedido
        await Conexion.ExecuteAsync(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        // 2️⃣ Obtener el idPedido generado
        int idPedidoInsertado = parametros.Get<int>("xidPedido");

        // 3️⃣ Insertar los productos
        foreach (var item in pedido.Productos)
        {
            var paramProd = new DynamicParameters();
            paramProd.Add("xidPedido", idPedidoInsertado);
            paramProd.Add("xidProducto", item.IdProducto);
            paramProd.Add("xcantidad", item.Cantidad);

            await Conexion.ExecuteAsync(
                "AltaProductoPedido",
                paramProd,
                commandType: CommandType.StoredProcedure
            );
        }


        // 4️⃣ Recuperar el pedido completo desde la BD
        var pedidoCompleto = await Conexion.QuerySingleAsync<Pedido>(
            @"SELECT idPedido AS IdPedido, idCliente AS IdCliente, fechaVenta AS FechaVenta, total AS Total
            FROM Pedido
            WHERE idPedido = @id",
            new { id = idPedidoInsertado }
        );

        var productos = await Conexion.QueryAsync<ProductoPedido>(
            @"SELECT pp.idProducto AS IdProducto,
                    p.nombre AS Nombre,
                    pp.cantidad AS Cantidad,
                    pp.precio AS PrecioUnitario
            FROM ProductoPedidos pp
            INNER JOIN Producto p ON p.idProducto = pp.idProducto
            WHERE pp.idPedido = @idPedido",
            new { idPedido = idPedidoInsertado }
        );

        pedidoCompleto.Productos = productos.ToList();

        return pedidoCompleto;
    }






  public async Task<IEnumerable<Pedido>> ObtenerAsync()
    {
        var pedidos = await Conexion.QueryAsync<Pedido>(
            "SELECT idPedido AS IdPedido, idCliente AS IdCliente, fechaVenta AS FechaVenta, total AS Total FROM Pedido"
        );

        foreach (var pedido in pedidos)
        {
            var productos = await Conexion.QueryAsync<ProductoPedido>(
                @"SELECT 
                    pp.idProducto AS IdProducto,
                    p.nombre AS Nombre,
                    pp.cantidad AS Cantidad,
                    pp.precio AS PrecioUnitario
                FROM ProductoPedidos pp
                INNER JOIN Producto p ON p.idProducto = pp.idProducto
                WHERE pp.idPedido = @idPedido",
                new { idPedido = pedido.IdPedido }
            );

            pedido.Productos = productos.ToList();
        }

        return pedidos;
    }


    public async Task<Pedido?> DetalleAsync(int idPedido)
    {
        var pedido = await Conexion.QuerySingleOrDefaultAsync<Pedido>(
            "SELECT idPedido AS IdPedido, idCliente AS IdCliente, fechaVenta AS FechaVenta, total AS Total FROM Pedido WHERE idPedido = @idPedido",
            new { idPedido }
        );

        if (pedido == null)
            return null;

        var productos = await Conexion.QueryAsync<ProductoPedido>(
            @"SELECT 
                pp.idProducto AS IdProducto,
                p.nombre AS Nombre,
                pp.cantidad AS Cantidad,
                pp.precio AS PrecioUnitario
            FROM ProductoPedidos pp
            INNER JOIN Producto p ON p.idProducto = pp.idProducto
            WHERE pp.idPedido = @idPedido",
            new { idPedido }
        );

        pedido.Productos = productos.ToList();

        return pedido;
    }

    public async Task<IEnumerable<Pedido>> ObtenerPorClienteAsync(int idCliente)
    {
    var sql = @"
        SELECT idPedido AS IdPedido, 
               idCliente AS IdCliente, 
               fechaVenta AS FechaVenta, 
               total AS Total
        FROM Pedido
        WHERE idCliente = @idCliente
        ORDER BY fechaVenta DESC";

    return await Conexion.QueryAsync<Pedido>(sql, new { idCliente });
}


}
