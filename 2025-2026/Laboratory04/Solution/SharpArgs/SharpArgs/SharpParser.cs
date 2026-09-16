using SharpArgs.Attributes;
using System.Reflection;

namespace SharpArgs;

/// <summary>
/// A parser for command-line arguments that populates an options model.
/// </summary>
/// <typeparam name="T">The type of the options model, which must inherit from <see cref="SharpOptions"/>.</typeparam>
public class SharpParser<T>
    where T : SharpOptions, new()
{
    private const string shortPrefix = "-";
    private const string longPrefix = "--";

    private readonly IEnumerable<PropertyInfo> flagProperties = [];

    private readonly Dictionary<string, PropertyInfo> longNamesMap = [];
    private readonly Dictionary<char, PropertyInfo> shortNamesMap = [];
    private readonly Dictionary<string, PropertyInfo> idsMap = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="SharpParser{T}"/> class.
    /// </summary>
    public SharpParser()
    {
        this.flagProperties = typeof(T).GetProperties()
            .Where(p => p.IsDefined(typeof(FlagAttribute)));

        foreach (var property in this.flagProperties)
        {
            var attribute = property.GetCustomAttribute<FlagAttribute>()!;

            if (!string.IsNullOrEmpty(attribute?.Long))
            {
                this.longNamesMap[attribute.Long] = property;
            }

            this.shortNamesMap[attribute!.Short] = property;
            this.idsMap[attribute.Id] = property;
        }
    }

    /// <summary>
    /// Parses the command-line arguments and populates a new instance of the options model.
    /// </summary>
    /// <param name="args">The command-line arguments to parse.</param>
    /// <returns>A <see cref="ParseResult{T}"/> containing the populated options object and any errors.</returns>
    public ParseResult<T> Parse(string[] args)
    {
        var options = new T();
        var errors = new List<string>();

        try
        {
            options.ValidateModel();
        }
        catch (AggregateException aex)
        {
            var validationMessages = aex.InnerExceptions
                .Select(ex => $"Model validation failed: [{ex.GetType().Name}] {ex.Message}.");
            errors.AddRange(validationMessages);
            return new ParseResult<T>(errors);
        }
        catch (Exception ex)
        {
            errors.Add($"Model validation failed: {ex.Message}.");
            return new ParseResult<T>(errors);
        }

        var mismatchedArgs = new List<string>();
        var matchedArgs = new HashSet<string>();

        foreach (var arg in args)
        {
            var matched = false;
            if (arg.StartsWith(longPrefix))
            {
                var longName = arg[longPrefix.Length..];
                if (this.longNamesMap.TryGetValue(longName, out var prop))
                {
                    prop.SetValue(options, true);
                    matched = true;
                }
            }
            else if (arg.StartsWith(shortPrefix))
            {
                var shortName = arg[shortPrefix.Length..];
                if (shortName.Length == 1 && this.shortNamesMap.TryGetValue(shortName[0], out var prop))
                {
                    prop.SetValue(options, true);
                    matched = true;
                }
            }

            if (matched)
            {
                matchedArgs.Add(arg);
            }
            else
            {
                mismatchedArgs.Add(arg);
            }
        }

        errors.AddRange(mismatchedArgs.Distinct().Select(arg => $"Unknown option: {arg}."));

        if (errors.Count != 0)
        {
            return new ParseResult<T>(errors);
        }

        return new ParseResult<T>(options);
    }
}