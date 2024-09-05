public class Cadete
{
    // Constante que define el pago por cada pedido entregado
    private const float pagoPorPedidoEntregado = 500f;

    // Variables privadas de la clase Cadete
    private int id; // Identificación única del cadete
    private string nombre; // Nombre del cadete
    private string direccion; // Dirección del cadete
    private string telefono; // Teléfono del cadete
    private List<Pedido> pedidos; // Lista de pedidos asignados al cadete

    // Constructor de la clase Cadete
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        // Inicialización de las variables con los valores pasados por parámetros
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;

        // Inicialización de la lista de pedidos
        this.pedidos = new List<Pedido>();
    }

    // Propiedad para obtener y establecer el id del cadete
    public int Id { get => id; set => id = value; }

    // Propiedad para obtener y establecer la lista de pedidos asignados al cadete
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    // Propiedad para obtener y establecer el nombre del cadete
    public string Nombre { get => nombre; set => nombre = value; }

    // Método para calcular el jornal a cobrar basado en la cantidad de pedidos completados
    public float JornalACobrar()
    {
        // Cuenta los pedidos en estado "COMPLETADO" y multiplica por el pago por pedido
        return pagoPorPedidoEntregado * pedidos.Where(p => p.Estado == Estado.COMPLETADO).Count();
    }

    // Método para buscar pedidos en un estado específico (como PENDIENTE o COMPLETADO)
    public List<Pedido> BuscarPedidos(Estado estado)
    {
        // Filtra los pedidos según su estado y los devuelve en una lista
        return pedidos.Where(p => p.Estado == estado).ToList();
    }

    // Sobrescribe el método ToString para devolver una cadena con la información básica del cadete
    public override string ToString()
    {
        return $"CADETE: {id} - {nombre} - {direccion} - {telefono}";
    }
}
