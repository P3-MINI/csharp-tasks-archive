using SharpArgs.Attributes;

namespace SharpArgs.Console;

/// <summary>
/// Invalid flag type - Verbose is int, but flags must be bool.
/// </summary>
public class InvalidFlagType : SharpOptions
{
    [Flag("verbose", 'v', Long = "verbose", Help = "Enables detailed logging")]
    public int Verbose { get; set; }

    [Option("output", 'o', Long = "output", Required = true, Help = "Output file path")]
    public string OutputPath { get; set; } = string.Empty;
}

/// <summary>
/// Duplicate flag short names - two flags use 'a'.
/// </summary>
public class DuplicateFlagShortsArgs : SharpOptions
{
    [Flag("all", 'a', Long = "all", Help = "Remove all artifacts")]
    public bool All { get; set; }

    [Flag("allow", 'a', Long = "allow", Help = "Allow something else")]
    public bool Allow { get; set; }
}

/// <summary>
/// Unsupported option property type - ComplexType does not implement IParsable<>.
/// </summary>
public class UnsupportedOptionTypeArgs : SharpOptions
{
    public class ComplexType { /* not IParsable<> */ }

    [Option("complex", 'c', Long = "complex", Help = "A complex option")]
    public ComplexType Complex { get; set; } = new ComplexType();
}

/// <summary>
///  Valid example - flags are bool, options are string or IParsable (int).
/// </summary>
public class ValidArgs : SharpOptions
{
    [Flag("verbose", 'v', Long = "verbose", Help = "Enable verbose logging")]
    public bool Verbose { get; set; }

    [Flag("dryrun", 'd', Long = "dry-run", Help = "Do not perform side effects")]
    public bool DryRun { get; set; }

    [Option("output", 'o', Long = "output", Required = true, Help = "Output path")]
    public string Output { get; set; } = string.Empty;

    [Option("retries", 'r', Long = "retries", Default = "3", Help = "Retry count")]
    public int Retries { get; set; }
}
