using System.ComponentModel;

namespace AuthenticationService
{
    public class AuthService(ICredentialsValidator validator)
    {
        private ICredentialsValidator CredentialsValidator { get; } = validator;
        private Dictionary<string, string> Users { get; } = [];
        private List<string> LoggedUsers { get; } = [];

        public int GetRegisteredUsersCount() => Users.Count;        
        public int GetLoggedUsersCount() => LoggedUsers.Count;

        public bool Register(string username, string password)
        {
            return CredentialsValidator.ValidateUsername(username) &&
                    CredentialsValidator.ValidatePassword(password) && 
                    Users.TryAdd(username, PasswordHasher.HashPassword(password));
        }

        public User GetRegisteredUserData(string username)
        {
            if(!Users.TryGetValue(username, out var passwordHash))
                throw new UserNotFoundException("User not found!");
            return new User(username, passwordHash);
        }

        public bool Login(string username, string password)
        {
            if (!Users.TryGetValue(username, out var value) || value != PasswordHasher.HashPassword(password))
                return false;
            LoggedUsers.Add(username);
            return true;
        }
        
        [Description("This method is implemented incorrectly on purpose. Do not change it.")]
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!Users.TryGetValue(username, out var hash))
            {
                throw new NotImplementedException("Method under construction!");
            }

            if (hash == oldPassword || CredentialsValidator.ValidatePassword(newPassword))
            {
                return false;
            }
            
            Users[username] = PasswordHasher.HashPassword(newPassword);
            return true;
        }
    }
}
