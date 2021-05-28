using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class EditParticipantViewModel
    {
        public int Id { get; set; }
 
       [Display(Name = "Förnamn")]
        public string FirstName {get; set;}

        [Display(Name = "Efternamn")] 
        public string LastName {get; set;}

        [Display(Name = "E-post")] 
        public string EmailAddress {get; set;}

        [Display(Name = "Mobilnummer")] 
        public int PhoneNumber {get; set;}

        [Display(Name = "Adress")] 
        public string Address  {get; set;} 
    }
}