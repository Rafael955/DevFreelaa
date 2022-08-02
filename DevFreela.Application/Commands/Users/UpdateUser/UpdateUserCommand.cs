using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public UpdateUserCommand(int id, string fullname, string email, DateTime birthdate)
        {
            Id = id;
            Fullname = fullname;
            Email = email;
            BirthDate = birthdate;
        }

        public int Id { get; private set; }

        public string Fullname { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}
