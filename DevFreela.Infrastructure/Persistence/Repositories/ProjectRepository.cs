using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync(string query)
        {
            return await _dbContext.Projects.Where(x => x.Title.Contains(query))
                .ToListAsync();
        }

        public async Task<ProjectDetailsDTO> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection())
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
    }
}
