using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class CourseNameRepository : ICourseNameRepository
    {
        private readonly DataContext _context;
        public CourseNameRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(CourseName courseName)
        {
            _context.Add(courseName);
        }

        public async Task<IEnumerable<CourseName>> GetCourseNameAsync()
        {
            return await _context.CourseNames.ToListAsync();
        }

    }
}