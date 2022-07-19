using Dapper;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            //Dapper
            using (var conn = new SqlConnection())
            {
                conn.Open();

                var script = $"SELECT Id, Title, Description, TotalCost, StartedAt, FinishedAt, ClientFullName, FreelancerFullName FROM Project WHERE Id = {request.Id}";

                var project = await conn.QueryAsync<ProjectDetailsViewModel>(script);

                return project.FirstOrDefault();
            }

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
