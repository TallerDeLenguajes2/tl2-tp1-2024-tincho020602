public class Cadeteria
{
    // Variables privadas de la clase Cadeteria
    private string nombre; // Nombre de la cadetería
    private string telefono; // Número de teléfono de la cadetería
    private List<Cadete> listadoCadetes; // Lista de cadetes disponibles en la cadetería
    private List<Pedido> listadoPedidos; // Lista de pedidos en la cadetería

    // Constructor de la clase Cadeteria
    public Cadeteria(string nombre, string telefono)
    {
        // Asignación de los parámetros a las variables privadas
        this.nombre = nombre;
        this.telefono = telefono;

        // Inicialización de las listas de cadetes y pedidos
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedido>();
    }

    // Propiedad para obtener y modificar la lista de cadetes
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    // Propiedad para obtener y modificar la lista de pedidos
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    // Método para agregar un pedido a la lista de pedidos
    public void TomarPedido(Pedido pedido)
    {
        listadoPedidos.Add(pedido);
    }

    // Método para asignar un cadete a un pedido
    public void AsignarCadete(Cadete cadete, Pedido pedido)
    {
        // Asignamos el pedido al cadete
        cadete.Pedidos.Add(pedido);

        // Si el pedido ya había sido asignado a otro cadete, lo removemos de su lista
        var cadeteReasignado = listadoCadetes.Where(c => c.Id != cadete.Id && c.Pedidos.Contains(pedido)).FirstOrDefault();
        if (cadeteReasignado != null) cadeteReasignado.Pedidos.Remove(pedido);

        // También removemos el pedido de la lista general para evitar duplicaciones
        listadoPedidos.Remove(pedido);
    }

    // Método para dar de alta un nuevo cadete y agregarlo a la lista de cadetes
    public void AltaCadete(Cadete cadete)
    {
        listadoCadetes.Add(cadete);
    }

    // Método para obtener todos los pedidos asignados que aún están pendientes
    public List<Pedido> ObtenerPedidosAsignados()
    {
        // Selecciona los pedidos pendientes de todos los cadetes
        return listadoCadetes.SelectMany(cadete => cadete.Pedidos)
                             .Where(pedido => pedido.Estado == Estado.PENDIENTE)
                             .ToList();
    }

    // Método para obtener todos los pedidos, tanto asignados como no asignados
    public List<Pedido> ObtenerTodosLosPedidos()
    {
        // Combina los pedidos generales con los pedidos asignados
        return listadoPedidos.Concat(ObtenerPedidosAsignados()).ToList();
    }

    // Sobrescribe el método ToString para devolver una cadena con la información de la cadetería
    public override string ToString()
    {
        return $"CADETERIA: {nombre} - {telefono}";
    }
}
