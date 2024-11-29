using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa a un usuario en el sistema.
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Obtiene o establece el nombre del usuario.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Obtiene o establece el apellido del usuario.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Obtiene o establece el correo electrónico del usuario.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Obtiene o establece la contraseña del usuario.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de reservas asociadas al usuario.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    /// <summary>
    /// Sobrecarga del método Equals para comparar objetos User por su identificador único.
    /// </summary>
    /// <param name="obj">El objeto a comparar con el usuario actual.</param>
    /// <returns>True si los objetos son iguales, de lo contrario, false.</returns>
    public override bool Equals(object? obj)
    {
        // Verifica si el objeto proporcionado no es nulo y es del mismo tipo que User
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        // Convierte el objeto a tipo User
        User other = (User)obj;

        // Compara los identificadores únicos para determinar la igualdad
        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Obtiene un código hash para el objeto actual.
    /// El código hash se basa en el identificador único (Id) del objeto User.
    /// </summary>
    /// <returns>Un código hash para el objeto actual.</returns>
    public override int GetHashCode()
    {
        // Retorna el código hash del identificador único (Id)
        return Id.GetHashCode();
    }
}
