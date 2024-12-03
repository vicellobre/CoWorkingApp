﻿using CoWorkingApp.Core.Primitives;
using CoWorkingApp.Core.Shared;
using CoWorkingApp.Core.ValueObjects.Composite;
using CoWorkingApp.Core.ValueObjects.Single;

namespace CoWorkingApp.Core.Entities;

/// <summary>
/// Representa a un usuario en el sistema.
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Obtiene o establece el nombre completo del usuario.
    /// </summary>
    public FullName Name { get; set; }

    /// <summary>
    /// Obtiene o establece las credenciales del usuario (correo electrónico y contraseña).
    /// </summary>
    public CredentialsWithEmailAndPassword Credentials { get; set; }

    /// <summary>
    /// Obtiene o establece la colección de reservas asociadas al usuario.
    /// </summary>
    public ICollection<Reservation> Reservations { get; set; } = [];

    /// <summary>
    /// Constructor privado para inicializar un usuario con un identificador especificado.
    /// </summary>
    /// <param name="id">El identificador del usuario.</param>
    /// <param name="fullName">El nombre completo del usuario.</param>
    /// <param name="credentials">Las credenciales del usuario.</param>
    private User(Guid id, FullName fullName, CredentialsWithEmailAndPassword credentials) : base(id)
    {
        Name = fullName;
        Credentials = credentials;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="User"/> con los valores especificados.
    /// </summary>
    /// <param name="id">El identificador del usuario.</param>
    /// <param name="firstName">El primer nombre del usuario.</param>
    /// <param name="lastName">El apellido del usuario.</param>
    /// <param name="email">El correo electrónico del usuario.</param>
    /// <param name="password">La contraseña del usuario.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="User"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<User> Create(Guid id, string? firstName, string? lastName, string? email, string? password)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
        {
            return Result<User>.Failure(fullNameResult.FirstError);
        }

        var credentialsResult = CredentialsWithEmailAndPassword.Create(email, password);
        if (credentialsResult.IsFailure)
        {
            return Result<User>.Failure(credentialsResult.FirstError);
        }

        return Result<User>.Success(new User(id, fullNameResult.Value, credentialsResult.Value));
    }

    /// <summary>
    /// Cambia el nombre completo del usuario.
    /// </summary>
    /// <param name="firstName">El nuevo primer nombre del usuario.</param>
    /// <param name="lastName">El nuevo apellido del usuario.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeName(string firstName, string lastName)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure(fullNameResult.FirstError);
        }

        Name = fullNameResult.Value;
        return Result.Success();
    }

    /// <summary>
    /// Cambia el correo electrónico del usuario.
    /// </summary>
    /// <param name="email">El nuevo correo electrónico del usuario.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangeEmail(string email)
    {
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.FirstError);
        }

        var credentialsResult = CredentialsWithEmailAndPassword.Create(emailResult.Value, Credentials.Password.Value);
        if (credentialsResult.IsFailure)
        {
            return Result.Failure(credentialsResult.FirstError);
        }

        Credentials = credentialsResult.Value;
        return Result.Success();
    }

    /// <summary>
    /// Cambia la contraseña del usuario.
    /// </summary>
    /// <param name="password">La nueva contraseña del usuario.</param>
    /// <returns>Un resultado que indica si la operación fue exitosa.</returns>
    public Result ChangePassword(string password)
    {
        var passwordResult = Password.Create(password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.FirstError);
        }

        var credentialsResult = CredentialsWithEmailAndPassword.Create(Credentials.Email.Value, passwordResult.Value);
        if (credentialsResult.IsFailure)
        {
            return Result.Failure(credentialsResult.FirstError);
        }

        Credentials = credentialsResult.Value;
        return Result.Success();
    }

    /// <summary>
    /// Agrega una reserva a la colección de reservas del usuario.
    /// </summary>
    /// <param name="reservation">La reserva a agregar.</param>
    public void AddReservation(Reservation reservation)
    {
        Reservations.Add(reservation);
    }

    /// <summary>
    /// Elimina una reserva de la colección de reservas del usuario.
    /// </summary>
    /// <param name="reservation">La reserva a eliminar.</param>
    public void RemoveReservation(Reservation reservation)
    {
        Reservations.Remove(reservation);
    }
}
