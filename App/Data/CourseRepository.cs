using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        // Här skriver vi logik för vårt repository - VIKTIGT ATT ANVÄNDA ALLA METODER SOM FINNS I INTERFACE & Om man vill lägga till ett nytt metod skapa ett nytt Interface och implementera det!
        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<IEnumerable<Course>> GetCourseAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseByCourseNoAsync(int courseNo)
        {
            return await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNo);
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }


        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}