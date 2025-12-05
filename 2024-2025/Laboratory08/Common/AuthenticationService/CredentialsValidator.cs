using System.Text.RegularExpressions;

namespace AuthenticationService
{
    public static class CredentialsValidator
    {
        public static bool ValidateUsername(string username)
        {
            var match = Regex.Match(username, "^[a-zA-Z][a-zA-Z0-9_]{7,31}$");
            return match.Success;
        }

        public static bool ValidatePassword(string password)
        {
            var match = Regex.Match(password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{}|\\\\,.<>?]).{8,16}$");
            return match.Success;
        }
    }
}
