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

    public void Alta(Pedido pedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xidPedido", pedido.IdPedido);
        parametros.Add("@xfechaventa", pedido.FechaVenta);

        Conexion.Execute("AltaPedido", parametros, commandType: CommandType.StoredProcedure);
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
