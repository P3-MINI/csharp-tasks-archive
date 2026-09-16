namespace SharpArgs.Exceptions;

/// <summary>
/// Thrown when duplicate values are found.
/// </summary>
/// <typeparam name="T">The type of the duplicate values.</typeparam>
public sealed class DuplicateValuesException<T> : SharpArgsException
{
    /// <summary>
    /// Gets the collection of duplicate values that were found.
    /// </summary>
    public IReadOnlyCollection<T> DuplicateValues { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateValuesException{T}"/> class with a pre-filtered list of duplicates.
    /// </summary>
    /// <param name="duplicateValues">The collection of duplicate values that were found.</param>
    public DuplicateValuesException(IReadOnlyCollection<T> duplicateValues)
    {
        this.DuplicateValues = duplicateValues;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateValuesException{T}"/> class with a pre-filtered list of duplicates.
    /// </summary>
    /// <param name="duplicateValues">The collection of duplicate values that were found.</param>
    /// <param name="message">The message that describes the error.</param>
    public DuplicateValuesException(IReadOnlyCollection<T> duplicateValues, string message)
        : base(message)
    {
        this.DuplicateValues = duplicateValues;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateValuesException{T}"/> class with a pre-filtered list of duplicates.
    /// </summary>
    /// <param name="duplicateValues">The collection of duplicate values that were found.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public DuplicateValuesException(IReadOnlyCollection<T> duplicateValues, string message, Exception inner)
        : base(message, inner)
    {
        this.DuplicateValues = duplicateValues;
    }
}