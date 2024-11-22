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
        /// GET: api/student/ListStudents -> [{"studentId":1,"studentFName":"Sarah","studentLName":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18T00:00:00"},{"studentId":2,"studentFName":"Jennifer","studentLName":"Faulkner","studentNumber":"N1679","enrolDate":"2018-08-02T00:00:00"},{"studentId":3,"studentFName":"Austin","studentLName":"Simon","studentNumber":"N1682","enrolDate":"2018-06-14T00:00:00"},{"studentId":4,"studentFName":"Mario","studentLName":"English","studentNumber":"N1686","enrolDate":"2018-07-03T00:00:00"},{"studentId":5,"studentFName":"Elizabeth","studentLName":"Murray","studentNumber":"N1690","enrolDate":"2018-07-12T00:00:00"},{"studentId":6,"studentFName":"Kevin","studentLName":"Williams","studentNumber":"N1691","enrolDate":"2018-08-04T00:00:00"},{"studentId":7,"studentFName":"Jason","studentLName":"Freeman","studentNumber":"N1694","enrolDate":"2018-08-16T00:00:00"},{"studentId":8,"studentFName":"Nicole","studentLName":"Armstrong","studentNumber":"N1698","enrolDate":"2018-07-10T00:00:00"},{"studentId":9,"studentFName":"Colleen","studentLName":"Riley","studentNumber":"N1702","enrolDate":"2018-07-15T00:00:00"},{"studentId":10,"studentFName":"Julie","studentLName":"Salazar","studentNumber":"N1705","enrolDate":"2018-07-10T00:00:00"},{"studentId":11,"studentFName":"Dr.","studentLName":"Bridges","studentNumber":"N1709","enrolDate":"2018-08-22T00:00:00"},{"studentId":12,"studentFName":"Vanessa","studentLName":"Cox","studentNumber":"N1712","enrolDate":"2018-08-17T00:00:00"},{"studentId":13,"studentFName":"Denise","studentLName":"Jackson","studentNumber":"N1714","enrolDate":"2018-07-26T00:00:00"},{"studentId":14,"studentFName":"Roy","studentLName":"Davidson","studentNumber":"N1715","enrolDate":"2018-08-11T00:00:00"},{"studentId":15,"studentFName":"Ryan","studentLName":"Walters","studentNumber":"N1717","enrolDate":"2018-07-25T00:00:00"},{"studentId":16,"studentFName":"Patricia","studentLName":"Sweeney","studentNumber":"N1719","enrolDate":"2018-08-08T00:00:00"},{"studentId":18,"studentFName":"Melissa","studentLName":"Morales","studentNumber":"N1723","enrolDate":"2018-08-10T00:00:00"},{"studentId":19,"studentFName":"Kimberly","studentLName":"Johnson","studentNumber":"N1727","enrolDate":"2018-08-02T00:00:00"},{"studentId":20,"studentFName":"Andrea","studentLName":"Flores","studentNumber":"N1731","enrolDate":"2018-07-09T00:00:00"},{"studentId":21,"studentFName":"Jason","studentLName":"II","studentNumber":"N1732","enrolDate":"2018-06-05T00:00:00"},{"studentId":22,"studentFName":"David","studentLName":"Dunlap","studentNumber":"N1734","enrolDate":"2018-08-27T00:00:00"},{"studentId":23,"studentFName":"Elizabeth","studentLName":"Thompson","studentNumber":"N1736","enrolDate":"2018-08-07T00:00:00"},{"studentId":24,"studentFName":"Becky","studentLName":"Medina","studentNumber":"N1737","enrolDate":"2018-07-02T00:00:00"},{"studentId":25,"studentFName":"Wayne","studentLName":"Collins","studentNumber":"N1740","enrolDate":"2018-07-20T00:00:00"},{"studentId":26,"studentFName":"Nicole","studentLName":"Henderson","studentNumber":"N1742","enrolDate":"2018-06-07T00:00:00"},{"studentId":27,"studentFName":"David","studentLName":"Larson","studentNumber":"N1744","enrolDate":"2018-07-19T00:00:00"},{"studentId":28,"studentFName":"John","studentLName":"Reed","studentNumber":"N1748","enrolDate":"2018-08-15T00:00:00"},{"studentId":29,"studentFName":"Richard","studentLName":"King","studentNumber":"N1751","enrolDate":"2018-08-17T00:00:00"},{"studentId":30,"studentFName":"Alexander","studentLName":"Bennett","studentNumber":"N1752","enrolDate":"2018-07-29T00:00:00"},{"studentId":31,"studentFName":"Caitlin","studentLName":"Cummings","studentNumber":"N1756","enrolDate":"2018-08-02T00:00:00"},{"studentId":32,"studentFName":"Christine","studentLName":"Bittle","studentNumber":"N0001","enrolDate":"2020-10-05T00:00:00"}]
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
        /// GET: api/Student/FindStudent/3 -> {"studentId":3,"studentFName":"Austin","studentLName":"Simon","studentNumber":"N1682","enrolDate":"2018-06-14T00:00:00"}
        /// GET: api/Student/FindStudent/12 -> {"studentId":12,"studentFName":"Vanessa","studentLName":"Cox","studentNumber":"N1712","enrolDate":"2018-08-17T00:00:00"}
        /// GET: api/Student/FindStudent/19 -> {"studentId":19,"studentFName":"Kimberly","studentLName":"Johnson","studentNumber":"N1727","enrolDate":"2018-08-02T00:00:00"}
        /// GET: api/Student/FindStudent/30 -> {"studentId":30,"studentFName":"Alexander","studentLName":"Bennett","studentNumber":"N1752","enrolDate":"2018-07-29T00:00:00"}
        /// </example>
        /// <returns>
        /// All the information of one student.
        /// </returns>

        [HttpGet]
        [Route(template: "FindStudent/{StudentId}")]

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
