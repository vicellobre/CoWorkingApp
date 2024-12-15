using System.Reflection;

namespace CoWorkingApp.Persistence;

/// <summary>
/// Proporciona una referencia al emsambaje actual.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// Representa al ensamblaje actual en la que se define esta clase.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
