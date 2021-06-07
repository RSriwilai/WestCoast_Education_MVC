using System.Threading.Tasks;
using Api.Interfaces;

namespace Api.Data
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly DataContext _context;
        public UnitOfWorkRepository(DataContext context)
        {
            _context = context;
        }

        public ICourseRepository CourseRepository => new CourseRepository(_context);

        public IParticipantRepository ParticipantRepository => new ParticipantRepository(_context);

        public ICourseNameRepository CourseNameRepository => new CourseNameRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}



 