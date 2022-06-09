namespace DevFreela.Application.ViewModels
{
    public class UserLoginViewModel
    {
        public UserLoginViewModel(string username, string password, string email, bool isLogged)
        {
            Username = username;
            Password = password;
            Email = email;
            this.isLogged = isLogged;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public bool isLogged { get; private set; }
    }
}
