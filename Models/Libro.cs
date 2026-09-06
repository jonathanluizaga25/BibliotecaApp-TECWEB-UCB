namespace BibliotecaAppTECWEB.Models;

public class Libro
{
    public string Titulo {get; set;}
    public string Autor { get; set;}
    public string Categoria {get; set;}
    public string Codigo {get; set;}
    public bool Disponible {get; set;}
   


    public Libro(string titulo ,string autor , string categoria,string codigo)
    {
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        Codigo = codigo;
        Disponible = true;

    }
}
