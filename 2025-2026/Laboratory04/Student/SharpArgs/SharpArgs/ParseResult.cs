namespace SharpArgs;

/// <summary>
/// Represents the result of a parsing operation.
/// </summary>
/// <typeparam name="T">The type of the options object.</typeparam>
public sealed class ParseResult<T> 
    where T : SharpOptions
{
    /// <summary>
    /// Gets the populated options object if parsing was successful, otherwise null.
    /// </summary>
    public T? Target { get; }

    /// <summary>
    /// Gets a list of errors that occurred during parsing.
    /// </summary>
    public IReadOnlyList<string> Errors { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the parsing was successful.
    /// </summary>
    public bool Success => !this.Errors.Any();

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseResult{T}"/> class.
    /// </summary>
    /// <param name="target">The populated options object.</param>
    public ParseResult(T target)
    {
        this.Target = target;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseResult{T}"/> class.
    /// </summary>
    /// <param name="errors">A list of parsing errors.</param>
    public ParseResult(IReadOnlyList<string> errors)
    {
        this.Target = null;
        this.Errors = errors;
    }
}