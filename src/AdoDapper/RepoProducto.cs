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

    public void Alta(Producto producto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xnombre", producto.Nombre);
        parametros.Add("@xprecio", producto.Precio);
        parametros.Add("@xstock", producto.Stock);
        parametros.Add("@xidmarca", producto.IdMarca);

        Conexion.Execute("AltaProducto", parametros, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Producto> Obtener()
    {
        return Conexion.Query<Producto>("SELECT * FROM Producto");
    }

    public Producto? Detalle(int id)
    {
        return Conexion.QueryFirstOrDefault<Producto>("SELECT * FROM Producto WHERE IdProducto = @id", new { id });
    }

    public IEnumerable<Producto> ObtenerPorMarca(int idMarca)
    {
        return Conexion.Query<Producto>("SELECT * FROM Producto WHERE IdMarca = @idMarca", new { idMarca });
    }

        public IEnumerable<Producto> ObtenerPorPrecio(decimal precio)
    {
        return Conexion.Query<Producto>("SELECT * FROM Producto WHERE Precio = @Precio", new { precio });
    }
}
