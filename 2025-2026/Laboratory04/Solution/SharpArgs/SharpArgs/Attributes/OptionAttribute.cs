namespace SharpArgs.Attributes;

/// <summary>
/// Defines a named option argument, which can be specified with a short or long name (e.g., -f file or --file file).
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="OptionAttribute"/> class.
/// </remarks>
/// <param name="id">The unique identifier for the attribute.</param>
/// <param name="short">The short name of the flag.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class OptionAttribute(string id, char @short) : Attribute
{
    /// <summary>
    /// Gets the unique identifier for the attribute within the class context.
    /// </summary>
    public string Id { get; } = id;

    /// <summary>
    /// Gets or sets the short name of the option (e.g., 'o' for -o).
    /// </summary>
    public char Short { get; set; } = @short;

    /// <summary>
    /// Gets or sets the long name of the option (e.g., "output" for --output).
    /// </summary>
    public string? Long { get; set; }

    /// <summary>
    /// Gets or sets the default value to be used if the argument is not provided.
    /// </summary>
    public string? Default { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the argument is required.
    /// </summary>
    public bool Required { get; set; } = true;

    /// <summary>
    /// Gets or sets the help text for the argument.
    /// </summary>
    public string? Help { get; set; }
}