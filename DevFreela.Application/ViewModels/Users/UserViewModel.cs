using System;

namespace DevFreela.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string fullname, string email, DateTime createdAt)
        {
            Fullname = fullname;
            Email = email;
            CreatedAt = createdAt;
        }

        public string Fullname { get; private set; }

        public string Email { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
}

