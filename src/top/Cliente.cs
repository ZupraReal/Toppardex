namespace Topardex;

public class Cliente
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public string Pais { get; set; } = "Argentina";
    public DateTime FechaDeNacimiento { get; set; }

    public List<Pedido> Pedidos { get; set; } = new();

    public string NombreCompleto => $"{Nombre} {Apellido}";
}
