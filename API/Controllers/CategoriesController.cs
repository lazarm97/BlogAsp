using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.CategoryCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public CategoriesController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult Get(
            [FromQuery] CategorySearch search,
            [FromServices] IGetCategoriesQuery query)
        {
            try
            {
                return Ok(executor.ExecuteQuery(query, search));
            }
            catch(EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Categories
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto,
            [FromServices] ICreateCategoryCommand command)
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

        // PUT: api/Categories/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, 
            [FromBody] CategoryDto dto,
            [FromServices] IEditCategoryCommand command)
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
            [FromServices] IDeleteCategoryCommand command)
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
