using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Commands.Users
{
    public class NewUserCommand : IRequest<Unit>
    {
        public NewUserCommand(string fullname, string email, DateTime birthDate)
        {
            Fullname = fullname;
            Email = email;
            BirthDate = birthDate;
        }

        public string Fullname { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
