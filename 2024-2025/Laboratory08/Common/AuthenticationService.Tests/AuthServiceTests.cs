namespace AuthenticationService.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void Register_NewUsername_ShouldAddNewUser()
        {
            var authService = new AuthService();
            var result = authService.Register("SuperUser", "Kpsv#1Au9");
            Assert.IsTrue(result);
            Assert.AreEqual(authService.GetRegisteredUsersCount(), 1);
        }

        [TestMethod]
        public void Register_InvalidUsername_ShouldRejectNewUser()
        {
            var authService = new AuthService();
            var result = authService.Register("superme", "StrongPass_123");
            Assert.IsFalse(result);
            Assert.AreEqual(authService.GetRegisteredUsersCount(), 0);
        }

        [TestMethod]
        public void Register_InvalidPassword_ShouldRejectNewUser()
        {
            var authService = new AuthService();
            var result = authService.Register("SuperUser", "StrongPass123");
            Assert.IsFalse(result);
            Assert.AreEqual(authService.GetRegisteredUsersCount(), 0);
        }

        [TestMethod]
        public void Register_ExistingUsername_ShouldRejectExistingUser()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            var result = authService.Register("SuperUser", "StrongerPass_123");
            Assert.IsFalse(result);
            Assert.AreEqual(authService.GetRegisteredUsersCount(), 1);
        }

        [TestMethod]
        public void Register_TwoDifferentUsernames_ShouldAddBothUsers()
        {
            var authService = new AuthService();
            var registerResult1 = authService.Register("SuperUser", "StrongPass_123");
            var registerResult2 = authService.Register("YetAnotherSuperUser", "StrongerPass_123");
            Assert.IsTrue(registerResult1);
            Assert.IsTrue(registerResult2);
            Assert.AreEqual(authService.GetRegisteredUsersCount(), 2);
        }

        [TestMethod]
        public void GetRegisteredUserData_NonExistingUsername_ShouldThrowError()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            Assert.ThrowsException<UserNotFoundException>(() => authService.GetRegisteredUserData("superuser"));
        }

        [TestMethod]
        public void GetRegisteredUserData_ExistingUsername_ShouldThrowError()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            var user = authService.GetRegisteredUserData("SuperUser");
            Assert.AreEqual(user.Username, "SuperUser");
            Assert.AreNotEqual(user.PasswordHash, "StrongPass_123");
        }

        [TestMethod]
        public void Login_NonExistingUsername_ShouldFail()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            var result = authService.Login("superuser", "StrongPass_123");
            Assert.IsFalse(result);
            Assert.AreEqual(authService.GetLoggedUsersCount(), 0);
        }

        [TestMethod]
        public void Login_ValidPassword_ShouldSucceed()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            var result = authService.Login("SuperUser", "StrongPass_123");
            Assert.IsTrue(result);
            Assert.AreEqual(authService.GetLoggedUsersCount(), 1);
        }

        [TestMethod]
        public void Login_InvalidPassword_ShouldFail()
        {
            var authService = new AuthService();
            authService.Register("SuperUser", "StrongPass_123");
            var result = authService.Login("SuperUser", "OldAccountPass!1");
            Assert.IsFalse(result);
            Assert.AreEqual(authService.GetLoggedUsersCount(), 0);
        }
    }
}