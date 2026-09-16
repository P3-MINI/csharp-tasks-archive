# Lab04 - Assemly, Reflection

## Introduction

Your task is to implement the `SharpArgs` library — a library for parsing command-line arguments using attributes and reflection.

The usage of the library is as follows:

- The user creates a class that inherits from the abstract class `SharpOptions`.
- Selected properties are marked with attributes provided by the library: `Flag` and `Option` (see **Stage 1**).
- The `SharpParser` class (see **Stage 4**) is responsible for processing the `string[] args` array and returning a model instance with populated properties.

## Scoring

- **Stage 1**: `1` points for each attribute.
- **Stage 2**: `2` points for each implemented method.
- **Stage 3**: `2.5` points for assembly scanning implementation, `0.5` points for proper invocation in the `Main` method.
- **Stage 4**: `3` points for proper parsing, assigning values to the model, and error handling.

## Useful links

- [Microsoft Learn: Attributes](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/)
- [Microsoft Learn: Retrieving Information Stored in Attributes](https://learn.microsoft.com/en-us/dotnet/standard/attributes/retrieving-information-stored-in-attributes)
- [Microsoft Learn: Retrieving Attributes from Class Members](https://learn.microsoft.com/en-us/dotnet/standard/attributes/retrieving-information-stored-in-attributes#retrieving-attributes-from-class-members)
- [Microsoft Learn: Assembly Class](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly?view=net-9.0)
- [Microsoft Learn: AggregateException Class](https://learn.microsoft.com/en-us/dotnet/api/system.aggregateexception?view=net-9.0)

## Stage 1 (no unit tests, just show the teacher)

Design the following attributes:

1. `[Flag]` for boolean values (flags, e.g., `--verbose`). Should have the following positional parameters: `string Id`, `char Short` and optional parameters: `string? Long`, `string? Help`.

2. `[Option]` for named arguments. Should have the following positional parameters: `string Id`, `char Short` and optional parameters: `string? Long`, `string? Default`, `bool Required`, `string? Help`.

### Property Descriptions

- `Id` – a unique identifier for the attribute within the class.
- `Short` – short form of the flag (single character, e.g., `v` mapped to `-v`).
- `Long` – long form of the flag (e.g., `verbose` mapped to `--verbose`).
- `Help` – help/description text for the argument.
- `Default` – default value used if the argument is not provided.
- `Required` – whether the argument is required, default is `true`.

## Stage 2 (unit tests in `SharpOptionsTests.cs` file)

Implement the following methods in the abstract base class `SharpOptions` (in `SharpOptions.cs` file). If a method description mentions exception being thrown, those exceptions are already implemented and can be found in the `Exceptions/` directory. These methods validate correct usage of attributes implemented in the first stage for the type which inherits from `SharpOptions`.

- `ValidateFlags`:

  - The method should check whether every property marked with `[Flag]` has the type `bool` and whether there are no duplicate short flag forms.
  - Otherwise, raises `InvalidTypeException` and `DuplicateValuesException<string>`.

- `ValidateOptions`:
  - The method should check whether the type of each property marked with `[Option]` implements the `IParsable<T>` interface from the `System` namespace or is equal to type `string`.
  - Otherwise, raises `InvalidTypeException`.

To find duplicates in generic `IEnumerable<T>` collections, use the ready method `FindDuplicates<T>` in the `EnumerableExtensions.cs` file.

## Stage 3 (testing via invocation in `Program.cs` file)

The goal of this stage is to enable automatic detection of all classes in a given assembly that inherit from `SharpOptions` and to validate their configuration. All validation errors should be collected in a single `AggregateException`.

Validation should be performed using the static `SharpOptionsAssemblyValidator` class (in `SharpOptionsAssemblyValidator.cs` file).

- The validator should search through `assembly` and find all the types inheriting from `SharpOptions`.
- For each type, call the `ValidateModel()` method to check flag and option configuration.
- All validation exceptions (subclasses of `SharpArgsException`) should be gathered and raised as a single `AggregateException`.

Use the implemented validator for the current assembly in the `Main` method of the `Program` class (`Program.cs` file).

## Stage 4 (unit tests in `SharpParserTests.cs` file)

The goal of this stage is to recognize all properties in the model marked with `[Flag]`, then parse the original `string[] args` argument array according to this model.

- The parsing logic should be located in the `SharpParser` class (`SharpParser.cs` file).
- Short flag forms start with a single dash (`-`), long forms with two dashes (`--`).
- The method should create a model instance (using `Activator.CreateInstance`) and set its flags based on the provided argument array.
- The method should return an instance of the `ParseResult<T>` class (this class is already prepared in `ParseResult.cs` file), containing the populated model or a collection of parsing errors.
- Encountering an unknown flag (or an option which doesn't start with `-` or `--`) adds the entry: `"Unknown option: {arg}."` to the collection of errors.
