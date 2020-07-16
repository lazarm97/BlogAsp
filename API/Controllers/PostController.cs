using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.PostCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.PostQueries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public PostController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/Post
        [HttpGet]
        public IActionResult Get(
            [FromQuery] PostSearch search,
            [FromServices] IGetPostsQuery query)
        {
            try
            {
                return Ok(query.Execute(search));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetPostQuery query)
        {
            try
            {
                return Ok(query.Execute(id));
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Post
        [Authorize]
        [HttpPost]
        public IActionResult Post(
            [FromForm] PostDto dto,
            [FromServices] ICreatePostCommand command)
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

        // POST: api/Post/CreateComment
        [Authorize]
        [HttpPost]
        [Route("[Action]")]
        public IActionResult CreateComment(
            [FromBody] CommentDto dto,
            [FromServices] ICreateCommentCommand command)
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

        // POST: api/Post/AddVote
        [Authorize]
        [HttpPost]
        [Route("[Action]")]
        public IActionResult AddVote(
            [FromBody] VoteDto dto,
            [FromServices] IAddVoteCommand command)
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

        // PUT: api/Post/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id,
            [FromBody] PostDto dto,
            [FromServices] IEditPostCommand command)
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
            [FromServices] IDeletePostCommand command)
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
