using System.Data;

namespace Topardex.Ado.Dapper;
public class RepoGenerico
{
    protected readonly IDbConnection Conexion;
    public RepoGenerico(IDbConnection conexion) => Conexion = conexion;
}