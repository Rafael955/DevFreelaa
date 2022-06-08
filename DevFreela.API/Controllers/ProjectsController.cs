using DevFreela.API.Models;
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
        private readonly OpeningTimeOptions _option;

        public ProjectsController(IOptions<OpeningTimeOptions> option)
        {
            _option = option.Value;
        }

        // api/projects?query=net core
        [HttpGet()]
        public IActionResult Get(string query)
        {
            // Buscar todos ou filtrar
            return Ok();
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
        public IActionResult Post([FromBody] CreateProjectModel project)
        {
            if(project.Title.Length > 50)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        // api/projects/2
        [HttpPut("update/{id:int}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel project)
        {
            if(project.Description.Length > 200)
            {
                return BadRequest();
            }

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

            // Remover
            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id:int}/comments")]
        public IActionResult PostComment(int id, [FromBody]CreateCommentModel comment)
        {
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id:int}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id:int}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }
    }
}
