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
        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Action metod..

        [HttpGet()] 
        public async Task<IActionResult> Index()
        {


            using var client = new HttpClient();

            //Utnyttja RestAPI
            var response = await client.GetAsync("https://localhost:5001/api/courses");
            
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<CourseModel>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View("Index", result); 
            }

            // var result = await _unitOfWork.CourseRepository.GetCourseAsync();
            return View("Index");
                  
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

            //Steg 5. Spara till databasen(SQLite)
            //Manuellt mappa viewmodel till entitet
            if(!ModelState.IsValid) return View("Create", data);
            

            var course = new Course {
                CourseNumber = (int)data.CourseNumber,
                CourseTitle = data.CourseTitle,
                CourseDescription = data.CourseDescription,
                CourseLength = (int)data.CourseLength,
                CourseComplexity = data.CourseComplexity,
                CourseStatus = data.CourseStatus
            };

            //placerar nu min entitet till EF ChangeTracking
            _unitOfWork.CourseRepository.Add(course);
            //Nu sparas det till databasen
            if(await _unitOfWork.Complete()) return RedirectToAction("Index");

            return View("Error");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
            var model = new EditCourseViewModel{
                Id = course.Id,
                CourseComplexity = course.CourseComplexity,
                CourseStatus = course.CourseStatus

            };
            return View("Edit", model);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(EditCourseViewModel data)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(data.Id);

            course.CourseComplexity = data.CourseComplexity;
            course.CourseStatus = data.CourseStatus;

            _unitOfWork.CourseRepository.Update(course);

            if(await _unitOfWork.Complete()) return RedirectToAction("Index");

            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);

            _unitOfWork.CourseRepository.Delete(course);

            if(await _unitOfWork.Complete()) return RedirectToAction("Index");
            return View("Error");
        }
    }
}

