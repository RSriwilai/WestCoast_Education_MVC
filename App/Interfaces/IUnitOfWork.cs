using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository {get;}
        IParticipantRepository ParticipantRepository {get;}
        ICourseNameRepository CourseNameRepository {get;}

        Task<bool> Complete();
        bool HasChanges();
    }
}