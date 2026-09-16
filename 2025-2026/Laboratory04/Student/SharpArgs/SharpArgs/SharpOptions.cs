using System.Reflection;

namespace SharpArgs;

/// <summary>
/// An abstract base class for defining command-line option models.
/// Provides validation logic for flag and options.
/// </summary>
public abstract class SharpOptions
{
    private readonly List<PropertyInfo> _properties = [];

    public SharpOptions()
    {
    }

    /// <summary>
    /// Validates all properties marked with the <see cref="FlagAttribute"/>.
    /// </summary>
    /// <exception cref="InvalidTypeException">Thrown if a flag property is not of type <see cref="bool"/>.</exception>
    /// <exception cref="DuplicateValuesException{T}">Thrown if duplicate short names are found among flags.</exception>
    public virtual void ValidateFlags()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Validates all properties marked with the <see cref="OptionAttribute"/>
    /// </summary>
    /// <exception cref="InvalidTypeException">Thrown if an option property's type is not a <see cref="string"/> and does not implement <see cref="IParsable{TSelf}"/>.</exception>
    public virtual void ValidateOptions()
    {
        // todo
        throw new NotImplementedException();
    }

    /// <summary>
    /// Validates the entire options model by calling all individual validation methods.
    /// </summary>
    public void ValidateModel()
    {
        ValidateFlags();
        ValidateOptions();
    }
}