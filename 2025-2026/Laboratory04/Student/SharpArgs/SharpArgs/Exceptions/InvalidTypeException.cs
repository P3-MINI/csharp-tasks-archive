namespace SharpArgs.Exceptions;

/// <summary>
/// Thrown when an operation encounters a type that is not supported or is invalid for the context.
/// </summary>
public sealed class InvalidTypeException : SharpArgsException
{
    /// <summary>
    /// Gets the type that was found to be invalid.
    /// </summary>
    public Type InvalidType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTypeException"/> class.
    /// </summary>
    /// <param name="invalidType">The type that was found to be invalid.</param>
    public InvalidTypeException(Type invalidType)
    {
        this.InvalidType = invalidType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTypeException"/> class with a specified error message.
    /// </summary>
    /// <param name="invalidType">The type that was found to be invalid.</param>
    /// <param name="message">The message that describes the error.</param>
    public InvalidTypeException(Type invalidType, string message)
        : base(message)
    {
        this.InvalidType = invalidType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTypeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="invalidType">The type that was found to be invalid.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public InvalidTypeException(Type invalidType, string message, Exception inner)
        : base(message, inner)
    {
        this.InvalidType = invalidType;
    }
}