using System;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantRepository _participantRepo;

        public ParticipantsController(IParticipantRepository participantRepo)
        {
            _participantRepo = participantRepo;
        }

        [HttpGet()]
        public async Task<IActionResult> GetParticipants()
        {
            var result = await _participantRepo.GetParticipantAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById(int id)
        {
            try
            {
                var participant = await _participantRepo.GetParticipantByIdAsync(id);

                if (participant == null) return NotFound();

                return Ok(participant);
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
                var participant = await _participantRepo.GetParticipantByEmailAsync(email);

                if(participant == null) return NotFound();
                return Ok(participant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddParticipant(Participant participant)
        {
            try
            {
                await _participantRepo.AddAsync(participant);

                if(await _participantRepo.SaveAllChangesAsync()) return StatusCode(201);

                return StatusCode(500, "Det gick inget vidare");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Participant participantModel)
        {
            var participant = await _participantRepo.GetParticipantByIdAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            participant.FirstName = participantModel.FirstName;
            participant.LastName = participantModel.LastName;
            participant.EmailAddress = participantModel.EmailAddress;
            participant.PhoneNumber = participantModel.PhoneNumber;
            participant.Address = participantModel.Address;
            //Steg 3. Spara
            _participantRepo.Update(participant);
            var result = await _participantRepo.SaveAllChangesAsync();
            
            return NoContent();
        }

        //PROBLEM
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteParticipant(string email)
        {
            try
            {
                var participant = await _participantRepo.GetParticipantByEmailAsync(email);
                if(participant == null) return NotFound();

                _participantRepo.Delete(participant);
                var result = _participantRepo.SaveAllChangesAsync();

                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}