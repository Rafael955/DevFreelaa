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
            if(query == string.Empty)
                return await _dbContext.Projects.ToListAsync();
            else
                return await _dbContext.Projects.Where(x => x.Title.Contains(query)).ToListAsync();
        }

        public async Task<ProjectDetailsDTO> GetByIdAsync(int id)
        {
            //dapper
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                var script = $"SELECT Id, Title, Description, TotalCost, StartedAt, FinishedAt, ClientFullName, FreelancerFullName FROM Project WHERE Id = {id}";

                var result = await conn.QueryAsync<ProjectDetailsDTO>(script);

                return result.FirstOrDefault();
            }
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
