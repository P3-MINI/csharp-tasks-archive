using SharpArgs.Exceptions;
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
        var exceptions = new List<Exception>();

        var optionTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(SharpOptions)) && !t.IsAbstract)
            .ToList();

        foreach (var type in optionTypes)
        {
            try
            {
                if (Activator.CreateInstance(type) is SharpOptions optionsInstance)
                {
                    optionsInstance.ValidateModel();
                }
            }
            catch (SharpArgsException ex)
            {
                exceptions.Add(ex);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is SharpArgsException sae)
            {
                exceptions.Add(sae);
            }
        }

        if (exceptions.Count != 0)
        {
            throw new AggregateException(
                "Model validation failed for one or more SharpOptions types.", exceptions);
        }
    }
}