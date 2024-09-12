using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

public class Cadete
{
    // Constante que define el pago por cada pedido entregado
    private const float pagoPorPedidoEntregado = 500f;

    // Variables privadas de la clase Cadete
    private int id; // Identificación única del cadete
    private string nombre; // Nombre del cadete
    private string direccion; // Dirección del cadete
    private string telefono; // Teléfono del cadete

      public Cadete()
    {
        id = -1;
        nombre = "";
        direccion = "";
        telefono = "";
    }


    // Constructor de la clase Cadete
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        // Inicialización de las variables con los valores pasados por parámetros
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

      [JsonPropertyName("id")]
    // Propiedad para obtener y establecer el id del cadete
    public int Id { get => id; set => id = value; }
    [JsonPropertyName("nombre")]
    // Propiedad para obtener y establecer el nombre del cadete
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("direccion")]
    public string Direccion { get => direccion; set => direccion = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }
    

    // Método para buscar pedidos en un estado específico (como PENDIENTE o COMPLETADO)
  /*  public List<Pedido> BuscarPedidos(Estado estado)
    {
        // Filtra los pedidos según su estado y los devuelve en una lista
        return pedidos.Where(p => p.Estado == estado).ToList();
    }*/

    // Sobrescribe el método ToString para devolver una cadena con la información básica del cadete
    public override string ToString()
    {
        return $"CADETE: {id} - {nombre} - {direccion} - {telefono}";
    }
}
