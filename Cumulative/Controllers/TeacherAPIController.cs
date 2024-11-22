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
        /// Returns information about all the teachers
        /// </summary>
        /// <example>
        /// GET api/teacher/GiveTeachersList -> ["","","","",""]
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
        /// GET api/teacher/GiveTeacherInfo/5 -> {"":"","":"","":""} 
        /// </example>
        /// <returns>
        /// All the information about one teacher
        /// </returns>
      
        [HttpGet]
        [Route(template:"GiveTeacherInfo/{TeacherId}")]
        public Teacher GiveTeacherInfo(int TeacherId)
        {
            Console.WriteLine(TeacherId);
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
