namespace DevFreela.Core.Entities
{
    public class UserLogin : BaseEntity
    {
        public UserLogin(string username, string password, string email, bool isLogged)
        {
            Username = username;
            Password = password;
            Email = email;
            this.isLogged = isLogged;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public bool isLogged { get; private set; } = false;

        public void Login()
        {
            isLogged = true;
        }

        public void Logoff()
        {
            isLogged = false;
        }
    }
}
