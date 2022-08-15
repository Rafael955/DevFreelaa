using DevFreela.Application.Commands.Users;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly IUserService _userService;
        //private readonly IUserLoginService _loginService;
        private readonly IMediator _mediator;
        
        public UsersController(IMediator mediator)
        {
            //_userService = userService;
            //_loginService = loginService;
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        // api/users
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] NewUserCommand user) 
        {
            //if(!ModelState.IsValid)
            //{
            //    var messages = ModelState
            //        .SelectMany(x => x.Value.Errors)
            //        .Select(e => e.ErrorMessage)
            //        .ToList();

            //    return BadRequest(messages);
            //}


            await _mediator.Send(user);
            return CreatedAtAction(nameof(GetById), new { Id = 1 }, user);
        }

        // api/users/1/login
        //[HttpPut("{id:int}/login")]
        //public async Task<IActionResult> Login(int id, [FromBody] UserLoginInputModel login)
        //{
        //    var user = _userService.GetById(id);

        //    if (user == null)
        //        return NotFound("Usuário não existe");

        //    _loginService.Login(login);

        //    return NoContent();
        //}
    }
}
