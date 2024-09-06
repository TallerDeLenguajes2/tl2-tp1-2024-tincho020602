class Program
{
    private static Cadeteria cadeteria;

    private static void Main(string[] args)
    {
        try
        {
            // Intenta cargar los datos de los archivos CSV y crear la instancia de la cadetería.
            cadeteria = CrearCadeteria();
            CargarCadetes(cadeteria);
        }
        catch (Exception ex)
        {
            // Si ocurre algún error durante la carga de datos, muestra el mensaje de error.
            MostrarError(ex.Message);
        }

        int opcionSeleccionada = 0;
        int opcionSalida = 5; // Esta es la opción que el usuario debe seleccionar para salir del programa.
        do
        {
            // Mostrar el menú principal de opciones.
            Console.WriteLine("----------MENU----------\n");
            Console.WriteLine("1) Dar de alta un pedido.");
            Console.WriteLine("2) Asignar un pedido a un cadete.");
            Console.WriteLine("3) Cambiar el estado de un pedido.");
            Console.WriteLine("4) Reasignar el cadete en un pedido.");
            Console.WriteLine("5) Salir del programa.");
            Console.Write("\nOpción: ");

            // Leer la opción seleccionada por el usuario desde la consola.
            var strSeleccion = Console.ReadLine() ?? string.Empty;

            try
            {
                // Validar si la entrada del usuario es un número entero válido.
                if (!int.TryParse(strSeleccion, out opcionSeleccionada))
                {
                    // Si no es un número entero, se lanza una excepción.
                    Console.WriteLine("\nDebe ingresar un número entero");
                }
                else if (opcionSeleccionada < 1 || opcionSeleccionada > opcionSalida)
                {
                    // Si el número está fuera del rango de opciones válidas, se lanza una excepción.
                    Console.WriteLine("\nDebe ingresar una opción válida");
                }
                else
                {
                    // Ejecutar la acción correspondiente según la opción seleccionada.
                    switch (opcionSeleccionada)
                    {
                        case 1:
                            // Opción 1: Dar de alta un nuevo pedido.
                            System.Console.WriteLine("\n\n*** INGRESANDO NUEVO PEDIDO ***\n");
                            var cliente = SolicitarDatosCliente(); // Solicita los datos del cliente.
                            var pedidoA = SolicitarDatosPedido(cliente); // Solicita los datos del pedido.
                            cadeteria.TomarPedido(pedidoA); // Agrega el pedido a la cadetería.
                            MostrarResultadoExitoso(
                                $"Pedido generado con éxito (NRO.: {pedidoA.Numero} - Cliente: {cliente.Nombre})"
                            );
                            break;

                        case 2:
                            // Opción 2: Asignar un pedido a un cadete.
                            if (!cadeteria.ListadoCadetes.Any())
                                Console.WriteLine("\nNo hay cadetes a los cuales asignarles pedidos");
                            if (!cadeteria.ListadoPedidos.Any())
                                Console.WriteLine("\nNo hay pedidos sin asignar");

                            System.Console.WriteLine("\n\n*** ASIGNANDO UN PEDIDO ***\n");
                            var pedidoB = SolicitarSeleccionPedido(cadeteria.ListadoPedidos); // Solicita la selección de un pedido de la lista.
                            System.Console.WriteLine();
                            var cadete = SolicitarSeleccionCadete(); // Solicita la selección de un cadete de la lista.
                            cadeteria.AsignarCadeteAPedido(cadete, pedidoB); // Asigna el pedido seleccionado al cadete seleccionado.
                            MostrarResultadoExitoso(
                                $"El pedido nro. {pedidoB.Numero} ha sido asignado al cadete {cadete.Nombre} ({cadete.Id})"
                            );
                            break;

                        case 3:
                            // Opción 3: Cambiar el estado de un pedido.
                            var pedidos = cadeteria.ObtenerTodosLosPedidos(); // Obtiene todos los pedidos.
                            if (!pedidos.Any())
                                Console.WriteLine("No hay pedidos a los cuales modificarles el estado");

                            System.Console.WriteLine("\n\n*** MODIFICANDO ESTADO DE UN PEDIDO ***\n");

                            var pedidoC = SolicitarSeleccionPedido(pedidos); // Solicita la selección de un pedido.
                            var nuevoEstado = SolicitarSeleccionEstado(); // Solicita la selección de un nuevo estado para el pedido.
                            pedidoC.Estado = nuevoEstado; // Asigna el nuevo estado al pedido.

                            MostrarResultadoExitoso($"El estado del pedido nro. {pedidoC.Numero} ha sido modificado a: {nuevoEstado}");
                            break;

                        case 4:
                            // Opción 4: Reasignar el cade  te en un pedido.
                            var pedidosAsignados = cadeteria.ObtenerPedidosAsignados(); // Obtiene todos los pedidos que ya han sido asignados a cadetes.
                            if (!pedidosAsignados.Any())
                                Console.WriteLine("No hay pedidos para reasignar");

                            Console.WriteLine("\n\n*** REASIGNANDO UN PEDIDO ***\n");

                            var pedidoD = SolicitarSeleccionPedido(pedidosAsignados); // Solicita la selección de un pedido asignado.
                            Console.WriteLine();
                            var cadeteB = SolicitarSeleccionCadete(); // Solicita la selección de un cadete para reasignar el pedido.
                            cadeteria.AsignarCadeteAPedido(cadeteB, pedidoD); // Reasigna el pedido al nuevo cadete seleccionado.
                            MostrarResultadoExitoso($"El pedido nro. {pedidoD.Numero} ha sido re-asignado al cadete {cadeteB.Nombre} ({cadeteB.Id})");
                            break;

                        default:
                            // Opción por defecto: salir del programa.
                            Console.WriteLine("\nSaliendo...");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre algún problema durante la ejecución de una opción.
                MostrarError(ex.Message);
            }
        } while (opcionSeleccionada != opcionSalida); // El bucle continúa hasta que el usuario selecciona la opción de salida.

        // Después de salir del bucle, se muestran los informes finales.
        Console.WriteLine("\n\n\n*** INFORME ***\n");

        // Mostrar el número de envíos realizados por cada cadete.
        Console.WriteLine("* Envíos de cada cadete:");
        int totalEnvios = 0;
        foreach (var cadete in cadeteria.ListadoCadetes)
        {
            var cantidadPedidosCompletados = cadeteria.ObtenerPedidos(cadete.Id, Estado.COMPLETADO).Count();
            var cantidadPedidosPendientes = cadeteria.ObtenerPedidos(cadete.Id, Estado.PENDIENTE).Count();

            // Contar los envíos completados y pendientes por cada cadete.
            Console.WriteLine($"\t> CADETE ID {cadete.Id} ({cadete.Nombre}) - Envíos terminados: {cantidadPedidosCompletados} - Envíos pendientes: {cantidadPedidosPendientes}");

            totalEnvios += cantidadPedidosCompletados + cantidadPedidosPendientes; // Sumar el total de pedidos de cada cadete.
        }
        System.Console.WriteLine($"\n* Envíos totales del día: {totalEnvios}");

        // Calcular y mostrar el promedio de envíos por cadete.
        System.Console.WriteLine(
            $"* Promedio de envíos por cadete: {cadeteria.ListadoCadetes.Select(c => c.Pedidos.Count()).Average()}"
        );
    }

    private static Cadeteria CrearCadeteria()
    {
        // Leer los datos del archivo CSV "datos_cadeteria.csv".
        var datosCsv = LeerCsv("datos_cadeteria.csv");

        // Dividir la primera línea del CSV en datos separados por comas.
        var datos = datosCsv[0].Split(",");

        // Verificar que haya al menos dos datos en la línea (nombre y teléfono).
        if (datos.Count() < 2)
            throw new Exception("No hay datos suficientes para instanciar la cadeteria");

        // Crear una nueva instancia de Cadeteria usando el nombre y teléfono obtenidos.
        return new Cadeteria(datos[0], datos[1]);
    }

    private static void CargarCadetes(Cadeteria cadeteria)
    {
        // Leer los datos del archivo CSV "datos_cadete.csv".
        var datosCsv = LeerCsv("datos_cadete.csv");

        // Iterar sobre cada línea del archivo CSV.
        foreach (var linea in datosCsv)
        {
            // Dividir la línea en datos separados por comas.
            var datos = linea.Split(",");

            // Verificar que haya al menos 4 datos (id, nombre, dirección, teléfono).
            if (datos.Count() < 4)
            {
                // Si no hay suficientes datos, mostrar un mensaje de advertencia y continuar con la siguiente línea.
                System.Console.WriteLine($"\n[!] No se pudo cargar el cadete: {linea} - {datos}");
                continue;
            }

            // Crear una nueva instancia de Cadete y agregarla a la cadetería.
            cadeteria.AltaCadete(new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]));
        }
    }

    private static List<string> LeerCsv(string nombreArchivo, bool tieneCabecera = true)
    {
        var lineas = new List<string>();

        // Abrir el archivo CSV para lectura.
        using (FileStream archivoCsv = new FileStream(nombreArchivo, FileMode.Open))
        {
            // Crear un StreamReader para leer el contenido del archivo.
            using (StreamReader readerCsv = new StreamReader(archivoCsv))
            {
                // Si el archivo tiene una cabecera (especificada por el parámetro `tieneCabecera`),
                // leer y descartar la primera línea del archivo.
                if (tieneCabecera)
                    readerCsv.ReadLine();

                // Leer línea por línea hasta que no haya más contenido en el archivo.
                while (readerCsv.Peek() != -1)
                {
                    // Leer una línea del archivo.
                    var linea = readerCsv.ReadLine();

                    // Verificar que la línea no esté vacía o contenga solo espacios en blanco.
                    // Si es válida, agregarla a la lista `lineas`.
                    if (!string.IsNullOrWhiteSpace(linea))
                        lineas.Add(linea);
                }
            }
        }

        // Devolver la lista de líneas leídas desde el archivo CSV.
        return lineas;
    }

    private static void MostrarError(string error)
    {
        // Mostrar el mensaje de error en la consola.
        Console.WriteLine($"\nError: {error}\n");
    }

    private static void MostrarResultadoExitoso(string mensaje)
    {    
        // Mostrar el mensaje de éxito en la consola.
        Console.WriteLine($"\n {mensaje}\n");
    }

    private static Cliente SolicitarDatosCliente()
    {
        // Solicitar el DNI del cliente.
        System.Console.Write("> Ingrese el DNI del cliente (sin puntos ni espacios): ");
        var dni = 0;
        var strDni = Console.ReadLine() ?? string.Empty;

        // Validar que el DNI no esté vacío y sea un número.
        if (string.IsNullOrWhiteSpace(strDni))
            throw new Exception("El DNI no puede estar vacío");
        if (!int.TryParse(strDni, out dni))
            throw new Exception("El DNI debe ser un número");

        // Solicitar el nombre del cliente.
        System.Console.Write("> Ingrese el nombre del cliente: ");
        var nombre = Console.ReadLine() ?? string.Empty;

        // Validar que el nombre no esté vacío.
        if (string.IsNullOrWhiteSpace(nombre))
            throw new Exception("El nombre no puede estar vacío");

        // Solicitar el teléfono del cliente.
        System.Console.Write("> Ingrese el teléfono del cliente: ");
        var telefono = Console.ReadLine() ?? string.Empty;

        // Validar que el teléfono no esté vacío.
        if (string.IsNullOrWhiteSpace(telefono))
            throw new Exception("El teléfono no puede estar vacío");

        // Solicitar la dirección del cliente.
        System.Console.Write("> Ingrese la dirección del cliente: ");
        var direccion = Console.ReadLine() ?? string.Empty;

        // Validar que la dirección no esté vacía.
        if (string.IsNullOrWhiteSpace(direccion))
            throw new Exception("La dirección no puede estar vacía");

        // Solicitar datos adicionales o referencias de la dirección (opcional).
        System.Console.Write(
            "> Ingrese datos o referencias de la dirección del cliente (opcional): "
        );
        var datosReferencia = Console.ReadLine() ?? string.Empty;

        // Crear y devolver una nueva instancia de la clase Cliente con los datos ingresados.
        return new Cliente(dni, nombre, direccion, telefono, datosReferencia);
    }

    private static Pedido SolicitarDatosPedido(Cliente cliente)
    {
        // Solicitar al usuario que ingrese los detalles del pedido.
        System.Console.Write("> Ingrese los detalles del pedido (obligatorio): ");
        var detalles = Console.ReadLine() ?? string.Empty;

        // Verificar que los detalles no estén vacíos.
        if (string.IsNullOrWhiteSpace(detalles))
            throw new Exception("Debe incluir los detalles del pedido");

        // Crear y devolver una nueva instancia de Pedido con los detalles y el cliente proporcionado.
        return new Pedido(detalles, cliente);
    }

    private static Pedido SolicitarSeleccionPedido(List<Pedido> pedidos)
    {
        // Convertir cada pedido en una cadena de texto y mostrarlo en la consola.
        var detallesPedidos = pedidos.Select(pedido => pedido.ToString());
        foreach (var detallePedido in detallesPedidos)
        {
            Console.WriteLine($"\t* {detallePedido}");
        }

        // Solicitar al usuario que ingrese el número del pedido que desea seleccionar.
        System.Console.Write("\n> Ingrese el número del pedido a asignar: ");
        var strNro = Console.ReadLine() ?? string.Empty;
        var nroPedido = 0;

        // Verificar que el número ingresado sea un número entero.
        if (!int.TryParse(strNro, out nroPedido))
            throw new Exception("Debe ingresar un número entero para seleccionar el pedido");

        // Buscar el pedido que coincida con el número ingresado.
        var pedidoSeleccionado = pedidos.Where(p => p.Numero == nroPedido).FirstOrDefault();

        // Si no se encuentra el pedido, lanzar una excepción.
        if (pedidoSeleccionado == null)
            throw new Exception($"El número de pedido ingresado es inválido ({nroPedido})");

        // Devolver el pedido seleccionado.
        return pedidoSeleccionado;
    }

    private static Cadete SolicitarSeleccionCadete()
    {
        // Convertir cada cadete en una cadena de texto y mostrarlo en la consola.
        var detallesCadetes = cadeteria.ListadoCadetes.Select(cadete => cadete.ToString());
        foreach (var detalle in detallesCadetes)
        {
            System.Console.WriteLine($"\t* {detalle}");
        }

        // Solicitar al usuario que ingrese el ID del cadete al cual se le asignará el pedido.
        System.Console.Write("\n> Ingrese el ID del cadete al cual asignarle el pedido: ");
        var strId = Console.ReadLine() ?? string.Empty;
        var id = 0;

        // Verificar que el ID ingresado sea un número entero.
        if (!int.TryParse(strId, out id))
            throw new Exception("El ID debe ser un número");

        // Buscar el cadete que coincida con el ID ingresado.
        var cadeteSeleccionado = cadeteria
            .ListadoCadetes.Where(cadete => cadete.Id == id)
            .FirstOrDefault();

        // Si no se encuentra un cadete con el ID proporcionado, lanzar una excepción.
        if (cadeteSeleccionado == null)
            throw new Exception($"No existe ningún cadete con el ID {id}");

        // Devolver el cadete seleccionado.
        return cadeteSeleccionado;
    }

    private static Estado SolicitarSeleccionEstado()
    {
        int contador = 0;

        // Mostrar todos los valores posibles del enum Estado en la consola.
        foreach (var estado in Enum.GetValues(typeof(Estado)))
        {
            System.Console.WriteLine($"> ID {++contador}. {estado}");
        }

        // Solicitar al usuario que seleccione el ID del nuevo estado para el pedido.
        System.Console.Write("> Selecciona el ID del nuevo estado para el pedido: ");
        var strOpcion = Console.ReadLine() ?? string.Empty;

        // Intentar convertir el ID ingresado en un valor del enum Estado.
        Estado nuevoEstado;
        if (!Enum.TryParse(strOpcion, out nuevoEstado))
            throw new Exception("Seleccione un ID válido");

        // Devolver el nuevo estado seleccionado.
        return nuevoEstado;
    }
}
