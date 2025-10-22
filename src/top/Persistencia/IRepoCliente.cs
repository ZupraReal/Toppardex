namespace Topardex.top.Persistencia;

public interface IRepoCliente
{
    public int IdCliente { get; set; }
    public  string Nombre { get; set; }
    public  string Apellido { get; set; }
    public string Pais { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
}
