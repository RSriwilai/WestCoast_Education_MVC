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
    public class ParticipantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _service;
        public ParticipantsController(IUnitOfWork unitOfWork, ICourseService service)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetCParticipantsAsync();
            return View("Index", result);
        }

        public async Task<IActionResult> Create()
        {
            var list = await _unitOfWork.CourseNameRepository.GetCourseNameAsync();
            return View("Create");
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ParticipantViewModel data)
        {
            if (!ModelState.IsValid) return View("Create", data);

            var participant = new ParticipantModel
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                CourseTitle = data.CourseTitle,
                EmailAddress = data.EmailAddress,
                PhoneNumber = (int)data.PhoneNumber,
                Address = data.Address
            };
            try
            {
                if(await _service.AddParticipant(participant)) return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View("Error");
            }

            return View("Error");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var participant = await _service.GetParticipantAsync(id);
            var model = new EditParticipantViewModel
            {
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
            try
            {
                var participant = await _service.GetParticipantAsync(data.Id);

                var participantModel = new UpdateParticipantViewModel
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    EmailAddress = data.EmailAddress,
                    PhoneNumber = data.PhoneNumber,
                    Address = data.Address
                };

                if (await _service.UpdateParticipant(data.Id, participantModel)) return RedirectToAction("Index");

                return RedirectToAction("Error");
            }
            catch (System.Exception)
            {
                return View("Error");                
            }
        }

        // [HttpGet("find/{email}")]
        public async Task<IActionResult> Details(string email)
        {
            var result = await _service.GetParticipantByEmailAsync(email);
            if(result != null) return Content($"Detta är detaljer på deltagare: {email}");

            return Content($"Kunde inte hitta kursen med email: {email}");
        }

        public async Task<IActionResult> Delete(string email)
        {
            if (await _service.DeleteParticipant(email)) return RedirectToAction("Index");
            return View("Error");
        }

    }
}
