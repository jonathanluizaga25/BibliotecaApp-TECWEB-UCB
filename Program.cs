using BibliotecaAppTECWEB.Models;
using BibliotecaAppTECWEB.Services;

BibliotecaService biblioteca = new BibliotecaService();

bool salir = false;

while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("====================================");
    Console.WriteLine("      SISTEMA DE BIBLIOTECA");
    Console.WriteLine("====================================");
    Console.WriteLine("1. Registrar libro");
    Console.WriteLine("2. Registrar usuario");
    Console.WriteLine("3. Listar libros");
    Console.WriteLine("4. Buscar libro por código");
    Console.WriteLine("5. Eliminar libro");
    Console.WriteLine("6. Prestar libro");
    Console.WriteLine("7. Devolver libro");
    Console.WriteLine("8. Ver libros disponibles");
    Console.WriteLine("9. Buscar libros por autor");
    Console.WriteLine("10. Buscar libros por categoría");
    Console.WriteLine("11. Ver préstamos activos");
    Console.WriteLine("0. Salir");
    Console.WriteLine("====================================");

    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                RegistrarLibro();
                break;

            case "2":
                RegistrarUsuario();
                break;

            case "3":
                ListarLibros();
                break;

            case "4":
                BuscarLibro();
                break;

            case "5":
                EliminarLibro();
                break;

            case "6":
                PrestarLibro();
                break;

            case "7":
                DevolverLibro();
                break;

            case "8":
                MostrarLibrosDisponibles();
                break;

            case "9":
                BuscarPorAutor();
                break;

            case "10":
                BuscarPorCategoria();
                break;

            case "11":
                MostrarPrestamosActivos();
                break;

            case "0":
                salir = true;
                Console.WriteLine("Programa finalizado.");
                break;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


// ==========================================
// REGISTRAR LIBRO
// ==========================================

void RegistrarLibro()
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine() ?? "";

    Console.Write("Autor: ");
    string autor = Console.ReadLine() ?? "";

    Console.WriteLine();
    Console.WriteLine("Categorías disponibles:");

    string[] categorias = biblioteca.ObtenerCategorias();

    for (int i = 0; i < categorias.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {categorias[i]}");
    }

    Console.Write("Seleccione una categoría: ");
    string? entradaCategoria = Console.ReadLine();

    if (!int.TryParse(entradaCategoria, out int numeroCategoria))
    {
        Console.WriteLine("Debe ingresar un número.");
        return;
    }

    if (numeroCategoria < 1 || numeroCategoria > categorias.Length)
    {
        Console.WriteLine("Categoría no válida.");
        return;
    }

    string categoria = categorias[numeroCategoria - 1];

    Console.Write("Código: ");
    string codigo = Console.ReadLine() ?? "";

    biblioteca.RegistrarLibro(
        titulo,
        autor,
        categoria,
        codigo
    );

    Console.WriteLine("Libro registrado correctamente.");
}


// ==========================================
// REGISTRAR USUARIO
// ==========================================

void RegistrarUsuario()
{
    Console.Write("ID del usuario: ");
    string? entradaId = Console.ReadLine();

    if (!int.TryParse(entradaId, out int id))
    {
        Console.WriteLine("El ID debe ser un número.");
        return;
    }

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Correo: ");
    string correo = Console.ReadLine() ?? "";

    biblioteca.RegistrarUsuario(
        id,
        nombre,
        correo
    );

    Console.WriteLine("Usuario registrado correctamente.");
}


// ==========================================
// LISTAR LIBROS
// ==========================================

