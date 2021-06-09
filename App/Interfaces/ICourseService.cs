using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;
using App.ViewModels;

namespace App.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetCoursesAsync();
        Task<List<ParticipantModel>> GetCParticipantsAsync();
        Task<CourseModel> GetCourseAsync(int id);
        Task<CourseModel> GetCourseByNoAsync(int courseNo);
        Task<ParticipantModel> GetParticipantAsync(int id);
        Task<ParticipantModel> GetParticipantByEmailAsync(string email);


        Task<bool> AddCourse(CourseModel model);
        Task<bool> AddParticipant(ParticipantModel model);

        //Implentera!!
        Task<bool> UpdateCourse(int id, UpdateCourseViewModel model);
        Task<bool> UpdateParticipant(int id, UpdateParticipantViewModel model);

        Task<bool> DeleteCourse(int course);
        Task<bool> DeleteParticipant(string email);

    }
}