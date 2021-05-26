using System.ComponentModel.DataAnnotations;
using App.Models;

namespace App.ViewModels
{
    public class CourseViewModel
    {
       [Display(Name = "Kursnummer")]
        public int CourseNumber {get; set;} 

        [Display(Name = "Kurs Titel")]
        public string CourseTitle {get; set;} 

        [Display(Name = "Kursbeskrivning")]
        public string CourseDescription {get; set;} 

        [Display(Name = "Kurslängd")]
        public int CourseLength {get; set;} 

        [Display(Name = "Svårighetsgrad (nybörjare, medel, avancerad)")]
        public string CourseComplexity  {get; set;}

        [Display(Name = "Status (aktiv, pensionerad)")] 
        public string CourseStatus {get; set;} 

    }
}