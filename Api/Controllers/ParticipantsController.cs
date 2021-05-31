using System;
using System.Threading.Tasks;
using Api.Data;
using App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly DataContext _context;
        public ParticipantsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetParticipants()
        {
            var result = await _context.Participants.ToListAsync();
            return Ok(result);
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> GetParticipant(string email)
        {
            try
            {
                var participant = await _context.Participants.SingleOrDefaultAsync(c => c.EmailAddress.ToLower() == email.ToLower());

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
                _context.Participants.Add(participant);
                var result = await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Participant participantModel)
        {
            var participant = await _context.Participants.FindAsync(id);
            //STEG 2. Uppdatera de egenskaper ifrån steg 1 med värden ifrån modellen
            participant.FirstName = participantModel.FirstName;
            participant.LastName = participantModel.LastName;
            participant.EmailAddress = participantModel.EmailAddress;
            participant.PhoneNumber = participantModel.PhoneNumber;
            participant.Address = participantModel.Address;
            //Steg 3. Spara
            _context.Update(participant);
            var result = await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{firstName}")]
        public async Task<IActionResult> DeleteParticipant(string firstName)
        {
            try
            {
                var participant = await _context.Participants.SingleOrDefaultAsync(c => c.FirstName.ToLower() == firstName.ToLower());

                if(participant == null) return NotFound();

                _context.Participants.Remove(participant);
                var result = _context.SaveChangesAsync();

                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}