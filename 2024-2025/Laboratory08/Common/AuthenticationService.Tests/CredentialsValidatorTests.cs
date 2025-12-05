using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Tests
{
    [TestClass]
    public class CredentialsValidatorTests
    {
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

        [DataRow("123abc456")]
        [DataRow("_username")]
        [DataRow("user")]
        [DataRow("\\(^o^)/")]
        [DataRow("User@name")]
        [DataRow("SuperLongUsernameNobodysGoingToRepeat")]
        [TestMethod]
        public void CredentialsValidator_InvalidUsername_ShouldFail(string s)
        {
            Assert.IsFalse(CredentialsValidator.ValidateUsername(s));
        }

        [DataRow("UserName")]
        [DataRow("MarioTheStrong_01")]
        [DataRow("hello_kitty_")]
        [DataRow("smiley2137")]
        [DataRow("qwertyqwerty")]
        [DataRow("ONLY_CAPSLOCK")]
        [TestMethod]
        public void CredentialsValidator_ValidUsername_ShouldSucceed(string s)
        {
            Assert.IsTrue(CredentialsValidator.ValidateUsername(s));
        }

        [DataRow("QWErtyASDfgh_123!@#")]
        [DataRow("password")]
        [DataRow("1234567Aa")]
        [DataRow("PaSsWoRd!")]
        [DataRow("STRONG_PASS!")]
        [DataRow("\\(^o^)/")]
        [TestMethod]
        public void CredentialsValidator_InvalidPassword_ShouldFail(string s)
        {
            Assert.IsFalse(CredentialsValidator.ValidatePassword(s));
        }


        [DataRow("o\\(O_0)/o")]
        [DataRow("!Nic3Password*")]
        [DataRow("(modnaRlat0t")]
        [DataRow("PJDs6a!q")]
        [TestMethod]
        public void CredentialsValidator_ValidPassword_ShouldSucceed(string s)
        {
            Assert.IsTrue(CredentialsValidator.ValidatePassword(s));
        }
    }
}
