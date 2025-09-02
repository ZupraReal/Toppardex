namespace Topardex;
public class Cliente
{
    public ushort IdCliente { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Pais { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
    public IEnumerable<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
