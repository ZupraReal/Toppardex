using Dapper;
using System.Data;
using Topardex.top.Persistencia;

namespace Topardex.Ado.Dapper;

public class RepoProducto : RepoGenerico
{
    private readonly IDbConnection _connection;

    public RepoProducto(IDbConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public async Task AltaAsync(Producto producto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", producto.Nombre);
        parametros.Add("@xprecio", producto.Precio);
        parametros.Add("@xstock", producto.Stock);
        parametros.Add("@xidmarca", producto.IdMarca);

        await Conexion.ExecuteAsync("AltaProducto", parametros, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Producto>> ObtenerAsync()
    {
        return await Conexion.QueryAsync<Producto>("SELECT * FROM Producto");
    }

    public async Task<Producto?> DetalleAsync(int id)
    {
        return await Conexion.QueryFirstOrDefaultAsync<Producto>(
            "SELECT * FROM Producto WHERE IdProducto = @id",
            new { id });
    }

    public async Task<IEnumerable<Producto>> ObtenerPorMarcaAsync(int idMarca)
    {
        return await Conexion.QueryAsync<Producto>(
            "SELECT * FROM Producto WHERE IdMarca = @idMarca",
            new { idMarca });
    }

    public async Task<IEnumerable<Producto>> ObtenerPorPrecioAsync(decimal precio)
    {
        return await Conexion.QueryAsync<Producto>(
            "SELECT * FROM Producto WHERE Precio = @Precio",
            new { precio });
    }
}
