using DevFreela.Application.ViewModels.Users;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
