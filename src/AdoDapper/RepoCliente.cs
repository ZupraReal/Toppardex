using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoCliente : RepoGenerico, IRepoCliente
{
    private readonly IDbConnection _connection;

    public RepoCliente(IDbConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public async Task AltaAsync(Cliente cliente)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", cliente.Nombre);
        parametros.Add("@xapellido", cliente.Apellido);
        parametros.Add("@xpais", cliente.Pais);
        parametros.Add("@xfecha", cliente.FechaDeNacimiento);

        await Conexion.ExecuteAsync("AltaCliente", parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Cliente>> ObtenerAsync()
    {
        return await Conexion.QueryAsync<Cliente>("SELECT * FROM Cliente");
    }

    public async Task<Cliente?> DetalleAsync(int id)
    {
        return await Conexion.QueryFirstOrDefaultAsync<Cliente>(
            "SELECT * FROM Cliente WHERE IdCliente = @id",
            new { id });
    }

    public async Task ActualizarAsync(Cliente cliente)
    {

        var parametros = new DynamicParameters();
        parametros.Add("xidCliente", cliente.IdCliente);
        parametros.Add("xnombre", cliente.Nombre);
        parametros.Add("xapellido", cliente.Apellido);
        parametros.Add("xpais", cliente.Pais);
        parametros.Add("xFechaDeNacimiento", cliente.FechaDeNacimiento);

        await Conexion.ExecuteAsync("ActualizarCliente", parametros, commandType: CommandType.StoredProcedure);
    }
    public async Task EliminarAsync(int idCliente)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidCliente", idCliente);

        await Conexion.ExecuteAsync("EliminarCliente", parametros, commandType: CommandType.StoredProcedure);

    }
}
