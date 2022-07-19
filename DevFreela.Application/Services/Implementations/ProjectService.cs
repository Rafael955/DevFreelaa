using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var projectToDelete = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            projectToDelete.Cancel();

            _dbContext.SaveChanges();
        }

        public void Finish(int id)
        {
            var projectToFinish = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            projectToFinish.Finish();

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, FinishedAt = @finishedAt WHERE Id = @id";

                sqlConn.Execute(script, new { status = projectToFinish.Status, finishedAt = projectToFinish.FinishedAt, id });
            }

            _dbContext.SaveChanges();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            //Dapper
            using (var conn = new SqlConnection())
            {
                conn.Open();

                var script = "SELECT Id, Title, CreatedAt FROM Project";

                return conn.Query<ProjectViewModel>(script).ToList();
            }

            //Entity Framework
            //var projects = _dbContext.Projects;

            //var projectsViewModel = projects
            //    .Where(x => x.Title.Contains(query))
            //    .Select(x => new ProjectViewModel(x.Id, x.Title, x.CreatedAt))
            //    .ToList();

            //return projectsViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            //Dapper
            using (var conn = new SqlConnection())
            {
                conn.Open();

                var script = $"SELECT Id, Title, Description, TotalCost, StartedAt, FinishedAt, ClientFullName, FreelancerFullName FROM Project WHERE Id = {id}";

                return conn.Query<ProjectDetailsViewModel>(script).FirstOrDefault();
            }

            //Entity Framework
            //var project = _dbContext.Projects
            //    .Include(p => p.Client)
            //    .Include(p => p.Freelancer)
            //    .SingleOrDefault(x => x.Id == id);

            //var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt, project.Client.Fullname, project.Freelancer.Fullname);

            //return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            //Dapper
            var projectToStart = _dbContext.Projects.SingleOrDefault(x => x.Id == id);
            
            projectToStart.Start();
            
            using (var sqlConn = new SqlConnection())
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                sqlConn.Execute(script, new { status = projectToStart.Status, startedAt = projectToStart.StartedAt, id });
            }

            //Entity Framework
            //var projectToStart = _dbContext.Projects.SingleOrDefault(x => x.Id == id);

            //projectToStart.Finish();

            //_dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var projectToUpdate = _dbContext.Projects.SingleOrDefault(x => x.Id == inputModel.Id);

            if (projectToUpdate != null)
            {
                projectToUpdate.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

                _dbContext.Entry(projectToUpdate).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }
    }
}
