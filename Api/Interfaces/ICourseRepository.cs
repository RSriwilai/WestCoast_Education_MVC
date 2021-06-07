using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course);
        
        Task<IEnumerable<Course>> GetCourseAsync();
        Task<Course> GetCourseByCourseNoAsync(int courseNo);
        Task<Course> GetCourseByIdAsync(int id);


        // 2 metoder för att förändra något
        void Update(Course course);
        void Delete(Course course);

        Task<bool> SaveAllChangesAsync();
    }
}