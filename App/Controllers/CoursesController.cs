using System;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    
    public class CoursesController : Controller
    {

        //Action metod..
        
        [HttpGet()]
        public IActionResult Index()
        {
            return View("Index");       
        }


        //Steg 1. Returnerar inmatningssida "Create"
        [HttpGet()]
        public IActionResult Create()
        {
            return View("Create");
        }

        //Steg 3. Den här metoden skickas in via DataContext till SQLite databas
        [HttpPost()]
        public IActionResult Create(CourseViewModel data)
        {

            //Steg 5. Spara till databasen(SQLite)
            return RedirectToAction("Index");
        }
    }
}