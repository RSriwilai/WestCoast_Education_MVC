using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class CourseNameRepository : ICourseNameRepository
    {
        private readonly DataContext _context;

        public CourseNameRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(CourseName courseName)
        {
            await _context.CourseNames.AddAsync(courseName);
        }

        public async Task<IEnumerable<CourseName>> GetCourseNameAsync()
        {
            return await _context.CourseNames.ToListAsync();
        }

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}