void ListarLibros()
{
    List<Libro> libros = biblioteca.ObtenerLibrosOrdenados();

    if (libros.Count == 0)
    {
        Console.WriteLine("No hay libros registrados.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("===== LIBROS =====");

    foreach (Libro libro in libros)
    {
        MostrarLibro(libro);
    }
}


// ==========================================
// BUSCAR LIBRO POR CÓDIGO
// ==========================================

void BuscarLibro()
{
    Console.Write("Ingrese el código del libro: ");
    string codigo = Console.ReadLine() ?? "";

    Libro? libro = biblioteca.BuscarLibroPorCodigo(codigo);

    if (libro == null)
    {
        Console.WriteLine("No se encontró ningún libro.");
    }
    else
    {
        MostrarLibro(libro);
    }
}


// ==========================================
// ELIMINAR LIBRO
// ==========================================

void EliminarLibro()
{
    Console.Write("Código del libro a eliminar: ");
    string codigo = Console.ReadLine() ?? "";

    biblioteca.EliminarLibro(codigo);

    Console.WriteLine("Libro eliminado correctamente.");
}


// ==========================================
// PRESTAR LIBRO
// ==========================================

void PrestarLibro()
{
    Console.Write("Código del libro: ");
    string codigo = Console.ReadLine() ?? "";

    Console.Write("ID del usuario: ");
    string? entradaId = Console.ReadLine();

    if (!int.TryParse(entradaId, out int idUsuario))
    {
        Console.WriteLine("El ID debe ser un número.");
        return;
    }

    biblioteca.PrestarLibro(
        codigo,
        idUsuario
    );

    Console.WriteLine("Préstamo registrado correctamente.");
}


// ==========================================
// DEVOLVER LIBRO
// ==========================================

void DevolverLibro()
{
    Console.Write("ID del préstamo: ");
    string? entradaId = Console.ReadLine();

    if (!int.TryParse(entradaId, out int idPrestamo))
    {
        Console.WriteLine("El ID del préstamo debe ser un número.");
        return;
    }

    biblioteca.DevolverLibro(idPrestamo);

    Console.WriteLine("Libro devuelto correctamente.");
}


// ==========================================
// MOSTRAR LIBROS DISPONIBLES
// ==========================================

void MostrarLibrosDisponibles()
{
    List<Libro> libros = biblioteca.ObtenerLibrosDisponibles();

    if (libros.Count == 0)
    {
        Console.WriteLine("No hay libros disponibles.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("===== LIBROS DISPONIBLES =====");

    foreach (Libro libro in libros)
    {
        MostrarLibro(libro);
    }
}


// ==========================================
// BUSCAR POR AUTOR
// ==========================================

void BuscarPorAutor()
{
    Console.Write("Ingrese el autor: ");
    string autor = Console.ReadLine() ?? "";

    List<Libro> libros = biblioteca.BuscarPorAutor(autor);

    if (libros.Count == 0)
    {
        Console.WriteLine(
            "No se encontraron libros de ese autor."
        );

        return;
    }

    foreach (Libro libro in libros)
    {
        MostrarLibro(libro);
    }
}


// ==========================================
// BUSCAR POR CATEGORÍA
// ==========================================

void BuscarPorCategoria()
{
    Console.WriteLine();
    Console.WriteLine("Categorías disponibles:");

    string[] categorias = biblioteca.ObtenerCategorias();

    for (int i = 0; i < categorias.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {categorias[i]}");
    }

    Console.Write("Seleccione una categoría: ");

    string? entradaCategoria = Console.ReadLine();

    if (!int.TryParse(
        entradaCategoria,
        out int numeroCategoria))
    {
        Console.WriteLine("Debe ingresar un número.");
        return;
    }

    if (
        numeroCategoria < 1 ||
        numeroCategoria > categorias.Length
    )
    {
        Console.WriteLine("Categoría no válida.");
        return;
    }

    string categoria =
        categorias[numeroCategoria - 1];

    List<Libro> libros =
        biblioteca.BuscarPorCategoria(categoria);

    if (libros.Count == 0)
    {
        Console.WriteLine(
            "No se encontraron libros en esa categoría."
        );

        return;
    }

    foreach (Libro libro in libros)
    {
        MostrarLibro(libro);
    }
}


// ==========================================
// MOSTRAR PRÉSTAMOS ACTIVOS
// ==========================================

void MostrarPrestamosActivos()
{
    List<Prestamo> prestamos =
        biblioteca.ObtenerPrestamosActivos();

    if (prestamos.Count == 0)
    {
        Console.WriteLine(
            "No hay préstamos activos."
        );

        return;
    }

    Console.WriteLine();
    Console.WriteLine("===== PRÉSTAMOS ACTIVOS =====");

    foreach (Prestamo prestamo in prestamos)
    {
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"ID préstamo: {prestamo.Id}");
        Console.WriteLine(
            $"Código libro: {prestamo.CodigoLibro}"
        );
        Console.WriteLine(
            $"ID usuario: {prestamo.IdUsuario}"
        );
        Console.WriteLine(
            $"Fecha: {prestamo.FechaPrestamo}"
        );
    }
}


// ==========================================
// MOSTRAR DATOS DE UN LIBRO
// ==========================================

void MostrarLibro(Libro libro)
{
    Console.WriteLine("-----------------------------");
    Console.WriteLine($"Código: {libro.Codigo}");
    Console.WriteLine($"Título: {libro.Titulo}");
    Console.WriteLine($"Autor: {libro.Autor}");
    Console.WriteLine($"Categoría: {libro.Categoria}");

    if (libro.Disponible)
    {
        Console.WriteLine("Estado: Disponible");
    }
    else
    {
        Console.WriteLine("Estado: Prestado");
    }
}