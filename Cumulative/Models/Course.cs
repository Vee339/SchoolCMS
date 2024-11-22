namespace Cumulative.Models
{
    /// <summary>
    /// Represents the courses table in the database
    /// </summary>
    public class Course
    {
        public int CourseId { get; set; }

        public string? CourseCode { get; set; }

        public int TeacherId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? CourseName { get; set; }
    }
}
