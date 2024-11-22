using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;
using System;
using MySql.Data.MySqlClient;

namespace Cumulative.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        // dependency injection of database context
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all the teachers in the database
        /// </summary>
        /// <example>
        /// GET api/teacher/ListTeachers -> [{"teacherId":1,"teacherFName":"Alexander","teacherLName":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":55.30},{"teacherId":2,"teacherFName":"Caitlin","teacherLName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77},{"teacherId":3,"teacherFName":"Linda","teacherLName":"Chan","employeeNumber":"T382","hireDate":"2015-08-22T00:00:00","salary":60.22},{"teacherId":4,"teacherFName":"Lauren","teacherLName":"Smith","employeeNumber":"T385","hireDate":"2014-06-22T00:00:00","salary":74.20},{"teacherId":5,"teacherFName":"Jessica","teacherLName":"Morris","employeeNumber":"T389","hireDate":"2012-06-04T00:00:00","salary":48.62},{"teacherId":6,"teacherFName":"Thomas","teacherLName":"Hawkins","employeeNumber":"T393","hireDate":"2016-08-10T00:00:00","salary":54.45},{"teacherId":7,"teacherFName":"Shannon","teacherLName":"Barton","employeeNumber":"T397","hireDate":"2013-08-04T00:00:00","salary":64.70},{"teacherId":8,"teacherFName":"Dana","teacherLName":"Ford","employeeNumber":"T401","hireDate":"2014-06-26T00:00:00","salary":71.15},{"teacherId":9,"teacherFName":"Cody","teacherLName":"Holland","employeeNumber":"T403","hireDate":"2016-06-13T00:00:00","salary":43.20},{"teacherId":10,"teacherFName":"John","teacherLName":"Taram","employeeNumber":"T505","hireDate":"2015-10-23T00:00:00","salary":79.63}]
        /// </example>
        /// <returns>
        /// A list of the teachers 
        /// </returns>
        /// 
        [HttpGet]
        [Route(template:"ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            // Create a list of instances of Teachers
            List<Teacher> Teachers = new List<Teacher>();

            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                // Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY;
                Command.CommandText = "SELECT * FROM teachers";

                // Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["teacherid"]);
                        string LastName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        Decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                        // Add the Teacher Data to the Teachers

                        Teacher CurrentTeacher = new Teacher()
                        {
                            TeacherId = Id,
                            TeacherFName = ResultSet["teacherfname"].ToString(),
                            TeacherLName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            HireDate = HireDate,
                            Salary = Salary
                        };

                        Teachers.Add(CurrentTeacher);

                    }
                }
            }
            return Teachers;
        }

        /// <summary>
        /// Receives a teacher id and returns the associated teacher information
        /// </summary>
        /// <param name="TeacherId">The primary key of the teachers table.</param>
        /// <example>
        /// GET api/teacher/GiveTeacherInfo/5 -> {"teacherId":5,"teacherFName":"Jessica","teacherLName":"Morris","employeeNumber":"T389","hireDate":"2012-06-04T00:00:00","salary":48.62}
        /// GET api/teacher/GiveTeacherInfo/2 -> {"teacherId":2,"teacherFName":"Caitlin","teacherLName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77}
        /// GET api/teacher/GiveTeacherInfo/8 -> {"teacherId":8,"teacherFName":"Dana","teacherLName":"Ford","employeeNumber":"T401","hireDate":"2014-06-26T00:00:00","salary":71.15}
        /// </example>
        /// <returns>
        /// All the information about one teacher
        /// </returns>

        [HttpGet]
        [Route(template:"GiveTeacherInfo/{TeacherId}")]
        public Teacher GiveTeacherInfo(int TeacherId)
        {
            Teacher SelectedTeacher = new Teacher();

            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                // Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY;
                Command.CommandText = $"SELECT * FROM teachers WHERE teacherid = {TeacherId}";
                //Command.CommandText = $"SELECT * FROM teachers WHERE teacherid = 4";

                // Gather Result Set of Query into the Teacher 
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the Result Set
                    while (ResultSet.Read())
                    {

                        // Setting the properties of SelectedTeacher to the values from the database
                        SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                        SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                        SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                        SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);

                    }
                }
            }
            
            return SelectedTeacher;
        }
    }
}
