using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DataContext _context;

        public ParticipantRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Participant participant)
        {
            await _context.Participants.AddAsync(participant);

        }

        public void Delete(Participant participant)
        {
            _context.Participants.Remove(participant);
        }

        public async Task<IEnumerable<Participant>> GetParticipantAsync()
        {
            // return await _context.Participants.ToListAsync();
            return await _context.Participants.Include(c => c.Course).ToListAsync();
        }

        public async Task<Participant> GetParticipantByEmailAsync(string participantEmail)
        {
            var participant = await _context.Participants.Include(c => c.Course).SingleOrDefaultAsync(c => c.EmailAddress.ToLower() == participantEmail.ToLower());

            return participant;
    
        }

        public async Task<Participant> GetParticipantByIdAsync(int id)
        {
            // return await _context.Participants.FindAsync(id);
            return await _context.Participants.Include(c => c.Course).SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Participant participant)
        {
            _context.Participants.Update(participant);
        }
    }
}