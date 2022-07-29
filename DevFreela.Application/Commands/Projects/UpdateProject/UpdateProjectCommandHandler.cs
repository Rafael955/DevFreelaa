using DevFreela.Core.Repositories;
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
        private readonly IProjectRepository _repository;

        public UpdateProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToUpdate = (await _repository.GetAllAsync()).SingleOrDefault(x => x.Id == request.Id);

            if (projectToUpdate != null)
            {
                projectToUpdate.Update(request.Title, request.Description, request.TotalCost);

                await _repository.UpdateProjectAsync(projectToUpdate);
            }

            return Unit.Value;
        }
    }
}
