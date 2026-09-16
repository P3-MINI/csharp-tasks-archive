using FluentAssertions;
using SharpArgs.Attributes;

namespace SharpArgs.Tests;

public class SharpParserTests
{
    // --- Test Model ---

    private class TestOptions : SharpOptions
    {
        [Flag("all", 'a', Long = "all", Help = "Show all items.")]
        public bool All { get; set; }

        [Flag("long", 'l', Long = "long", Help = "Use long listing format.")]
        public bool Long { get; set; }

        [Flag("verbose", 'v', Long = "verbose", Help = "Enable verbose output.")]
        public bool Verbose { get; set; }
    }

    private readonly SharpParser<TestOptions> parser;

    public SharpParserTests()
    {
        this.parser = new SharpParser<TestOptions>();
    }

    // --- Tests ---

    [Fact]
    public void Parse_ShouldSetFlagToTrue_WhenLongNameIsProvided()
    {
        // Arrange
        var args = new[] { "--verbose" };

        // Act
        var result = this.parser.Parse(args);

        // Assert
        result.Success.Should().BeTrue();
        result.Target.Should().NotBeNull();
        result.Target!.Verbose.Should().BeTrue();
        result.Target!.All.Should().BeFalse();
    }

    [Fact]
    public void Parse_ShouldSetFlagToTrue_WhenShortNameIsProvided()
    {
        // Arrange
        var args = new[] { "-v" };

        // Act
        var result = this.parser.Parse(args);

        // Assert
        result.Success.Should().BeTrue();
        result.Target.Should().NotBeNull();
        result.Target!.Verbose.Should().BeTrue();
        result.Target!.Long.Should().BeFalse();
    }

    [Fact]
    public void Parse_ShouldSetMultipleFlagsToTrue_WhenProvided()
    {
        // Arrange
        var args = new[] { "--all", "-v" };

        // Act
        var result = this.parser.Parse(args);

        // Assert
        result.Success.Should().BeTrue();
        result.Target.Should().NotBeNull();
        result.Target!.All.Should().BeTrue();
        result.Target!.Verbose.Should().BeTrue();
        result.Target!.Long.Should().BeFalse();
    }

    [Fact]
    public void Parse_ShouldFail_ForUnknownOption()
    {
        // Arrange
        var args = new[] { "--unknown-flag" };

        // Act
        var result = this.parser.Parse(args);

        // Assert
        result.Success.Should().BeFalse();
        result.Target.Should().BeNull();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().Contain("--unknown-flag");
    }
}