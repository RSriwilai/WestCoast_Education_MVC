using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{

    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _services;
        public CoursesController(IUnitOfWork unitOfWork, ICourseService services)
        {
            _services = services;
            _unitOfWork = unitOfWork;
        }

        //Action metod..

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetCoursesAsync();
            return View("Index", result);

        }


        //Steg 1. Returnerar inmatningssida "Create"
        [HttpGet()]
        public IActionResult Create()
        {
            var model = new CourseViewModel();
            return View("Create", model);
        }

        //Steg 3. Den här metoden skickas in via DataContext till SQLite databas
        [HttpPost()]
        public async Task<IActionResult> Create(CourseViewModel data)
        {

            if (!ModelState.IsValid) return View("Create", data);

            var course = new CourseModel
            {
                CourseNumber = (int)data.CourseNumber,
                CourseTitle = data.CourseTitle,
                CourseDescription = data.CourseDescription,
                CourseLength = (int)data.CourseLength,
                CourseComplexity = data.CourseComplexity,
                CourseStatus = data.CourseStatus
            };

            try
            {
                if(await _services.AddCourse(course)) return RedirectToAction("Index");                
            }
            catch (System.Exception)
            {
                return View("Error");
            }
            return View("Error");



            // _unitOfWork.CourseRepository.Add(course);
            // //Nu sparas det till databasen
            // if(await _unitOfWork.Complete()) return RedirectToAction("Index");

            // return View("Error");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _services.GetCourseAsync(id);
            var model = new EditCourseViewModel
            {
                Id = course.Id,
                CourseComplexity = course.CourseComplexity,
                CourseStatus = course.CourseStatus

            };
            return View("Edit", model);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(EditCourseViewModel data)
        {
            try
            {
                var course = await _services.GetCourseAsync(data.Id);

                var courseModel = new UpdateCourseViewModel
                {
                  CourseComplexity = data.CourseComplexity,
                  CourseStatus = data.CourseStatus
                };

                if (await _services.UpdateCourse(data.Id, courseModel)) return RedirectToAction("Index");

                return RedirectToAction("Error");
            }
            catch (System.Exception)
            {
                return View("Error");                
            }
        }


        // [HttpGet("find/{courseNo}")]
        public async Task<IActionResult> Details(int courseNo)
        {
            var result = await _services.GetCourseByNoAsync(courseNo);
            if(result != null) return Content($"Detta är detaljer på kursen: {courseNo}");

            return Content($"Kunde inte hitta kursen med kursnummer: {courseNo}");
        }

        public async Task<IActionResult> Delete(int courseNo)
        {
            if (await _services.DeleteCourse(courseNo)) return RedirectToAction("Index");
            return View("Error");
        }
    }
}

