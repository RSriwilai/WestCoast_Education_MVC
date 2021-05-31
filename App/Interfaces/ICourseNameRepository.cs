using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;

namespace App.Interfaces
{
    public interface ICourseNameRepository
    {
        void Add(CourseName courseName);

        Task<IEnumerable<CourseName>> GetCourseNameAsync();
    }
}