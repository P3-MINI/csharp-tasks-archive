using MiniTest;

namespace AuthenticationService.Tests;

[TestClass]
public class PasswordHasherTests
{
    [DataRow("!Nic3Password*", Description = "!Nic3Password*")]
    [DataRow("(modnaRlat0t", Description = "(modnaRlat0t")]
    [DataRow("PJDs6a!q", Description = "PJDs6a!q")]
    [TestMethod]
    public void PasswordHasher_SamePassword_ShouldHaveSameHash(string s)
    {
        var passHash1 = PasswordHasher.HashPassword(s);
        var passHash2 = PasswordHasher.HashPassword(s);
        Assert.AreEqual(passHash1, passHash2);
    }

    [TestMethod]
    public void PasswordHasher_DifferentPasswords_ShouldHaveDifferentHashes()
    {
        var passHash1 = PasswordHasher.HashPassword("KaWaRaNo_FTW!");
        var passHash2 = PasswordHasher.HashPassword("KaWaRaNo_FTW?");
        Assert.AreNotEqual(passHash1, passHash2);
    }
}