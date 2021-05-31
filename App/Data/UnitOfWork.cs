using System.Threading.Tasks;
using App.Interfaces;

namespace App.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
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