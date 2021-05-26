using System;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class ParticipantsController : Controller
    {
        public IActionResult Index()
        {
            return View("index");
        }

        public IActionResult Create()
        {
            return View("create");
        }

        [HttpPost()]
        public IActionResult Create(ParticipantViewModel data)
        {

            return RedirectToAction("Index");
        }
    }
}