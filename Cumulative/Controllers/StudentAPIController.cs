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
            List<Student> students = new List<Student>();

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

                        students.Add(CurrentStudent);
                    }
                }
            }

            return students;
        }
    }

}
