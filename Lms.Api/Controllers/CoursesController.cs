using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public CoursesController(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCource(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {
            var course = await uow.CourseRepository.FindAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }

            var courseDto = mapper.Map<CourseDto>(course);
            
            patchDocument.ApplyTo(courseDto, ModelState);

            if (!TryValidateModel(courseDto))
            {
                return BadRequest();
            }

            
            mapper.Map(courseDto, course);

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(courseId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }




            return Ok();

        }


        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse(bool includeModules)
        {            
            return Ok(mapper.Map<IEnumerable<CourseDto>>(await uow.CourseRepository.GetAllCourses(includeModules)));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {            
            var course = await uow.CourseRepository.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return mapper.Map<CourseDto>(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto courseDto)
        {            
            if (!TryValidateModel(courseDto))
            {
                return BadRequest();
            }

            Course c = mapper.Map<Course>(courseDto);
            c.Id = id;

            uow.CourseRepository.Update(c);

            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);                    
                }
            }

            return Ok();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto courseDto)
        {
            if (!TryValidateModel(courseDto))
            {
                return BadRequest();
            }

            Course course = mapper.Map<Course>(courseDto);
            
            uow.CourseRepository.Add(course);
            await uow.CompleteAsync();
                       

            return CreatedAtAction("GetCourse", new { id = course.Id }, mapper.Map<CourseDto>(course));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await uow.CourseRepository.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            uow.CourseRepository.Remove(course);
            await uow.CompleteAsync();

            return Ok();
        }

        private async Task<bool> CourseExists(int id)
        {
            return await uow.CourseRepository.AnyAsync(id);
            
        }
    }
}
