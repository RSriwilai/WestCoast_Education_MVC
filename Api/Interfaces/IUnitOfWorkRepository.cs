using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        ICourseRepository CourseRepository {get;}
        IParticipantRepository ParticipantRepository {get;}
        ICourseNameRepository CourseNameRepository {get;}

        Task<bool> Complete();
        bool HasChanges();
    }
}