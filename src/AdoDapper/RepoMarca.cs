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

    public async Task AltaAsync(Marca marca)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", marca.Nombre);
        await Conexion.ExecuteAsync("AltaMarca", parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Marca>> ObtenerAsync()
    {
        return await Conexion.QueryAsync<Marca>("SELECT * FROM Marca");
    }

    public async Task<Marca?> DetalleAsync(int id)
    {
        return await Conexion.QueryFirstOrDefaultAsync<Marca>(
            "SELECT * FROM Marca WHERE IdMarca = @id",
            new { id });
    }
}
