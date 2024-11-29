namespace CoWorkingApp.Core.Primitives;

/// <summary>
/// Clase base abstracta para todas las entidades del sistema.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Obtiene o establece el identificador único de la entidad.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Constructor de la clase EntityBase.
    /// </summary>
    public EntityBase()
    {
        // Genera un nuevo identificador único al crear una instancia de la entidad
        Id = Guid.NewGuid();
    }
}
