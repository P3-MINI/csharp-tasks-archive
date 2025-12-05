using System.Text.RegularExpressions;

namespace AuthenticationService;

public partial class CredentialsValidator : ICredentialsValidator
{
    public bool ValidateUsername(string username)
    {
        var match = UserNameRegex().Match(username);
        return match.Success;
    }

    public bool ValidatePassword(string password)
    {
        var match = PasswordRegex().Match(password);
        return match.Success;
    }

    [GeneratedRegex("^[a-zA-Z][a-zA-Z0-9_]{7,31}$")]
    private static partial Regex UserNameRegex();

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{}|\\,.<>?]).{8,16}$")]
    private static partial Regex PasswordRegex();
}