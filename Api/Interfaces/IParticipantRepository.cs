using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IParticipantRepository
    {
        Task AddAsync(Participant participant);
        
        // TASK SKAPAR ASYNKRONA METODER
        // 3 metoder för att hämta något
        Task<IEnumerable<Participant>> GetParticipantAsync();
        Task<Participant> GetParticipantByEmailAsync(string participantEmail);
        Task<Participant> GetParticipantByIdAsync(int id);

        // 2 metoder för att förändra något
        void Update(Participant participant);
        void Delete(Participant participant);

        Task<bool> SaveAllChangesAsync();
    }
}