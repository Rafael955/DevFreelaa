using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string query);

        Task<ProjectDetailsDTO> GetByIdAsync(int id);

        Task<int> CreateProjectAsync(Project project);

        Task CreateCommentAsync(ProjectComment comment);
    }
}
