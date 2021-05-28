using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    
    public class CoursesController : Controller
    {
        private readonly DataContext _context;

        public CoursesController(DataContext context)
        {
            _context = context;
        }

        //Action metod..

        [HttpGet()] 
        public async Task<IActionResult> Index()
        {
            var result = await _context.Courses.ToListAsync();
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
            _context.Courses.Add(course);
            //Nu sparas det till databasen
            var result = await _context.SaveChangesAsync();
            //Komma till våra kurser
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
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
            var course = await _context.Courses.FindAsync(data.Id);

            course.CourseComplexity = data.CourseComplexity;
            course.CourseStatus = data.CourseStatus;

            _context.Courses.Update(course);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details (int id)
        {
            return Content($"Detta är kursens detaljer {id}");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return Content($"Detta är kursens detaljer {id}");
        }
    }
}

