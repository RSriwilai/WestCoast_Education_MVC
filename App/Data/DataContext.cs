using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Course> Courses {get; set;}
        public DbSet<Participant> Participants {get; set;}
        public DbSet<CourseName> CourseNames { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}