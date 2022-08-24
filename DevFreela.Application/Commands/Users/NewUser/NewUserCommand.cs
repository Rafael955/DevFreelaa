using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Commands.Users
{
    public class NewUserCommand : IRequest<int>
    {
        public string Fullname { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
