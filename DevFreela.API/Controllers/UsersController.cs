using DevFreela.Application.Commands.Users;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
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
            var user = new GetUserByIdQuery(id);

            var userViewModel = await _mediator.Send(user);

            if (userViewModel == null)
                return NotFound("Usuário não encontrado!");

            return Ok(userViewModel);
        }

        // api/users
        [HttpPost("create")]
        [AllowAnonymous]
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

        // api/users/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand login)
        {
            var loginUserViewModel = await _mediator.Send(login);

            if (loginUserViewModel == null)
                return BadRequest();
            
            return Ok(loginUserViewModel);
        }
    }
}
