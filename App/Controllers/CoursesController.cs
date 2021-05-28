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
            return View("Create");
        }

        //Steg 3. Den här metoden skickas in via DataContext till SQLite databas
        [HttpPost()]
        public async Task<IActionResult> Create(CourseViewModel data)
        {

            //Steg 5. Spara till databasen(SQLite)
            //Manuellt mappa viewmodel till entitet
            var course = new Course {
                CourseNumber = data.CourseNumber,
                CourseTitle = data.CourseTitle,
                CourseDescription = data.CourseDescription,
                CourseLength = data.CourseLength,
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
    }
}

