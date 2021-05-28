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
            var model = new ParticipantViewModel();
            return View("Create", model);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ParticipantViewModel data)
        {
            if(!ModelState.IsValid) return View("Create", data);

            var participant = new Participant {
                FirstName = data.FirstName,
                LastName = data.LastName,
                EmailAddress = data.EmailAddress,
                PhoneNumber = (int)data.PhoneNumber,
                Address = data.Address
            };
            _context.Participants.Add(participant);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
                var model = new EditParticipantViewModel{
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                EmailAddress = participant.EmailAddress,
                PhoneNumber = participant.PhoneNumber,
                Address = participant.Address
            };
            return View("Edit", model);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(EditParticipantViewModel data)
        {

            var participant = await _context.Participants.FindAsync(data.Id);

            participant.FirstName = data.FirstName;
            participant.LastName = data.LastName;
            participant.EmailAddress = data.EmailAddress;
            participant.PhoneNumber = data.PhoneNumber;
            participant.Address = data.Address;

            _context.Participants.Update(participant);
            var result = await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete (int id)
        {
            return Content($"Detta är kursens detaljer {id}");
        }

    }
}
