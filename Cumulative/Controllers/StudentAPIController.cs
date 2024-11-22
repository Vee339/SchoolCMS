using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;
using MySql.Data.MySqlClient;

namespace Cumulative.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        // dependency injection of database context

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This api returns the list of all the students in student table of the database
        /// </summary>
        /// <example>
        /// GET: api/student/ListStudents -> 
        /// </example>
        /// <returns>
        /// The list of information about all the students.
        /// </returns>
        /// 

        [HttpGet]
        [Route(template: "ListStudents")]

        public List<Student> ListStudents()
        {
            // Creating an empty list of students from Student class
            List<Student> Students = new List<Student>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Establishing a new command for our database
                MySqlCommand Command = Connection.CreateCommand();

                // Creating the sql query to get all the rows from student table
                Command.CommandText = "SELECT * FROM students";

                // Gather result set of query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                 { 
                    // Loop through each set of row
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["studentid"]);
                        string FirstName = ResultSet["studentfname"].ToString();
                        string LastName = ResultSet["studentlname"].ToString();
                        string StuNumber = ResultSet["studentnumber"].ToString();
                        DateTime DateOfEnrolment = Convert.ToDateTime(ResultSet["enroldate"]);

                        Student CurrentStudent = new Student()
                        {
                            StudentId = Id,
                            StudentFName = FirstName,
                            StudentLName = LastName,
                            StudentNumber = StuNumber,
                            EnrolDate = DateOfEnrolment
                        };

                        Students.Add(CurrentStudent);
                    }
                }
            }

            return Students;
        }

        /// <summary>
        /// This API finds the information of a student on the basis of id it receives
        /// </summary>
        /// <param id="StudentId">The perimeter is the unique id of the students table. Its data type is int.</param> 
        /// <example>
        /// GET: api/Student/FindStudent/3 ->
        /// GET: api/Student/FindStudent/12 ->
        /// GET: api/Student/FindStudent/19 ->
        /// GET: api/Student/FindStudent/30 ->
        /// </example>
        /// <returns>
        /// All the information of one student.
        /// </returns>

        [HttpGet]
        [Route(template: "FindStudent{StudentId}")]

        public Student FindStudent(int StudentId)
        {
            Student SelectedStudent = new Student();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open the connection with database
                Connection.Open();

                // Establish a new command for our database
                MySqlCommand Command = Connection.CreateCommand();

                // Query to get a particular row from the database
                Command.CommandText = $"SELECT * FROM students WHERE studentid = {StudentId}";

                // Gather Result set of query into the student
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        // Setting the properties of SelectedStudent to the values from the database
                        SelectedStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        SelectedStudent.StudentFName = ResultSet["studentfname"].ToString();
                        SelectedStudent.StudentLName = ResultSet["studentlname"].ToString();
                        SelectedStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                        SelectedStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                    }
                }
            }
            return SelectedStudent;
        }

    }

}
