﻿using System.Text.RegularExpressions;
using CoWorkingApp.Core.DomainErrors;
using CoWorkingApp.Core.Extensions;
using CoWorkingApp.Core.Shared;

namespace CoWorkingApp.Core.ValueObjects.Single;

/// <summary>
/// Representa el valor del nombre del usuario.
/// </summary>
public readonly record struct FirstName
{
    /// <summary>
    /// Obtiene el nombre del usuario.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Longitud mínima permitida para el nombre.
    /// </summary>
    public const int MinLength = 2;

    /// <summary>
    /// Longitud máxima permitida para el nombre.
    /// </summary>
    public const int MaxLength = 50;

    /// <summary>
    /// Patrón de expresión regular para validar el formato del nombre.
    /// </summary>
    public const string Pattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑçÇüÜàÀèÈìÌòÒùÙâêÊîôûäëïöüß\s]+$";


    /// <summary>
    /// Constructor privado para inicializar el valor del nombre.
    /// </summary>
    /// <param name="value">El valor del nombre.</param>
    private FirstName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Crea una nueva instancia de <see cref="FirstName"/> con el valor especificado.
    /// </summary>
    /// <param name="value">El valor del nombre.</param>
    /// <returns>Un resultado que contiene una instancia de <see cref="FirstName"/> si es exitoso; de lo contrario, contiene un error.</returns>
    public static Result<FirstName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result<FirstName>.Failure(Errors.FirstName.IsNullOrEmpty);
        }

        List<Error> errors = [];

        if (value.Length < MinLength)
        {
            errors.Add(Errors.FirstName.TooShort(MinLength));
        }

        if (value.Length > MaxLength)
        {
            errors.Add(Errors.FirstName.TooLong(MaxLength));
        }

        if (!Regex.IsMatch(value, Pattern))
        {
            errors.Add(Errors.FirstName.InvalidFormat);
        }

        return errors.IsEmpty()
            ? Result<FirstName>.Success(new(value))
            : Result<FirstName>.Failure(errors);
    }

    /// <summary>
    /// Devuelve una representación en cadena del nombre.
    /// </summary>
    /// <returns>El nombre como una cadena.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Define una conversión implícita de <see cref="FirstName"/> a <see cref="string"/>.
    /// </summary>
    /// <param name="firstName">El valor de <see cref="FirstName"/> a convertir.</param>
    public static implicit operator string(FirstName firstName) => firstName.Value;
}
