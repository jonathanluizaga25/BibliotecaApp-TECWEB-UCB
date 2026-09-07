using BibliotecaAppTECWEB.Models;

namespace BibliotecaAppTECWEB.Services;

public class BibliotecaService
{
    private readonly List<Libro> libros = new();
    private readonly List<Usuario> usuarios = new();
    private readonly List<Prestamo> prestamos = new();

    private int siguientePrestamoId = 1;

    ////METODOS
    /// 
    /// 
    /// 
    
    public void RegistrarLibros(

        string titulo, string autor, string categoria,
        string codigo
    )
    {
        Libro nuevoLibro = new Libro( titulo , autor,categoria,codigo);

        libros.Add(nuevoLibro);
    }

    public void RegistrarUsuario(
        int id , string nombre , string correo
    )
    {
        Usuario nuevoUsuario = new Usuario
        (id,nombre,correo);

        usuarios.Add(nuevoUsuario);
    }

    public List<Libro> ObtenerLibros()
    {
        return libros;
    }
    /// LINQ
    public List<Libro> ObtenerLibrosOrdenados()
    {
    return libros
        .OrderBy(l => l.Titulo)
        .ToList();
    }

public Libro? BuscarLibroPorCodigo(string codigo)
{
    return libros.FirstOrDefault(
        l => l.Codigo.Equals(
            codigo,
            StringComparison.OrdinalIgnoreCase
        )
    );
}


public List<Libro> ObtenerLibrosDisponibles()
{
    return libros
        .Where(l => l.Disponible)
        .ToList();
}



}