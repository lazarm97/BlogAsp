using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.TagCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.Tags;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public TagsController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }

        // GET: api/Tags
        [HttpGet]
        public IActionResult Get(
            [FromQuery] TagSearch search,
            [FromServices] IGetTagsQuery query)
        {
            try
            {
                return Ok(_executor.ExecuteQuery(query, search));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetTagQuery query)
        {
            try
            {
                return Ok(_executor.ExecuteQuery(query, id));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Tags
        [Authorize]
        [HttpPost]
        public void Post(
            [FromBody] TagDto dto,
            [FromServices] ICreateTagCommand command)
        {
            _executor.ExecuteCommand(command, dto);
        }

        // PUT: api/Tags/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] TagDto dto,
            [FromServices] IEditTagCommand command)
        {
            try
            {
                dto.Id = id;
                _executor.ExecuteCommand(command, dto);
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
            [FromServices] IDeleteTagCommand command)
        {
            try
            {
                _executor.ExecuteCommand(command, id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
