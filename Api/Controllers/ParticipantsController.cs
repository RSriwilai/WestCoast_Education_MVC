using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;

        public ParticipantsController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        public async Task<IActionResult> GetParticipants()
        {
            var result = await _unitOfWork.ParticipantRepository.GetParticipantAsync();

            var participants = new List<ParticipantViewModel>();

            if(result == null) return NotFound();

            foreach (var v in result)
            {
                participants.Add(new ParticipantViewModel
                {
                    Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    CourseTitle = v.Course.CourseTitle,
                    EmailAddress = v.EmailAddress,
                    PhoneNumber = v.PhoneNumber,
                    Address = v.Address

                });
            }

            return Ok(participants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById(int id)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.GetParticipantByIdAsync(id);

                if (participant == null) return NotFound();

                var model = new ParticipantViewModel
                {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                CourseTitle = participant.Course.CourseTitle,
                EmailAddress = participant.EmailAddress,
                PhoneNumber = participant.PhoneNumber,
                Address = participant.Address
                };

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("find/{email}")]
        public async Task<IActionResult> GetParticipant(string email)
        {
            try
            {
                // var participant = await _context.Participants.SingleOrDefaultAsync(c => c.EmailAddress.ToLower() == email.ToLower());
                var participant = await _unitOfWork.ParticipantRepository.GetParticipantByEmailAsync(email);

                if (participant == null) return NotFound();

                var model = new ParticipantViewModel
                {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                CourseTitle = participant.Course.CourseTitle,
                EmailAddress = participant.EmailAddress,
                PhoneNumber = participant.PhoneNumber,
                Address = participant.Address
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // [HttpPost()]
        // public async Task<IActionResult> AddParticipant(Participant participant)
        // {
        //     try
        //     {
        //         await _unitOfWork.ParticipantRepository.AddAsync(participant);

        //         if (await _unitOfWork.ParticipantRepository.SaveAllChangesAsync()) return StatusCode(201);

        //         return StatusCode(500, "Det gick inget vidare");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }

        [HttpPost()]
        public async Task<IActionResult> AddParticipant(AddParticipantViewModel model)
        {
            try
            {

                var course = await _unitOfWork.CourseRepository.GetCourseNameAsync(model.CourseTitle);

                if (course == null) return NotFound("Kunde inte hitta");

                var participant = new Participant
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Course = course,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address

                };
                await _unitOfWork.ParticipantRepository.AddAsync(participant);

                if (await _unitOfWork.ParticipantRepository.SaveAllChangesAsync()) return StatusCode(201);

                return StatusCode(500, "Det gick inget vidare!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
 


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, UpdateParticipantViewModel participantModel)
        {
            var participant = await _unitOfWork.ParticipantRepository.GetParticipantByIdAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            participant.FirstName = participantModel.FirstName;
            participant.LastName = participantModel.LastName;
            participant.EmailAddress = participantModel.EmailAddress;
            participant.PhoneNumber = participantModel.PhoneNumber;
            participant.Address = participantModel.Address;
            //Steg 3. Spara
            _unitOfWork.ParticipantRepository.Update(participant);
            var result = await _unitOfWork.ParticipantRepository.SaveAllChangesAsync();

            return NoContent();
        }

        //PROBLEM
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteParticipant(string email)
        {
            try
            {
                var participant = await _unitOfWork.ParticipantRepository.GetParticipantByEmailAsync(email);
                if (participant == null) return NotFound();

                _unitOfWork.ParticipantRepository.Delete(participant);
                var result = _unitOfWork.ParticipantRepository.SaveAllChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}