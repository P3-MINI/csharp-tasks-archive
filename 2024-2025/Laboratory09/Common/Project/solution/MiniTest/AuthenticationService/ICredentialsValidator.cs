namespace AuthenticationService;

public interface ICredentialsValidator
{
    public bool ValidateUsername(string username);
    public bool ValidatePassword(string password);
}