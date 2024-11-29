using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa un asiento en el sistema.
/// </summary>
public class Seat : EntityBase
{
    /// <summary>
    /// Obtiene o establece el nombre del asiento.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si el asiento está bloqueado.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Obtiene o establece la descripción del asiento.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de reservas asociadas al asiento.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = new();

    /// <summary>
    /// Determina si el objeto actual es igual a otro objeto.
    /// Dos objetos de tipo Seat se consideran iguales si tienen el mismo identificador único (Id).
    /// </summary>
    /// <param name="obj">El objeto que se va a comparar con el objeto actual.</param>
    /// <returns>True si el objeto actual es igual al parámetro obj; de lo contrario, false.</returns>
    public override bool Equals(object? obj)
    {
        // Verifica si el objeto proporcionado no es nulo y es del mismo tipo que Seat
        if (obj is null || GetType() != obj.GetType())
        {
            // Si el objeto proporcionado es nulo o no es del mismo tipo que Seat, retorna false
            return false;
        }

        // Convierte el objeto a tipo Seat
        Seat other = (Seat)obj;

        // Compara los identificadores únicos para determinar la igualdad
        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Obtiene un código hash para el objeto actual.
    /// El código hash se basa en el identificador único (Id) del objeto Seat.
    /// </summary>
    /// <returns>Un código hash para el objeto actual.</returns>
    public override int GetHashCode()
    {
        // Retorna el código hash del identificador único (Id)
        return Id.GetHashCode();
    }
}
