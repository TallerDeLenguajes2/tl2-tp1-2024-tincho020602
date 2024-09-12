using System.IO.Compression;
using System.Text.Json.Serialization;
public class Cadeteria
{
    // Variables privadas de la clase Cadeteria
    private string nombre; // Nombre de la cadetería
    private string telefono; // Número de teléfono de la cadetería
    private List<Cadete> listadoCadetes; // Lista de cadetes disponibles en la cadetería
    private List<Pedido> pedidosAsignados;
    private List<Pedido> pedidosTomados;

    // Constructor de la clase Cadeteria
        public Cadeteria()
    {
        nombre = "";
        telefono = "";
        // Inicialización de las listas de cadetes y pedidos
        listadoCadetes = new List<Cadete>();
        pedidosAsignados = new List<Pedido>();
        pedidosTomados = new List<Pedido>();
    }

    public Cadeteria(string nombre, string telefono) : this()
    {
        // Asignación de los parámetros a las variables privadas
        this.nombre = nombre;
        this.telefono = telefono;
    }

        [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }
    // Propiedad para obtener y modificar la lista de cadetes
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public List<Pedido> PedidosAsignados { get => pedidosAsignados; set => pedidosAsignados = value; }
    public List<Pedido> PedidosTomados { get => pedidosTomados; set => pedidosTomados = value; }


    // Método para agregar un pedido a la lista de pedidos
    public void TomarPedido(Pedido pedido)
    {
        pedidosTomados.Add(pedido);
    }



    // Método para asignar un cadete a un pedido
    /*public void AsignarCadete(Cadete cadete, Pedido pedido)
    {
        // Asignamos el pedido al cadete
        cadete.Pedidos.Add(pedido);

        // Si el pedido ya había sido asignado a otro cadete, lo removemos de su lista
        var cadeteReasignado = listadoCadetes.Where(c => c.Id != cadete.Id && c.Pedidos.Contains(pedido)).FirstOrDefault();
        if (cadeteReasignado != null) cadeteReasignado.Pedidos.Remove(pedido);

        // También removemos el pedido de la lista general para evitar duplicaciones
        listadoPedidos.Remove(pedido);
    }*/

    // Método para dar de alta un nuevo cadete y agregarlo a la lista de cadetes
    public void AltaCadete(Cadete cadete)
    {
        listadoCadetes.Add(cadete);
    }

    // Método para obtener todos los pedidos asignados que aún están pendientes
    public List<Pedido> ObtenerPedidos(int idCadete, Estado estadoPedido)
    {
        // Selecciona los pedidos pendientes de todos los cadetes
        return pedidosTomados.Where(pedido => pedido.Cadete.Id == idCadete && 
                                              pedido.Estado == estadoPedido)
                             .ToList();
    }

    // Método para obtener todos los pedidos, tanto asignados como no asignados
    public List<Pedido> ObtenerTodosLosPedidos()
    {
        // Combina los pedidos generales con los pedidos asignados
          return pedidosTomados.Concat(pedidosAsignados).ToList();
    }

    // Sobrescribe el método ToString para devolver una cadena con la información de la cadetería
    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }


    // Método para calcular el jornal a cobrar basado en la cantidad de pedidos completados
    public float JornalACobrar(int id)
    {
        // Cuenta los pedidos en estado "COMPLETADO" y multiplica por el pago por pedido

        return 500 * pedidosAsignados.Where(p => p.Cadete.Id == id).Count();
    }

    //Agregar el método AsignarCadeteAPedido en la clase Cadeteria que recibe como parámetro el id del cadete y el id del Pedido
    public void AsignarCadeteAPedido(Cadete cadete, Pedido pedido)
    {
        pedido.Cadete = cadete;
        pedidosTomados.Remove(pedido);
        pedidosAsignados.Add(pedido);
    }


    public List<Pedido> BuscarPedidos(int idCadete)
    {
        return pedidosAsignados.Where(p => p.Cadete.Id == idCadete).ToList();
    }
}
