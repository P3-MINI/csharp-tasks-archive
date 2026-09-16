using FluentAssertions;
using SharpArgs.Attributes;
using SharpArgs.Exceptions;

namespace SharpArgs.Tests;

public class SharpOptionsTests
{
    // --- Test Models ---

    private class ValidOptions : SharpOptions { }

    private class InvalidFlagTypeOptions : SharpOptions
    {
        [Flag("invalid", 'i')]
        public int InvalidFlag { get; set; }
    }

    private class DuplicateShortNameOptions : SharpOptions
    {
        [Flag("help", 'h')]
        public bool Help { get; set; }

        [Flag("host", 'h')] // duplicate 'h' short name
        public bool Host { get; set; }
    }

    private class UnsupportedOptionTypeOptions : SharpOptions
    {
        [Option("unsupported", 'u')]
        public object? Data { get; set; } // object does not implement IParsable<>
    }

    // --- Tests ---

    [Fact]
    public void ValidateModel_ShouldNotThrow_ForValidModel()
    {
        // Arrange
        var options = new ValidOptions();
        var act = options.ValidateModel;

        // Act & Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ValidateFlags_ShouldThrowInvalidTypeException_ForNonBoolFlag()
    {
        // Arrange
        var options = new InvalidFlagTypeOptions();
        var act = options.ValidateFlags;

        // Act & Assert
        act.Should().Throw<InvalidTypeException>()
            .Where(ex => ex.InvalidType == typeof(int));
    }

    [Fact]
    public void ValidateFlags_ShouldThrowDuplicateValuesException_ForDuplicateShortNames()
    {
        // Arrange
        var options = new DuplicateShortNameOptions();
        var act = options.ValidateFlags;

        // Act & Assert
        act.Should().Throw<DuplicateValuesException<char>>()
            .Where(ex => ex.DuplicateValues.Contains('h'));
    }

    [Fact]
    public void ValidateOptions_ShouldThrowInvalidTypeException_ForUnsupportedType()
    {
        // Arrange
        var options = new UnsupportedOptionTypeOptions();
        var act = options.ValidateOptions;

        // Act & Assert
        act.Should().Throw<InvalidTypeException>()
            .Where(ex => ex.InvalidType == typeof(object));
    }
}