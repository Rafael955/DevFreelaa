using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _repository;

        public GetProjectByIdQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            //Dapper
            var project = await _repository.GetByIdAsync(request.Id);

            var projectViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt, project.ClientFullName, project.FreelancerFullName);

            return projectViewModel;

            //Entity Framework
            //var project = _dbContext.Projects
            //    .Include(p => p.Client)
            //    .Include(p => p.Freelancer)
            //    .SingleOrDefault(x => x.Id == id);

            //var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt, project.Client.Fullname, project.Freelancer.Fullname);

            //return projectDetailsViewModel;
        }
    }
}
