using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Commands.Project
{ 
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _repository;

        public DeleteProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToDelete = (await _repository.GetAllAsync()).SingleOrDefault(x => x.Id == request.Id);

            projectToDelete.Cancel();

            await _repository.DeleteProjectAsync(projectToDelete);

            return Unit.Value;
        }
    }
}
