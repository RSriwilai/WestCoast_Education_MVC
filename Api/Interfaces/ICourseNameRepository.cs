using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseNameRepository
    {
        Task<CourseName> GetCourseNameAsync(string name);
    }
}