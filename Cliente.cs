public class Cliente
{
    // Variables privadas de la clase Cliente
    private int dni; // DNI o identificación del cliente
    private string nombre; // Nombre del cliente
    private string direccion; // Dirección del cliente
    private string telefono; // Número de teléfono del cliente
    private string datosReferenciaDireccion; // Información adicional o referencias sobre la dirección del cliente

    // Constructor de la clase Cliente
    public Cliente(int dni, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        // Asignación de valores pasados por parámetros a las variables privadas
        this.dni = dni;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    // Propiedad para obtener y establecer la dirección del cliente
    public string Direccion { get => direccion; set => direccion = value; }

    // Propiedad para obtener y establecer el nombre del cliente
    public string Nombre { get => nombre; set => nombre = value; }

    // Sobrescribe el método ToString para devolver una cadena con la información completa del cliente
    public override string ToString()
    {
        return $"CLIENTE: \n\t* dni: {dni} \n\t* nombre: {nombre} \n\t* teléfono: {telefono} \n\t* dirección: {direccion} ({datosReferenciaDireccion})";
    }
}
