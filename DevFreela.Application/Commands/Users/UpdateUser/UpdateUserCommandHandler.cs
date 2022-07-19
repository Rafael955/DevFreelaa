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
        private readonly DevFreelaDbContext _dbContext;

        public UpdateUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var projectToUpdate = _dbContext.Projects.SingleOrDefault(x => x.Id == request.Id);

            if (projectToUpdate != null)
            {
                projectToUpdate.Update(request.Title, request.Description, request.TotalCost);

                _dbContext.Entry(projectToUpdate).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
