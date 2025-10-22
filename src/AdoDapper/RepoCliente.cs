using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoCliente : RepoGenerico
{
    public RepoCliente(IDbConnection conexion) : base(conexion) { }

    public void Alta(Cliente cliente)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", cliente.Nombre);
        parametros.Add("@xapellido", cliente.Apellido);
        parametros.Add("@xpais", cliente.Pais);
        parametros.Add("@xfecha", cliente.FechaDeNacimiento);

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
