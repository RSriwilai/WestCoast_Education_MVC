namespace Api.ViewModels
{
    public class AddCourseViewModel
    {
        public int CourseNumber { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public int CourseLength { get; set; }
        public string CourseComplexity { get; set; }
        public string CourseStatus { get; set; }
    }
}