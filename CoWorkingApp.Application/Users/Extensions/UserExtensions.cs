using CoWorkingApp.Application.Users.Commands.AuthenticateUser;
using CoWorkingApp.Application.Users.Commands.CreateUser;
using CoWorkingApp.Application.Users.Commands.DeleteUser;
using CoWorkingApp.Application.Users.Commands.UpdateUser;
using CoWorkingApp.Application.Users.DTOs;
using CoWorkingApp.Application.Users.Queries.GetUserByEmail;
using CoWorkingApp.Application.Users.Queries.GetUserById;
using CoWorkingApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace CoWorkingApp.Application.Users.Extensions;

/// <summary>
/// Define métodos de extensión para la entidad <see cref="User"/>.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="UserResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="UserResponse"/> con los datos del usuario.</returns>
    public static UserResponse ToUserResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="AuthenticateUserCommandResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <param name="token">El token de autenticación.</param>
    /// <returns>Una instancia de <see cref="AuthenticateUserCommandResponse"/> con los datos del usuario y el token.</returns>
    public static AuthenticateUserCommandResponse ToAuthenticateUserCommandResponse(this User user, JsonResult token) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email,
            token);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="CreateUserCommandResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="CreateUserCommandResponse"/> con los datos del usuario.</returns>
    public static CreateUserCommandResponse ToCreateUserCommandResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="DeleteUserCommandResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="DeleteUserCommandResponse"/> con los datos del usuario.</returns>
    public static DeleteUserCommandResponse ToDeleteUserCommandResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="UpdateUserCommandResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="UpdateUserCommandResponse"/> con los datos del usuario.</returns>
    public static UpdateUserCommandResponse ToUpdateUserCommandResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="GetUserByEmailQueryResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetUserByEmailQueryResponse"/> con los datos del usuario.</returns>
    public static GetUserByEmailQueryResponse ToGetUserByEmailQueryResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);

    /// <summary>
    /// Convierte un objeto <see cref="User"/> a <see cref="GetUserByIdQueryResponse"/>.
    /// </summary>
    /// <param name="user">El objeto <see cref="User"/> que se va a convertir.</param>
    /// <returns>Una instancia de <see cref="GetUserByIdQueryResponse"/> con los datos del usuario.</returns>
    public static GetUserByIdQueryResponse ToGetUserByIdQueryResponse(this User user) =>
        new(user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Credentials.Email);
}
