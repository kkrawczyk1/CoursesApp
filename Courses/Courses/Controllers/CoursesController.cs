using System;
using System.Threading.Tasks;
using Courses.Commands;
using Courses.Interfaces;
using Courses.Models;
using Courses.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace Courses.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly Logger _logger;

        public CoursesController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        /// <summary>
        /// Zwraca kurs.
        /// </summary>
        /// <param name="id">Id kursu.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var course = await _queryBus.Send<GetCourseByIdQuery, CourseDTO>(new GetCourseByIdQuery(id));
                return Ok(course);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return NotFound();
            }
        }

        /// <summary>
        /// Zwraca wszystkie kursy.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(CourseDTO[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var courses = await _queryBus.Send<GetCoursesQuery, CourseDTO[]>(new GetCoursesQuery());
                return Ok(courses);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return NotFound();
            }
        }

        /// <summary>
        /// Tworzy nowy kurs.
        /// </summary>
        /// <param name="course">Model kursu.</param>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CourseDTO course)
        {
            try
            {
                await _commandBus.Send(new CreateCourseCommand(course));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return BadRequest();
            }
        }

        /// <summary>
        /// Edytuje kurs.
        /// </summary>
        /// <param name="course">Model kursu.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CourseDTO course)
        {
            try
            {
                await _commandBus.Send(new EditCourseCommand(id, course));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return BadRequest();
            }
        }

        /// <summary>
        /// Usuwa kurs.
        /// </summary>
        /// <param name="id">Id kursu.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _commandBus.Send(new DeleteCourseCommand(id));
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return BadRequest();
            }
        }
    }
}
