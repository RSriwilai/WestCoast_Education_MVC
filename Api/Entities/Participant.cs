using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CourseId {get; set;}
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }

        
        //Indikerar foreign key constraint
        [ForeignKey("CourseId")]
        public virtual Course Course {get; set;}
    }
}