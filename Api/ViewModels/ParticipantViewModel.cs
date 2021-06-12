namespace Api.ViewModels
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CourseTitle {get; set;}
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}