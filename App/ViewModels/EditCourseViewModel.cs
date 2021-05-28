using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        
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