namespace CoWorkingApp.Application.Contracts;

/// <summary>
/// Interfaz para solicitudes que se pueden filtrar y normalizar.
/// </summary>
public interface IInputFilter
{
    /// <summary>
    /// Filtra y normaliza los valores de la solicitud.
    /// </summary>
    void Filter();
}
