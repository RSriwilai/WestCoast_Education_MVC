using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ParticipantViewModel
    {
        [Display(Name = "Förnamn")]
        public string FirstName {get; set;}

        [Display(Name = "Efternamn")] 
        public string LastName {get; set;}

        [Display(Name = "E-post")] 
        public string EmailAddress {get; set;}

        [Display(Name = "Mobilnummer")] 
        public int PhoneNumber {get; set;}

        [Display(Name = "adress")] 
        public string Address  {get; set;} 
    }
}