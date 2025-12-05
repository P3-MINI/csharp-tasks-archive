using MiniTest;
using Moq;

namespace AuthenticationService.Tests;

[TestClass]
public class AuthServiceTests
{
    private AuthService AuthService { get; set; } = null!;

    [BeforeEach]
    public void Setup()
    {
        var mockValidator = new Mock<ICredentialsValidator>();
        mockValidator.Setup(v => v.ValidateUsername(It.IsRegex("^valid\\d*$"))).Returns(true);
        mockValidator.Setup(v => v.ValidateUsername(It.IsRegex("^invalid$"))).Returns(false);
        mockValidator.Setup(v => v.ValidatePassword(It.IsRegex("^valid\\d*$"))).Returns(true);
        mockValidator.Setup(v => v.ValidatePassword(It.IsRegex("^invalid$"))).Returns(false);
        AuthService = new AuthService(mockValidator.Object);
    }
    
    [TestMethod]
    public void Register_NewUsername_ShouldAddNewUser()
    {
        var result = AuthService.Register("valid", "valid");
        Assert.IsTrue(result);
        Assert.AreEqual(AuthService.GetRegisteredUsersCount(), 1);
    }

    [TestMethod]
    public void Register_InvalidUsername_ShouldRejectNewUser()
    {
        var result = AuthService.Register("invalid", "valid");
        Assert.IsFalse(result);
        Assert.AreEqual(AuthService.GetRegisteredUsersCount(), 0);
    }

    [TestMethod]
    public void Register_InvalidPassword_ShouldRejectNewUser()
    {
        var result = AuthService.Register("valid", "invalid");
        Assert.IsFalse(result);
        Assert.AreEqual(AuthService.GetRegisteredUsersCount(), 0);
    }

    [TestMethod]
    public void Register_ExistingUsername_ShouldRejectRegisteringUser()
    {
        AuthService.Register("valid000", "valid");
        var result = AuthService.Register("valid000", "valid");
        Assert.IsFalse(result);
        Assert.AreEqual(AuthService.GetRegisteredUsersCount(), 1);
    }

    [TestMethod]
    public void Register_TwoDifferentUsernames_ShouldAddBothUsers()
    {
        var registerResult1 = AuthService.Register("valid000", "valid");
        var registerResult2 = AuthService.Register("valid001", "valid");
        Assert.IsTrue(registerResult1);
        Assert.IsTrue(registerResult2);
        Assert.AreEqual(AuthService.GetRegisteredUsersCount(), 2);
    }

    [TestMethod]
    public void GetRegisteredUserData_NonExistingUsername_ShouldThrowError()
    {
        AuthService.Register("valid", "valid");
        Assert.ThrowsException<UserNotFoundException>(() => AuthService.GetRegisteredUserData("superuser"));
    }

    [TestMethod]
    public void GetRegisteredUserData_ExistingUsername_ShouldThrowError()
    {
        AuthService.Register("valid", "valid");
        var user = AuthService.GetRegisteredUserData("valid");
        Assert.AreEqual(user.Username, "valid");
        Assert.AreNotEqual(user.PasswordHash, "valid");
    }

    [TestMethod]
    public void Login_NonExistingUsername_ShouldFail()
    {
        AuthService.Register("valid", "valid");
        var result = AuthService.Login("invalid", "valid");
        Assert.IsFalse(result);
        Assert.AreEqual(AuthService.GetLoggedUsersCount(), 0);
    }

    [TestMethod]
    public void Login_ValidPassword_ShouldSucceed()
    {
        AuthService.Register("valid", "valid");
        var result = AuthService.Login("valid", "valid");
        Assert.IsTrue(result);
        Assert.AreEqual(AuthService.GetLoggedUsersCount(), 1);
    }

    [TestMethod]
    public void Login_InvalidPassword_ShouldFail()
    {
        AuthService.Register("valid", "valid");
        var result = AuthService.Login("valid", "invalid");
        Assert.IsFalse(result);
        Assert.AreEqual(AuthService.GetLoggedUsersCount(), 0);
    }
    
    [TestMethod]
    [Priority(5)]
    [Description("This test is supposed to fail, just for testing purposes.")]
    public void ChangePassword_NonExistingUsername_ShouldThrowError()
    {
        AuthService.Register("valid", "valid");
        Assert.ThrowsException<UserNotFoundException>(() => AuthService.ChangePassword("invalid", "valid", "valid"));
    }
    
    [TestMethod]
    [Priority(5)]
    [Description("This test is supposed to fail, just for testing purposes.")]
    public void ChangePassword_ValidUserAndPassword_ShouldSucceed()
    {
        AuthService.Register("valid", "valid");
        var result = AuthService.ChangePassword("valid", "valid", "valid");
        Assert.IsTrue(result, "Existing user should be able to change password to something valid.");
    }
    
    [TestMethod]
    [Priority(5)]
    [Description("This test is supposed to fail, just for testing purposes.")]
    public void ChangePassword_InvalidNewPassword_ShouldFail()
    {
        AuthService.Register("valid", "valid");
        var result = AuthService.ChangePassword("valid", "valid", "invalid");
        Assert.IsFalse(result, "User should not be able to change password to something invalid.");
    }
}