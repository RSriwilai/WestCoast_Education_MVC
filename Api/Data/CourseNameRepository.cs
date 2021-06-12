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

        public async Task<CourseName> GetCourseNameAsync(string name)
        {
            return await _context.CourseNames.SingleOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}