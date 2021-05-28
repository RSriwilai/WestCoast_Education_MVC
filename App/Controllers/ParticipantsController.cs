using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Entities;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly DataContext _context;

        public ParticipantsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Participants.ToListAsync();
            return View("index", result);
        }

        public IActionResult Create()
        {
            return View("create");
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ParticipantViewModel data)
        {

            var participant = new Participant {
                FirstName = data.FirstName,
                LastName = data.LastName,
                EmailAddress = data.EmailAddress,
                PhoneNumber = data.PhoneNumber,
                Address = data.Address
            };
            _context.Participants.Add(participant);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
