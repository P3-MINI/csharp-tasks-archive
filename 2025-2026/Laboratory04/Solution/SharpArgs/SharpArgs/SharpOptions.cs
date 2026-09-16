using SharpArgs.Attributes;
using SharpArgs.Exceptions;
using System.Reflection;

namespace SharpArgs;

/// <summary>
/// An abstract base class for defining command-line option models.
/// Provides validation logic for flag and options.
/// </summary>
public abstract class SharpOptions
{
    private readonly List<PropertyInfo> _properties;

    public SharpOptions()
    {
        _properties = this.GetType()
            .GetProperties()
            .ToList();
    }

    /// <summary>
    /// Validates all properties marked with the <see cref="FlagAttribute"/>.
    /// </summary>
    /// <exception cref="InvalidTypeException">Thrown if a flag property is not of type <see cref="bool"/>.</exception>
    /// <exception cref="DuplicateValuesException{T}">Thrown if duplicate short names are found among flags.</exception>
    public virtual void ValidateFlags()
    {
        var flagProperties = _properties
            .Where(p => p.IsDefined(typeof(FlagAttribute), false));

        if (!flagProperties.Any())
        {
            return;
        }

        foreach (var prop in flagProperties)
        {
            if (prop.PropertyType != typeof(bool))
            {
                throw new InvalidTypeException(
                    prop.PropertyType,
                    $"Flag property '{prop.Name}' must be of type bool, but is '{prop.PropertyType.Name}'.");
            }
        }

        var flagAttributes = flagProperties
            .Select(p => p.GetCustomAttribute<FlagAttribute>(false))
            .Where(attr => attr != null)
            .Select(attr => attr!)
            .ToList();

        var shortNames = flagAttributes
            .Select(attr => attr.Short);

        var duplicateShortNames = shortNames.FindDuplicates();

        if (duplicateShortNames.Count != 0)
        {
            throw new DuplicateValuesException<char>(
                duplicateShortNames,
                $"Duplicate short names found: {string.Join(", ", duplicateShortNames)}");
        }
    }

    /// <summary>
    /// Validates all properties marked with the <see cref="OptionAttribute"/>
    /// </summary>
    /// <exception cref="InvalidTypeException">Thrown if an option property's type is not a <see cref="string"/> or it does not implement <see cref="IParsable{TSelf}"/>.</exception>
    public virtual void ValidateOptions()
    {
        var optionProperties = this._properties
            .Where(p => p.IsDefined(typeof(OptionAttribute), false));

        foreach (var prop in optionProperties)
        {
            var propType = prop.PropertyType;

            if (propType == typeof(string))
            {
                continue;
            }

            var isParsable = propType.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IParsable<>));

            if (!isParsable)
            {
                throw new InvalidTypeException(
                    propType,
                    $"Option property '{prop.Name}' has an unsupported type '{propType.Name}'. Type must be a string or implement IParsable<>.");
            }
        }
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