using CoWorkingApp.Application.Abstracts.Messaging;
using CoWorkingApp.Application.Contracts;
using CoWorkingApp.Core.Extensions;

namespace CoWorkingApp.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// Consulta para obtener un usuario por su correo electrónico.
/// </summary>
/// <param name="Email">El correo electrónico del usuario.</param>
public record struct GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailResponse>, IInputFilter
{
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
