using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ParticipantViewModel
    {
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Du måste ange förnamn")]

        public string FirstName {get; set;}

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Du måste ange efternamn")] 
        
        public string LastName {get; set;}

        [Display(Name = "E-post")] 
        [Required(ErrorMessage = "Du måste ange E-post")]
        public string EmailAddress {get; set;}

        [Display(Name = "Mobilnummer")]
        [Required(ErrorMessage = "Du måste ange mobilnummer")] 
        public int? PhoneNumber {get; set;}

        [Display(Name = "Adress")]
        [Required(ErrorMessage = "Du måste ange adress")] 
        public string Address  {get; set;} 
    }
}