using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;

        //TODO - gör om till repository när det fungerar
        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            // var result = await _context.Courses.ToListAsync();
            var result = await _courseRepo.GetCourseAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _courseRepo.GetCourseByIdAsync(id);

                if (course == null) return NotFound();

                return Ok(course);
            }
            catch (Exception ex)
            {
                 return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find/{courseNo}")]
        public async Task<IActionResult> GetCourse(int courseNo)
        {
            try
            {
                // var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);
                var course = await _courseRepo.GetCourseByCourseNoAsync(courseNo);

                if(course == null) return NotFound();
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
                // _context.Courses.Add(course);
                // var result = await _context.SaveChangesAsync();
                await _courseRepo.AddAsync(course);

                if(await _courseRepo.SaveAllChangesAsync()) return StatusCode(201);

                return StatusCode(500, "Det gick inget vidare");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
            
        }


        //Kolla igenom put
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course courseModel)
        {
            //STEG 1. Hämta befintlig kurs med hjälp av inskickat id
            var course = await _courseRepo.GetCourseByIdAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            course.CourseComplexity = courseModel.CourseComplexity;
            course.CourseStatus = courseModel.CourseStatus;
            //Steg 3. Spara
            _courseRepo.Update(course);
            var result = await _courseRepo.SaveAllChangesAsync();

            return NoContent();
        }

        [HttpDelete("{courseNo}")]
        public async Task<IActionResult> DeleteCourse(int courseNo)
        {
            try
            {
                // var course = await _.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);
                var course = await _courseRepo.GetCourseByCourseNoAsync(courseNo);
                if(course == null) return NotFound();

                _courseRepo.Delete(course);
                var result = _courseRepo.SaveAllChangesAsync();

                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}