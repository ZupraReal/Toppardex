using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoMarca : RepoGenerico
{
        private readonly IDbConnection _connection;

        public RepoMarca(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

    public void Alta(Marca marca)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", marca.Nombre);
        Conexion.Execute("AltaMarca", parametros, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Marca> Obtener()
    {
        return Conexion.Query<Marca>("SELECT * FROM Marca");
    }

    public Marca? Detalle(int id)
    {
        return Conexion.QueryFirstOrDefault<Marca>("SELECT * FROM Marca WHERE IdMarca = @id", new { id });
    }
}
