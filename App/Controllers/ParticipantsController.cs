using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParticipantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.ParticipantRepository.GetParticipantAsync();
            return View("index", result);
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

            var participant = new Participant
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                EmailAddress = data.EmailAddress,
                PhoneNumber = (int)data.PhoneNumber,
                Address = data.Address
            };
            _unitOfWork.ParticipantRepository.Add(participant);

            if (await _unitOfWork.Complete()) return RedirectToAction("Index");

            return View("Error");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var participant = await _unitOfWork.ParticipantRepository.GetParticipantByIdAsync(id);
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

            var participant = await _unitOfWork.ParticipantRepository.GetParticipantByIdAsync(data.Id);

            participant.FirstName = data.FirstName;
            participant.LastName = data.LastName;
            participant.EmailAddress = data.EmailAddress;
            participant.PhoneNumber = data.PhoneNumber;
            participant.Address = data.Address;

            _unitOfWork.ParticipantRepository.Update(participant);

            if (await _unitOfWork.Complete()) return RedirectToAction("Index");

            return RedirectToAction("Error"); ;
        }


        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _unitOfWork.ParticipantRepository.GetParticipantByIdAsync(id);

            _unitOfWork.ParticipantRepository.Delete(participant);

            if (await _unitOfWork.Complete()) return RedirectToAction("Index");
            return View("Error");
        }

    }
}
