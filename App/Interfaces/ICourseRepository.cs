using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;

namespace App.Interfaces
{
    public interface ICourseRepository
    {
        // I denna Interfacen ska vi dekorera vilka metoder det är som behövs för att jobba med våra applikation


        // Metod för att lägga till en kurs
        void Add(Course course);
        
        // TASK SKAPAR ASYNKRONA METODER
        // 3 metoder för att hämta något
        Task<IEnumerable<Course>> GetCourseAsync();
        Task<Course> GetCourseByCourseNoAsync(int courseNo);
        Task<Course> GetCourseByIdAsync(int id);

        // 2 metoder för att förändra något
        void Update(Course course);
        void Delete(Course course);

    }
}