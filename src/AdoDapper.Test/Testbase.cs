using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Topardex.Ado.Dapper.Test;

public class TestBase
{
    protected readonly IDbConnection Conexion;

    public TestBase()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .Build();

        string cadena = config.GetConnectionString("MySQL")!;   
        Conexion = new MySqlConnection(cadena);
        Conexion.Open(); // 👈 abrimos la conexión acá
    }
}
