namespace Cumulative.Models
{

    /// <summary>
    /// Represents the Student entity in the database
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Primary key of the students table
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// First name of the student
        /// </summary>
        public string StudentFName { get; set; }

        /// <summary>
        /// Last name of the student
        /// </summary>
        public string StudentLName { get; set; }

        /// <summary>
        /// Student number of the student
        /// </summary>
        public string StudentNumber { get; set; }
        
        /// <summary>
        /// Date of enrollment of the student in the class
        /// </summary>
        public DateTime EnrolDate { get; set; }
    }
}
