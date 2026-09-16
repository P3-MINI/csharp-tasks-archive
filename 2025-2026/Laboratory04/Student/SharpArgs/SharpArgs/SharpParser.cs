namespace SharpArgs;

/// <summary>
/// A parser for command-line arguments that populates an options model.
/// </summary>
/// <typeparam name="T">The type of the options model, which must inherit from <see cref="SharpOptions"/>.</typeparam>
public class SharpParser<T>
    where T : SharpOptions, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SharpParser{T}"/> class.
    /// </summary>
    public SharpParser()
    {
    }

    /// <summary>
    /// Parses the command-line arguments and populates a new instance of the options model.
    /// </summary>
    /// <param name="args">The command-line arguments to parse.</param>
    /// <returns>A <see cref="ParseResult{T}"/> containing the populated options object and any errors.</returns>
    public ParseResult<T> Parse(string[] args)
    {
        // TODO: Implement in Stage04
        throw new NotImplementedException();
    }
}