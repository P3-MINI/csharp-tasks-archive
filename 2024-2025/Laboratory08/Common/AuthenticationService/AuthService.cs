namespace AuthenticationService
{
    public class AuthService
    {
        Dictionary<string, string> _users = new();
        private readonly List<string> _loggedUsers = new();

        public int GetRegisteredUsersCount() => _users.Count;        
        public int GetLoggedUsersCount() => _loggedUsers.Count;

        public bool Register(string username, string password)
        {
            return CredentialsValidator.ValidateUsername(username) &&
                    CredentialsValidator.ValidatePassword(password) && 
                    _users.TryAdd(username, PasswordHasher.HashPassword(password));
        }

        public User GetRegisteredUserData(string username)
        {
            if(!_users.ContainsKey(username))
                throw new UserNotFoundException("User not found!");
            return new User(username, _users[username]);
        }

        public bool Login(string username, string password)
        {
            if (!_users.ContainsKey(username) || _users[username] != PasswordHasher.HashPassword(password))
                return false;
            _loggedUsers.Add(username);
            return true;
            throw new NotImplementedException();
        }

    }
}
