using CoWorkingApp.Core.Entities;
using CoWorkingApp.Core.Primitives;

namespace CoWorkingApp.Tests.Core.Entities.Users;

/// <summary>
/// Clase de pruebas unitarias para la entidad User.
/// </summary>
public partial class UserTests
{
    public class AnotherEntityType : EntityBase;

    private static User GetCreateValidUser()
    {
        var userId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var email = "valid@example.com";
        var password = "Valid1!";

        return User.Create(userId, firstName, lastName, email, password).Value;
    }

    private static Reservation CreateReservation()
    {
        // Crear y devolver una instancia de reserva válida aquí
        // Este método debería incluir la lógica para crear una reserva válida
        //return new Reservation();
        return null!;
    }
}
