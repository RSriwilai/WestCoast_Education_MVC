using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.ViewModels
{
    public class CourseViewModel
    {
        [Display(Name = "Kursnummer")]
        [Required(ErrorMessage = "Du måste ange kursnummer!")]
        public int? CourseNumber {get; set;} 

        [Display(Name = "Kurs Titel")]
        [Required(ErrorMessage = "Du måste ange kurs titeln!")]
        public string CourseTitle {get; set;} 

        [Display(Name = "Kursbeskrivning")]
        [Required(ErrorMessage = "Du måste ange kursbeskrivning!")]
        public string CourseDescription {get; set;} 

        [Display(Name = "Kurslängd")]
        [Required(ErrorMessage = "Du måste ange kurslängd!")]
        public int? CourseLength {get; set;} 

        [Display(Name = "Svårighetsgrad (Nybörjare, Medel, Avancerad)")]
        [Required]
        [RegularExpression("Nybörjare|Medel|Avancerad", ErrorMessage = "Du måste ange svårighetsgrad (Nybörjare, Medel, Avancerad)")]

        public string CourseComplexity  {get; set;}

        [Display(Name = "Status (Aktiv, Pensionerad)")] 
        [Required]
        [RegularExpression("Aktiv|Pensionerad", ErrorMessage = "Du måste ange status (Aktiv, Pensionerad)")]
        public string CourseStatus {get; set;} 

    }
}