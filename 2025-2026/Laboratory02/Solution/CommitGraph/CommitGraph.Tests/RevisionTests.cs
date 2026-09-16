using FluentAssertions;

namespace CommitGraph.Tests;

public class RevisionTests
{
    [Theory]
    [InlineData("HEAD", "HEAD")]
    [InlineData("main", "main")]
    [InlineData("feature/gui", "feature/gui")]
    [InlineData("a1b2c3d4", "a1b2c3d4")]
    [InlineData("HEAD~", "HEAD~")]
    [InlineData("HEAD^", "HEAD^")]
    [InlineData("feature/branch^1", "feature/branch^")]
    [InlineData("HEAD~1", "HEAD~")]
    [InlineData("HEAD^1", "HEAD^")]
    [InlineData("HEAD~~", "HEAD~~")]
    [InlineData("HEAD^^", "HEAD^^")]
    [InlineData("HEAD~2", "HEAD~2")]
    [InlineData("HEAD^2", "HEAD^2")]
    [InlineData("HEAD^2~3", "HEAD^2~3")]
    [InlineData("HEAD^2~3^", "HEAD^2~3^")]
    [InlineData("HEAD^2^3~", "HEAD^2^3~")]
    [InlineData("HEAD^^3~", "HEAD^^3~")]
    [InlineData("a1b2c3d4^3~1", "a1b2c3d4^3~")]
    public void Parse_ShouldCorrectlyIdentifyBaseAndModifiers_WhenPatternIsValid(string inputPattern, string expectedOutput)
    {
        // Arrange
        var revision = Revision.Parse(inputPattern);

        // Act
        var resultString = revision.ToString();

        // Assert
        resultString.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("")]                    // empty sequence
    [InlineData(" ")]                   // whitespace characters
    [InlineData("~HEAD")]               // no base reference at the beginning
    [InlineData("main^ 2")]             // space
    [InlineData("HEAD~%2")]              // invalid modifier symbol
    [InlineData("feature/branch-1~0")]  // 0 after modifier symbol
    [InlineData("HEAD~1.5")]            // invalid number or invalid modifier symbol
    [InlineData("a1b2c3d^ ")]           // whitespace character at the end
    public void Parse_ShouldThrowInvalidOperationException_ForInvalidPatterns(string invalidPattern)
    {
        // Arrange
        Action act = () => Revision.Parse(invalidPattern);

        // Act & Assert
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage($"Pattern '{invalidPattern}' does not follow revision syntax");
    }
}
