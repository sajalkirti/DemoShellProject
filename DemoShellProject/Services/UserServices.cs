using DemoShellProject.Models;

namespace DemoShellProject.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();

        public bool ValidateUser(User user)
        {
            return _users.Any(u => u.Id == user.Id && u.Password == user.Password);
        }

        public void UpdateUser(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing == null) throw new Exception("User not found");
            if (string.IsNullOrEmpty(user.Email)) throw new Exception("Email cannot be empty");
            existing.Email = user.Email;
        }
    }
}
