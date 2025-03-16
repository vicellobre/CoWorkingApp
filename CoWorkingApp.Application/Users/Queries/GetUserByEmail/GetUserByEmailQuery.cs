using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Consulta para obtener un usuario por su correo electrónico.
/// </summary>
public record class GetUserByEmailQuery : IQuery<GetUserByEmailQueryResponse>, IInputFilter
{
    /// <summary>
    /// El correo electrónico del usuario.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="GetUserByEmailQuery"/>.
    /// </summary>
    /// <param name="email">El correo electrónico del usuario.</param>
    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }

    /// <summary>
    /// Filtra y normaliza el correo electrónico del usuario.
    /// </summary>
    public void Filter()
    {
        Email = Email
            .GetValueOrDefault(string.Empty)
            .Trim()
            .ToLowerInvariant();
    }
}
