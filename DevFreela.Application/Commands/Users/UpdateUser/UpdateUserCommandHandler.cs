using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var projectToUpdate = await _repository.GetByIdAsync(request.Id);

            if (projectToUpdate != null)
            {
                projectToUpdate.Update(request.Fullname, request.Email, request.BirthDate);

                await _repository.UpdateUserAsync(projectToUpdate);
            }

            return Unit.Value;
        }
    }
}
