using MiniTest;

namespace AuthenticationService.Tests;

[TestClass]
public class CredentialsValidatorTests
{
    private CredentialsValidator CredentialsValidator { get; set; } = null!;

    [BeforeEach]
    public void Setup()
    {
        CredentialsValidator = new CredentialsValidator();
    }
    
    [TestMethod]
    public void CredentialsValidator_UsernameStartsWithNumber_ShouldFail()
    {
        Assert.IsFalse(CredentialsValidator.ValidateUsername("123abc456"));
    }

    [TestMethod]
    public void CredentialsValidator_UsernameStartsWithUnderscore_ShouldFail()
    {
        Assert.IsFalse(CredentialsValidator.ValidateUsername("_username"));
    }

    [DataRow("123abc456", Description = "Starts with a number")]
    [DataRow("_username", Description = "Starts with underscore")]
    [DataRow("user", Description = "To short (needs at least 8 characters)")]
    [DataRow("\\(^o^)/", Description = "Contains (mostly) illegal characters")]
    [DataRow("User@name", Description = "Contains illegal character")]
    [DataRow("SuperLongUsernameNobodysGoingToRepeat", Description = "Exceeds Username Length Limit")]
    [TestMethod]
    public void CredentialsValidator_InvalidUsername_ShouldFail(string s)
    {
        Assert.IsFalse(CredentialsValidator.ValidateUsername(s));
    }

    [DataRow("UserName", Description = "UserName")]
    [DataRow("MarioTheStrong_01", Description = "MarioTheStrong_01")]
    [DataRow("hello_kitty_", Description = "hello_kitty_")]
    [DataRow("smiley2137", Description = "smiley2137")]
    [DataRow("qwertyqwerty", Description = "qwertyqwerty")]
    [DataRow("ONLY_CAPSLOCK", Description = "ONLY_CAPSLOCK")]
    [TestMethod]
    [Description("These username contain only legal characters ([a-zA-Z0-9_])\n" +
                 "  and have correct length (8-32)")]
    public void CredentialsValidator_ValidUsername_ShouldSucceed(string s)
    {
        Assert.IsTrue(CredentialsValidator.ValidateUsername(s));
    }

    [DataRow("QWErtyASDfgh_123!@#", Description = "Password too long")]
    [DataRow("pass", Description = "Password too short")]
    [DataRow("password", Description = "No upper case letter, number or special character")]
    [DataRow("1234567Aa", Description = "No special character")]
    [DataRow("PaSsWoRd!", Description = "No number")]
    [DataRow("STRONG_PASS1!", Description = "No lowercase letter")]
    [DataRow("\\(^o^)/", Description = "No number, no upper case letter, too short")]
    [TestMethod]
    public void CredentialsValidator_InvalidPassword_ShouldFail(string s)
    {
        Assert.IsFalse(CredentialsValidator.ValidatePassword(s));
    }


    [DataRow("o\\(O_0)/o", Description = "o\\(O_0)/o")]
    [DataRow("!Nic3Password*", Description = "!Nic3Password*")]
    [DataRow("(modnaRlat0t", Description = "(modnaRlat0t")]
    [DataRow("PJDs6a!q", Description = "PJDs6a!q")]
    [TestMethod]
    [Description("These passwords contain between 8 and 16 characters,\n" +
                 "  lower/uppercase char, number and a special character")]
    public void CredentialsValidator_ValidPassword_ShouldSucceed(string s)
    {
        Assert.IsTrue(CredentialsValidator.ValidatePassword(s));
    }
}