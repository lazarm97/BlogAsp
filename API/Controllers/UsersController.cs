using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.UserQueries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public UsersController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public IActionResult Get(
            [FromQuery] UserSearch search,
            [FromServices] IGetUsersQuery query)
        {
            try
            {
                return Ok(executor.ExecuteQuery(query, search));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetUserQuery query)
        {
            try
            {
                return Ok(executor.ExecuteQuery(query, id));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto,
            [FromServices] ICreateUserCommand command)
        {
            try
            {
                command.Execute(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Users/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] UserDto dto,
            [FromServices] IEditUserCommand command)
        {
            try
            {
                dto.Id = id;
                executor.ExecuteCommand(command, dto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] IDeleteUserCommand command)
        {
            try
            {
                executor.ExecuteCommand(command, id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
