using DevFreela.Application.Commands.Project;
using DevFreela.Application.Commands.Projects;
using DevFreela.Application.Queries;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        //private readonly IProjectService _service;
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
           //_service = service;
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(string query = "")
        {
            var _query = new GetAllProjectsQuery(query);
            var projects = await _mediator.Send(_query);

            // Buscar todos ou filtrar
            return Ok(projects);
        }

        // api/projects/3
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, freelancer")]
        public IActionResult GetById(int id)
        {
            // Buscar o projeto
            var query = new GetProjectByIdQuery(id);
            var project = _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost("create")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            //if (!ModelState.IsValid)
            //{
            //    var messages = ModelState
            //        .SelectMany(x => x.Value.Errors)
            //        .Select(e => e.ErrorMessage)
            //        .ToList();

            //    return BadRequest(messages);
            //}

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/projects/2
        [HttpPut("update/{id:int}")]
        [Authorize(Roles = "client")]
        public IActionResult Put(int id, [FromBody] UpdateProjectCommand project)
        {
            if (project.Description.Length > 200)
            {
                return BadRequest();
            }

            var command = new UpdateProjectCommand
            {
                Id = id,
                Title = project.Title,
                Description = project.Description,
                TotalCost = project.TotalCost
            };

            _mediator.Send(command);

            //Atualizo o objeto
            return NoContent();
        }

        // api/project/3
        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            //Buscar, se não existir, retornar NotFound()
            var project = GetById(id);

            if (project == null)
                return NotFound();

            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);

            // Remover
            return NoContent();
        }

        // api/projects/1/create-comment POST
        [HttpPost("{id:int}/create-comment")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            var project = await GetProject(id);

            if (project == null)
                NotFound("Projeto não foi encontrado. Não será possível adicionar comentário!");

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id:int}/start")]
        [Authorize(Roles = "client")]
        public IActionResult Start(int id)
        {
            var startProject = new StartProjectCommand(id);
            _mediator.Send(startProject);

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand finishProjectCommand)
        {
            var finishProject = new FinishProjectCommand();
            
            finishProject.Id = id;
            finishProject.CreditCardNumber = finishProjectCommand.CreditCardNumber;
            finishProject.Cvv = finishProjectCommand.Cvv;
            finishProject.ExpiresAt = finishProjectCommand.ExpiresAt;
            finishProject.Fullname = finishProjectCommand.Fullname;

            var result = await _mediator.Send(finishProject);

            if (result == System.Net.HttpStatusCode.BadRequest)
                return BadRequest(new { message = "Erro ao finalizar projeto!" });

            return NoContent();
        }

        private async Task<ProjectDetailsViewModel> GetProject(int id)
        {
            // Buscar o projeto
            var query = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
                return null;

            return project;
        }
    }
}
