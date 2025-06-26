using System.Data;
using Topardex;
using MySqlConnector;

namespace AdoDapper.Test;

public class TestAdo
{
    protected readonly IAdo Ado;

    IDbConnection _conexion;

    private const string _cadena = "Server=localhost;Database=5to_Toppardex;user=5to_agbd;Password=Trigg3rs!";

    public TestAdo() : this(_cadena) {}

    public TestAdo(string cadena)
    {
        _conexion = new MySqlConnection(_cadena);
        Ado = new AdoDaper(_conexion);
    }
}
