using System.Data;
using Topardex;
using Dapper;
using System.ComponentModel;

namespace AdoDapper;

public class AdoDaper
{
    private readonly IDbConnection _conexion;

    public AdoDaper(IDbConnection conexion)
        => _conexion = conexion;

    public void AltaMarca(Marca marca)
    {
        var parametros = new DynamicParameters();
        var query = "INSERT INTO Marca Values (@idmarca,@nombre)";
        _conexion.Execute(
                query,
                new
                {
                    nombre = marca.Nombre
                }
            );
    }

    public void AltaProducto(Producto producto)
    {
        var parametros = new DynamicParameters();

        parametros.Add("xidProducto", producto.IdProducto);
        parametros.Add("xnombre", producto.Nombre);
        parametros.Add("xprecio", producto.Precio);
        parametros.Add("xstock", producto.Stock);
        parametros.Add("xidMarca", producto.IdMarca);
          _conexion.Execute("AgrProducto", parametros);
    }

    public List<Marca> ObtenerMarcas()
    {
        var queryMarcas = "SELECT * FROM Marca";
        var productos = _conexion.Query<Marca>(queryMarcas);
        return productos.ToList();
    }

    public Producto? ObtenerProducto(short id)
    {
        var queryProductos = $"SELECT * FROM Productos WHERE idProducto = {id}";
        var producto = _conexion.QueryFirstOrDefault<Producto>(queryProductos);
        return producto;
    }

    public List<Producto> ObtenerProductos()
    {
        var queryProductos = "SELECT * FROM Productos";
        var productos = _conexion.Query<Producto>(queryProductos);
        return productos.ToList();
    }
}
