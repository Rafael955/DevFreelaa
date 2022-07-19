using DevFreela.Application.Commands.Project;
using DevFreela.Application.Queries;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet()]
        public IActionResult Get(string query)
        {
            var _query = new GetAllProjectsQuery(query);
            var projects = _mediator.Send(_query);

            // Buscar todos ou filtrar
            return Ok(projects);
        }

        // api/projects/3
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            if (command.Title.Length > 50)
            {
                return BadRequest();
            }

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/projects/2
        [HttpPut("update/{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectCommand project)
        {
            if (project.Description.Length > 200)
            {
                return BadRequest();
            }

            var command = new UpdateProjectCommand(id, project.Title, project.Description, project.TotalCost);
            _mediator.Send(command);

            //Atualizo o objeto
            return NoContent();
        }

        // api/project/3
        [HttpDelete("delete/{id:int}")]
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

        // api/projects/1/comments POST
        [HttpPost("{id:int}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_service.CreateComment(comment);
            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id:int}/start")]
        public IActionResult Start(int id)
        {
            _service.Start(id);

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        public IActionResult Finish(int id)
        {
            _service.Finish(id);

            return NoContent();
        }
    }
}
