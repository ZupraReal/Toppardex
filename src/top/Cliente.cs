using System.Text.Json.Serialization;

namespace Topardex;

public class Cliente
{
    public int IdCliente { get; set; }


    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Pais { get; set; }
    public DateTime FechaDeNacimiento { get; set; }

    [JsonIgnore]
    public List<Pedido> Pedidos { get; set; } = new();

    public string NombreCompleto => $"{Nombre} {Apellido}";
}
