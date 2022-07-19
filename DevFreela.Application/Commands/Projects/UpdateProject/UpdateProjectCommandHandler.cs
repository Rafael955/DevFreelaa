using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Project
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
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
