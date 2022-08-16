using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users
{
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, Unit>
    {
        private readonly IUserRepository _repository;

        public NewUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Fullname, request.Email, request.BirthDate, request.Password, request.Role);

            await _repository.CreateUserAsync(user);

            return Unit.Value;
        }
    }
}
