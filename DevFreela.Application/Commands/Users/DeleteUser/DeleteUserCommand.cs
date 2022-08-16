using DevFreela.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
