namespace SharpArgs.Attributes;

/// <summary>
/// Defines a flag argument, which is a boolean value that is present or not (e.g., --verbose).
/// The target property should be of type bool.
/// </summary>
/// <param name="id">The unique identifier for the attribute.</param>
/// <param name="short">The short name of the flag.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class FlagAttribute(string id, char @short) : Attribute
{
    /// <summary>
    /// Gets the unique identifier for the attribute within the class context.
    /// </summary>
    public string Id { get; } = id;

    /// <summary>
    /// Gets or sets the short name of the flag (e.g., 'v' for -v).
    /// </summary>
    public char Short { get; set; } = @short;

    /// <summary>
    /// Gets or sets the long name of the flag (e.g., "verbose" for --verbose).
    /// </summary>
    public string? Long { get; set; }

    /// <summary>
    /// Gets or sets the help text for the argument.
    /// </summary>
    public string? Help { get; set; }

}