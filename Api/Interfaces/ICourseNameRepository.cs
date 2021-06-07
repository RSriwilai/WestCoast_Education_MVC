using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseNameRepository
    {
        
        Task Add(CourseName courseName);

        Task<IEnumerable<CourseName>> GetCourseNameAsync();

        Task<bool> SaveAllChanges();
    }
}