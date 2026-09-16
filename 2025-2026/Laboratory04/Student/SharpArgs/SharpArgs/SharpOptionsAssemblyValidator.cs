using System.Reflection;

namespace SharpArgs;

/// <summary>
/// Provides functionality to validate all SharpOptions-derived classes within an assembly.
/// </summary>
public static class SharpOptionsAssemblyValidator
{
    /// <summary>
    /// Finds all concrete types inheriting from SharpOptions in the specified assembly and validates them.
    /// </summary>
    /// <param name="assembly">The assembly to scan for SharpOptions types.</param>
    /// <exception cref="AggregateException">Thrown if one or more validation errors occur, containing all caught exception instances.</exception>
    public static void ValidateAssembly(Assembly assembly)
    {
        // todo
        throw new NotImplementedException();
    }
}