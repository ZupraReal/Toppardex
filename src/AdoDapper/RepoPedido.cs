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

    public async Task<int> AltaPedidoAsync(Pedido pedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidcliente", pedido.idCliente);
        parametros.Add("xfechaventa", pedido.FechaVenta);

        int idPedidoInsertado = await Conexion.ExecuteScalarAsync<int>(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure);

        return idPedidoInsertado;
    }

    public async Task<IEnumerable<Pedido>> ObtenerAsync()
    {
        return await Conexion.QueryAsync<Pedido>("SELECT * FROM Pedido");
    }

    public async Task<Pedido?> DetalleAsync(int id)
    {
        return await Conexion.QueryFirstOrDefaultAsync<Pedido>(
            "SELECT * FROM Pedido WHERE IdPedido = @id",
            new { id });
    }
}
