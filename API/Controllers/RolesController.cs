using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.RoleCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.RoleQueries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public RolesController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/Roles
        [HttpGet]
        public IActionResult Get(
            [FromQuery] RoleSearch search,
            [FromServices] IGetRolesQuery query)
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

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetRoleQuery query)
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

        // POST: api/Roles
        [Authorize]
        [HttpPost]
        public IActionResult Post(
            [FromBody] RoleDto dto,
            [FromServices] ICreateRoleCommand command)
        {
            try
            {
                executor.ExecuteCommand(command, dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Roles/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] RoleDto dto,
            [FromServices] IEditRoleCommand command)
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
            [FromServices] IDeleteRoleCommand command)
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
