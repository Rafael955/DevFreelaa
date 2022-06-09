using System;

namespace DevFreela.Application.ViewModels
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel(string fullname, string email, DateTime birthDate, DateTime createdAt, bool active)
        {
            Fullname = fullname;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = createdAt;
            Active = active;
        }

        public string Fullname { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool Active { get; private set; }
    }
}
