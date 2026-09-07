
namespace BibliotecaAppTECWEB.Models;

 public record Prestamo
(
    int Id,
    string CodigoLibro,
    int IdUsuario,
    DateTime FechaPrestamo,
    DateTime? FechaDevolucion
);