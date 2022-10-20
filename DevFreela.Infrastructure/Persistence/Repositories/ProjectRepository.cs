using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<Project>> GetAllAsync(string query = "")
        {
            List<Project> result;

            if (query == string.Empty)
                result = await _dbContext.Projects.ToListAsync();
            else
                result = await _dbContext.Projects.Where(x => x.Title.Contains(query)).ToListAsync();

            return result;
        }

        public async Task<ProjectDetailsDTO> GetByIdAsync(int id)
        {
            //dapper
            //using (var conn = new SqlConnection(_connectionString))
            //{
            //    await conn.OpenAsync();

            //    var script = $"SELECT P.Id, P.Title, P.Description, P.TotalCost, P.StartedAt, P.FinishedAt, U.FullName as ClientFullName, FreelancerFullName FROM Project as P INNER JOIN User as U WHERE P.Id = {id}";

            //    var result = await conn.QueryAsync<ProjectDetailsDTO>(script);

            //    return result.FirstOrDefault();
            //}

            var project = await _dbContext.Projects.Include(x => x.Client).Include(x => x.Freelancer).SingleOrDefaultAsync(x => x.Id == id);

            var projectDTO = new ProjectDetailsDTO
            {
                Id = project.Id,
                ClientFullName = project.Client.Fullname,
                Description = project.Description,
                FinishedAt = project.FinishedAt,
                FreelancerFullName = project.Freelancer.Fullname,
                StartedAt = project.StartedAt,
                Title = project.Title,
                TotalCost = project.TotalCost
            };

            return projectDTO;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }

        public async Task<int> CreateProjectAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task CreateCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Project project)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task FinishProjectAsync(Project project)
        {
            //Dapper
            using (var sqlConn = new SqlConnection(_connectionString))
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, FinishedAt = @finishedAt WHERE Id = @id";

                await sqlConn.ExecuteAsync(script, new { status = project.Status, finishedAt = project.FinishedAt, project.Id });
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task StartProjectAsync(Project project)
        {
            //Dapper
            using (var sqlConn = new SqlConnection(_connectionString))
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConn.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, project.Id });
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _dbContext.Entry(project).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
