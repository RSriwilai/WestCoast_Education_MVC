using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;

        //TODO - gör om till repository när det fungerar
        public CoursesController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet()]
        public async Task<IActionResult> GetCourse()
         {
             try
             {
                var result = await _unitOfWork.CourseRepository.GetCourseAsync();

                var courses = new List<CourseViewModel>();

                if (result == null) return NotFound();

                foreach (var v in result)
                {
                    courses.Add(new CourseViewModel
                    {
                    Id = v.Id,
                    CourseNumber = v.CourseNumber,
                    CourseTitle = v.CourseTitle,
                    CourseDescription = v.CourseDescription,
                    CourseLength = v.CourseLength,
                    CourseComplexity = v.CourseComplexity,
                    CourseStatus = v.CourseStatus
                    });
                }
                return Ok(courses);
             }
             catch (Exception ex)
             {
                return StatusCode(500, ex.Message);                 
             }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
             try
             {
                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);

                if (course == null) return NotFound();

                var model = new CourseViewModel
                {
                    Id = course.Id,
                    CourseNumber = course.CourseNumber,
                    CourseTitle = course.CourseTitle,
                    CourseDescription = course.CourseDescription,
                    CourseLength = course.CourseLength,
                    CourseComplexity = course.CourseComplexity,
                    CourseStatus = course.CourseStatus
                    
                };
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
                var course = await _unitOfWork.CourseRepository.GetCourseByCourseNoAsync(courseNo);

                if (course == null) return NotFound();

                var model = new CourseViewModel
                {
                    Id = course.Id,
                    CourseNumber = course.CourseNumber,
                    CourseTitle = course.CourseTitle,
                    CourseDescription = course.CourseDescription,
                    CourseLength = course.CourseLength,
                    CourseComplexity = course.CourseComplexity,
                    CourseStatus = course.CourseStatus
                    
                };
                return Ok(model);
             }
             catch (Exception ex)
             {
                return StatusCode(500, ex.Message);                 
             }

        }

        // [HttpPost()]
        // public async Task<IActionResult> AddCourse(Course course)
        // {
        //     try
        //     {
        //         // _context.Courses.Add(course);
        //         // var result = await _context.SaveChangesAsync();
        //         // await _courseRepo.AddAsync(course);

        //         await _unitOfWork.CourseRepository.AddAsync(course);

        //         if (await _unitOfWork.CourseRepository.SaveAllChangesAsync()) return StatusCode(201);

        //         return StatusCode(500, "Det gick inget vidare");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }

        // }

        [HttpPost()]
        public async Task<IActionResult> AddCourse(AddCourseViewModel model)
        {
            try
            {

                // var course = await _unitOfWork.CourseRepository.GetCourseNameAsync(course. ) GetCourseNameAsync(model.CourseTitle);

                // if (course == null) return NotFound("Kunde inte hitta");

                var course = new Course
                {
                    CourseNumber = model.CourseNumber,
                    CourseTitle = model.CourseTitle,
                    CourseDescription = model.CourseDescription,
                    CourseLength = model.CourseLength,
                    CourseComplexity = model.CourseComplexity,
                    CourseStatus = model.CourseStatus,

                };
                await _unitOfWork.CourseRepository.AddAsync(course);

                if (await _unitOfWork.CourseRepository.SaveAllChangesAsync()) return StatusCode(201);

                return StatusCode(500, "Det gick inget vidare!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //Kolla igenom put
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseViewModel courseModel)
        {
            //STEG 1. Hämta befintlig kurs med hjälp av inskickat id
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            course.CourseComplexity = courseModel.CourseComplexity;
            course.CourseStatus = courseModel.CourseStatus;
            //Steg 3. Spara
            _unitOfWork.CourseRepository.Update(course);
            var result = await _unitOfWork.CourseRepository.SaveAllChangesAsync();

            return NoContent();
        }

        [HttpDelete("{courseNo}")]
        public async Task<IActionResult> DeleteCourse(int courseNo)
        {
            try
            {
                // var course = await _.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);
                var course = await _unitOfWork.CourseRepository.GetCourseByCourseNoAsync(courseNo);
                if (course == null) return NotFound();

                _unitOfWork.CourseRepository.Delete(course);
                var result = _unitOfWork.CourseRepository.SaveAllChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}