public class Pedido
{
    // Variable estática para generar un número de pedido único y consecutivo
    private static int ultimoNumeroGenerado = 0;
    
    // Variables privadas de la clase Pedido
    private int numero; // Número de pedido único
    private string observaciones; // Observaciones o comentarios del pedido
    private Cliente cliente; // Cliente asociado al pedido
    private Estado estado; // Estado actual del pedido (pendiente, completado, etc.)

    private Cadete cadete;//atributo cadete


    // Constructor de la clase Pedido
    public Pedido(string observaciones, Cliente cliente)    
    {
        // Asigna un número de pedido único, incrementando en 1 el último número generado
        numero = ++ultimoNumeroGenerado; 

        // El estado inicial del pedido es "PENDIENTE"
        estado = Estado.PENDIENTE;

        // Asigna las observaciones y el cliente proporcionados al crear el pedido
        this.observaciones = observaciones;
        this.cliente = cliente;

        // Inicializo el cadete en NULL porque puede que el pedido aún NO haya sido asignado a un cadete
        cadete = null;

    }

    // Propiedad para obtener y establecer el número del pedido
    public int Numero { get => numero; set => numero = value; }

    // Propiedad para obtener y establecer el estado del pedido
    public Estado Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    // Método que devuelve los datos completos del cliente asociado al pedido
    public string VerDatosCliente()
    {
        return $"*** CLIENTE ASOCIADO AL PEDIDO NRO. {numero} *** \n{cliente}";
    }

    // Método que devuelve la dirección del cliente asociada al pedido
    public string VerDireccionCliente()
    {
        return $"Pedido NRO. {numero} - Dirección del cliente: {cliente.Direccion}";
    }

    // Sobrescribe el método ToString para devolver una cadena con la información completa del pedido
    public override string ToString()
    {
        return $"PEDIDO NRO. {numero} - Obs.: {observaciones} - Cliente: {cliente.Nombre} - Estado: {estado}";
    }
}
