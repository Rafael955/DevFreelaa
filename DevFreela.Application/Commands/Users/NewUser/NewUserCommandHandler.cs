using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Users
{
    public class NewUserCommandHandler : IRequestHandler<NewUserCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public NewUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Fullname, request.Email, request.BirthDate);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
