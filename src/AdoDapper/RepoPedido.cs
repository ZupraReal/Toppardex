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

    public int AltaPedido(Pedido pedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidcliente", pedido.idCliente);
        parametros.Add("xfechaventa", pedido.FechaVenta);

        int idPedidoInsertado = Conexion.ExecuteScalar<int>(
            "AltaPedido",
            parametros,
            commandType: CommandType.StoredProcedure);

        return idPedidoInsertado;
    }



    public IEnumerable<Pedido> Obtener()
    {
        return Conexion.Query<Pedido>("SELECT * FROM Pedido");
    }

    public Pedido? Detalle(int id)
    {
        return Conexion.QueryFirstOrDefault<Pedido>("SELECT * FROM Pedido WHERE IdPedido = @id", new { id });
    }
}
