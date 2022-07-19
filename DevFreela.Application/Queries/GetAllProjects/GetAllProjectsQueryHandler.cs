using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            //Dapper
            //using (var conn = new SqlConnection())
            //{
            //    conn.Open();

            //    var script = "SELECT Id, Title, CreatedAt FROM Project";

            //    var projects = (await conn.QueryAsync<ProjectViewModel>(script)).ToList();

            //    return projects;
            //}

            //Entity Framework
            var projects = _dbContext.Projects;

            var projectsViewModel = await projects
                .Where(x => x.Title.Contains(request.Query))
                .Select(x => new ProjectViewModel(x.Id, x.Title, x.CreatedAt))
                .ToListAsync();

            return projectsViewModel;
        }
    }
}
