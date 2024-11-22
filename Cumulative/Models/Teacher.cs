namespace Cumulative.Models
{
    /// <summary>
    /// Represent the teacher table in the database
    /// </summary>
    public class Teacher
    {
        // Primary key of teacher table
        public int TeacherId { get; set; }

        // First name of the teacher
        public string TeacherFName { get; set; }

        // Last name of the teacher
        public string TeacherLName { get; set; }

        // The employee number assigned the teacher
        public string EmployeeNumber { get; set; }

        // The date teacher was hired
        public DateTime HireDate { get; set; }
        
        // Salary of the teacher in decimal format
        public Decimal Salary { get; set; }
    }
}
