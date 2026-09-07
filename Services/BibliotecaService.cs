using BibliotecaAppTECWEB.Models;

namespace BibliotecaAppTECWEB.Services;

public class BibliotecaService
{
    // ARRAY PARA DATOS FIJOS
    private readonly string[] categoriasDisponibles =
    {
        "Novela",
        "Ciencia",
        "Historia",
        "Tecnología",
        "Literatura"
    };

    // LISTAS
    private readonly List<Libro> libros = new();
    private readonly List<Usuario> usuarios = new();
    private readonly List<Prestamo> prestamos = new();

    private int siguientePrestamoId = 1;


    // ==========================================
    // REGISTRAR LIBRO
    // ==========================================

    public void RegistrarLibro(
        string titulo,
        string autor,
        string categoria,
        string codigo)
    {
        Libro? existente = libros.FirstOrDefault(
            l => l.Codigo.Equals(
                codigo,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (existente != null)
        {
            throw new InvalidOperationException(
                "Ya existe un libro con ese código."
            );
        }

        Libro nuevoLibro = new Libro(
            titulo,
            autor,
            categoria,
            codigo
        );

        libros.Add(nuevoLibro);
    }


    // ==========================================
    // REGISTRAR USUARIO
    // ==========================================

    public void RegistrarUsuario(
        int id,
        string nombre,
        string correo)
    {
        Usuario? existente = usuarios.FirstOrDefault(
            u => u.Id == id
        );

        if (existente != null)
        {
            throw new InvalidOperationException(
                "Ya existe un usuario con ese identificador."
            );
        }

        Usuario nuevoUsuario = new Usuario(
            id,
            nombre,
            correo
        );

        usuarios.Add(nuevoUsuario);
    }


    // ==========================================
    // OBTENER CATEGORÍAS
    // ==========================================

    public string[] ObtenerCategorias()
    {
        return categoriasDisponibles;
    }


    // ==========================================
    // OBTENER TODOS LOS LIBROS
    // ==========================================

    public List<Libro> ObtenerLibros()
    {
        return libros;
    }


    // ==========================================
    // LINQ - ORDENAR LIBROS POR TÍTULO
    // ==========================================

    public List<Libro> ObtenerLibrosOrdenados()
    {
        return libros
            .OrderBy(l => l.Titulo)
            .ToList();
    }


    // ==========================================
    // LINQ - BUSCAR LIBRO POR CÓDIGO
    // ==========================================

    public Libro? BuscarLibroPorCodigo(string codigo)
    {
        return libros.FirstOrDefault(
            l => l.Codigo.Equals(
                codigo,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }


    // ==========================================
    // LINQ - LIBROS DISPONIBLES
    // ==========================================

    public List<Libro> ObtenerLibrosDisponibles()
    {
        return libros
            .Where(l => l.Disponible)
            .ToList();
    }


    // ==========================================
    // LINQ - BUSCAR POR AUTOR
    // ==========================================

    public List<Libro> BuscarPorAutor(string autor)
    {
        return libros
            .Where(l => l.Autor.Contains(
                autor,
                StringComparison.OrdinalIgnoreCase
            ))
            .ToList();
    }


    // ==========================================
    // LINQ - BUSCAR POR CATEGORÍA
    // ==========================================

    public List<Libro> BuscarPorCategoria(string categoria)
    {
        return libros
            .Where(l => l.Categoria.Contains(
                categoria,
                StringComparison.OrdinalIgnoreCase
            ))
            .ToList();
    }


    // ==========================================
    // ELIMINAR LIBRO
    // ==========================================

    public void EliminarLibro(string codigo)
    {
        Libro? libro = BuscarLibroPorCodigo(codigo);

        if (libro == null)
        {
            throw new InvalidOperationException(
                "El libro no existe."
            );
        }

        libros.Remove(libro);
    }


    // ==========================================
    // PRESTAR LIBRO
    // ==========================================

    public void PrestarLibro(
        string codigoLibro,
        int idUsuario)
    {
        Libro? libro = BuscarLibroPorCodigo(codigoLibro);

        if (libro == null)
        {
            throw new InvalidOperationException(
                "El libro no existe."
            );
        }

        if (!libro.Disponible)
        {
            throw new InvalidOperationException(
                "El libro no está disponible."
            );
        }

        Usuario? usuario = usuarios.FirstOrDefault(
            u => u.Id == idUsuario
        );

        if (usuario == null)
        {
            throw new InvalidOperationException(
                "El usuario no existe."
            );
        }

        Prestamo nuevoPrestamo = new Prestamo(
            siguientePrestamoId,
            libro.Codigo,
            usuario.Id,
            DateTime.Now,
            null
        );

        prestamos.Add(nuevoPrestamo);

        libro.Disponible = false;

        siguientePrestamoId++;
    }


    // ==========================================
    // DEVOLVER LIBRO
    // ==========================================

    public void DevolverLibro(int idPrestamo)
    {
        int indice = prestamos.FindIndex(
            p => p.Id == idPrestamo &&
                 p.FechaDevolucion == null
        );

        if (indice == -1)
        {
            throw new InvalidOperationException(
                "No existe un préstamo activo con ese ID."
            );
        }

        Prestamo prestamo = prestamos[indice];

        prestamos[indice] = prestamo with
        {
            FechaDevolucion = DateTime.Now
        };

        Libro? libro = BuscarLibroPorCodigo(
            prestamo.CodigoLibro
        );

        if (libro != null)
        {
            libro.Disponible = true;
        }
    }


    // ==========================================
    // LINQ - PRÉSTAMOS ACTIVOS
    // ==========================================

    public List<Prestamo> ObtenerPrestamosActivos()
    {
        return prestamos
            .Where(p => p.FechaDevolucion == null)
            .ToList();
    }


    // ==========================================
    // LINQ - SELECT DE PRÉSTAMOS ACTIVOS
    // ==========================================

    public IEnumerable<object> ObtenerDatosPrestamosActivos()
    {
        return prestamos
            .Where(p => p.FechaDevolucion == null)
            .Select(p => new
            {
                p.Id,
                p.CodigoLibro,
                p.IdUsuario,
                p.FechaPrestamo
            });
    }
}