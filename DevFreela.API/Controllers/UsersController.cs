﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // api/users
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel user) 
        {
            return CreatedAtAction(nameof(GetById), new { Id = 1 }, user);
        }

        // api/users/1/login
        [HttpPut("{id:int}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            return NoContent();
        }
    }
}
