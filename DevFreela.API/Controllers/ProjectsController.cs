using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        // api/projects?query=net core
        [HttpGet()]
        public IActionResult Get(string query)
        {
            var projects = _service.GetAll(query);
            // Buscar todos ou filtrar
            return Ok(projects);
        }
        
        // api/projects/3
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            // Buscar o projeto
            // return NotFound();

            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Post([FromBody] NewProjectInputModel project)
        {
            if(project.Title.Length > 50)
            {
                return BadRequest();
            }
            
            var id = _service.Create(project);

            return CreatedAtAction(nameof(GetById), new { id = id }, project);
        }

        // api/projects/2
        [HttpPut("update/{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel project)
        {
            if(project.Description.Length > 200)
            {
                return BadRequest();
            }

            _service.Update(project);

            //Atualizo o objeto
            return NoContent();
        }

        // api/project/3
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            //Buscar, se não existir, retornar NotFound()
            var project = GetById(id);

            if(project == null)
            {
                return NotFound();
            }

            _service.Delete(id);

            // Remover
            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id:int}/comments")]
        public IActionResult PostComment(int id, [FromBody]CreateCommentInputModel comment)
        {
            _service.CreateComment(comment);

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
