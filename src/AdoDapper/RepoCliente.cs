using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoCliente : RepoGenerico
{
    private readonly IDbConnection _connection;

    public RepoCliente(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

    public void Alta(Cliente Cliente)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", Cliente.Nombre);
        parametros.Add("@xapellido", Cliente.Apellido);
        parametros.Add("@xpais", Cliente.Pais);
        parametros.Add("@xfecha", Cliente.FechaDeNacimiento);

        Conexion.Execute("AltaCliente", parametros, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Cliente> Obtener()
    {
        return Conexion.Query<Cliente>("SELECT * FROM Cliente");
    }

    public Cliente? Detalle(int id)
    {
        return Conexion.QueryFirstOrDefault<Cliente>("SELECT * FROM Cliente WHERE IdCliente = @id", new { id });
    }
}
