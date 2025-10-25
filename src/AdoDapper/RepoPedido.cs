using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoPedido : RepoGenerico  
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

        int idPedidoInsertado = await Conexion.ExecuteScalarAsync<int>(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        foreach (var item in pedido.Productos)
        {
            var param = new DynamicParameters();
            param.Add("xidpedido", idPedidoInsertado);
            param.Add("xidproducto", item.IdProducto);
            param.Add("xcantidad", item.Cantidad);
            param.Add("xpreciounitario", item.PrecioUnitario);
            await Conexion.ExecuteAsync("AltaProductoPedido", param, commandType: CommandType.StoredProcedure);
        }

        await Task.Delay(200);

        var pedidoCompleto = await Conexion.QuerySingleAsync<Pedido>(
            "SELECT idPedido AS IdPedido, idCliente AS IdCliente, fechaVenta AS FechaVenta, total AS Total FROM Pedido WHERE idPedido = @id",
            new { id = idPedidoInsertado }
        );

        var productos = await Conexion.QueryAsync<ProductoPedido>(
            @"SELECT 
                pp.idProducto AS IdProducto,
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
}
