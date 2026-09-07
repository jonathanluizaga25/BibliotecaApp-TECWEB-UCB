namespace BibliotecaAppTECWEB.Interfaces;

public interface IRepositorio<T>
{
    void Agregar(T elemento);
    List<T> ObtenerTodos();
}