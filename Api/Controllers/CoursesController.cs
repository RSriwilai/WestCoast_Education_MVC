using System;
using System.Threading.Tasks;
using Api.Data;
using App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly DataContext _context;

        //TODO - gör om till repository när det fungerar
        public CoursesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            var result = await _context.Courses.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{courseNo}")]
        public async Task<IActionResult> GetCourse(int courseNo)
        {
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                var result = await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course courseModel)
        {
            //STEG 1. Hämta befintlig kurs med hjälp av inskickat id
            var course = await _context.Courses.FindAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            course.CourseComplexity = courseModel.CourseComplexity;
            course.CourseStatus = courseModel.CourseStatus;
            //Steg 3. Spara
            _context.Update(course);
            var result = await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{courseNo}")]
        public async Task<IActionResult> DeleteCourse(int courseNo)
        {
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);

                if(course == null) return NotFound();

                _context.Courses.Remove(course);
                var result = _context.SaveChangesAsync();

                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}