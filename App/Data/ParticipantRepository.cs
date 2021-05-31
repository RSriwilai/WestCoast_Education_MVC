using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DataContext _context;

        public ParticipantRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Participant participant)
        {
            _context.Participants.Add(participant);
        }

        public void Delete(Participant participant)
        {
            _context.Participants.Remove(participant);
        }

        public async Task<IEnumerable<Participant>> GetParticipantAsync()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<Participant> GetParticipantByIdAsync(int id)
        {
            return await _context.Participants.FindAsync(id);
        }

        public async Task<Participant> GetParticipantByEmailAsync(string participantEmail)
        {
            return await _context.Participants.SingleOrDefaultAsync(c => c.EmailAddress.ToLower() == participantEmail.ToLower());
        }


        public void Update(Participant participant)
        {
            _context.Participants.Update(participant);
        }
    }
}