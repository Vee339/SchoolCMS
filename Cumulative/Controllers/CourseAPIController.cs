﻿using Cumulative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics.Contracts;

namespace Cumulative.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This API returns the list of all the courses in courses table of the database
        /// </summary>
        /// <example>
        /// GET: api/Course/ListCourses -> [{"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04T00:00:00","endDate":"2018-12-14T00:00:00","courseName":"Web Application Development"},{"courseId":2,"courseCode":"http5102","teacherId":2,"startDate":"2018-09-04T00:00:00","endDate":"2018-12-14T00:00:00","courseName":"Project Management"},{"courseId":3,"courseCode":"http5103","teacherId":5,"startDate":"2018-09-04T00:00:00","endDate":"2018-12-14T00:00:00","courseName":"Web Programming"},{"courseId":4,"courseCode":"http5104","teacherId":7,"startDate":"2018-09-04T00:00:00","endDate":"2018-12-14T00:00:00","courseName":"Digital Design"},{"courseId":5,"courseCode":"http5105","teacherId":8,"startDate":"2018-09-04T00:00:00","endDate":"2018-12-14T00:00:00","courseName":"Database Development"},{"courseId":6,"courseCode":"http5201","teacherId":2,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"Security & Quality Assurance"},{"courseId":7,"courseCode":"http5202","teacherId":3,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"Web Application Development 2"},{"courseId":8,"courseCode":"http5203","teacherId":4,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"XML and Web Services"},{"courseId":9,"courseCode":"http5204","teacherId":5,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"Mobile Development"},{"courseId":10,"courseCode":"http5205","teacherId":6,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"Career Connections"},{"courseId":11,"courseCode":"http5206","teacherId":8,"startDate":"2019-01-08T00:00:00","endDate":"2019-04-27T00:00:00","courseName":"Web Information Architecture"},{"courseId":12,"courseCode":"PHYS2203","teacherId":10,"startDate":"2019-09-04T00:00:00","endDate":"2019-12-14T00:00:00","courseName":"Massage Therapy"}]
        /// </example>
        /// <returns>
        /// Returns the list of course objects
        /// </returns>

        [HttpGet]
        [Route(template:"ListCourses")]

        public List<Course> ListCourses()
        {
            // Creating a list in which each course will be added by looping
            List<Course> Courses = new List<Course>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Creating a new command for storing our query
                MySqlCommand Command = Connection.CreateCommand();

                // Creating a query that would retrieve list of all the courses from our database
                Command.CommandText = "SELECT * FROM courses";

                // Gather result set of query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through each set of row
                    while (ResultSet.Read())
                    {
                        Course CurrentCourse = new Course();

                        CurrentCourse.CourseId = Convert.ToInt32(ResultSet["courseid"]);
                        CurrentCourse.CourseCode = ResultSet["coursecode"].ToString();
                        CurrentCourse.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        CurrentCourse.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                        CurrentCourse.EndDate = Convert.ToDateTime(ResultSet["finishdate"]);
                        CurrentCourse.CourseName = ResultSet["coursename"].ToString();

                        Courses.Add(CurrentCourse);
                    }
                }
            }

            return Courses;

        }

        [HttpGet]
        [Route(template: "FindCourse/{CourseId}")]

        public Course FindCourse(int CourseId)
        {
            Course SelectedCourse = new Course();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Opening the connection
                Connection.Open();

                // Establishing new command for our database
                MySqlCommand Command = Connection.CreateCommand();

                // Query to get particular row from the database
                Command.CommandText = $"SELECT * FROM courses WHERE courseid = {CourseId}";

                Command.Parameters.AddWithValue("@CourseId",CourseId);

                // Gather result set of row into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through the rows of ResultSet
                    while (ResultSet.Read())
                    {
                        SelectedCourse.CourseId = Convert.ToInt32(ResultSet["courseid"]);
                        SelectedCourse.CourseCode = ResultSet["coursecode"].ToString();
                        SelectedCourse.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedCourse.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                        SelectedCourse.EndDate = Convert.ToDateTime(ResultSet["finishdate"]);
                        SelectedCourse.CourseName = ResultSet["coursename"].ToString();
                    }
                }
            }
            return SelectedCourse;
        }

        /// <summary>
        /// This API Controller takes the information about a course and insert into the courses table in the database.
        /// </summary>
        /// <example>
        /// POST
        /// 
        /// Request body:
        /// 
        ///{"CourseCode":"Http3453","TeacherId":4,"StartDate": "2023-02-12","EndDate":"2024-05-10","CourseName":"Full-stack Development"}
        ///
        /// Response body:
        /// 
        /// "The course is added successfully"
        ///
        /// </example>
        /// <returns>
        /// string - "The course is added successfully"
        /// </returns>
        /// 

        [HttpPost(template: "AddCourse")]
        public string AddCourse([FromBody]Course NewCourse)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string query = "INSERT INTO courses(coursecode, teacherid, startdate, finishdate, coursename) VALUES(@code, @teacher, @start, @finish, @name)";


                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = query;

                Command.Parameters.AddWithValue("@code", NewCourse.CourseCode);
                Command.Parameters.AddWithValue("@teacher", NewCourse.TeacherId);
                Command.Parameters.AddWithValue("@start", NewCourse.StartDate);
                Command.Parameters.AddWithValue("@finish", NewCourse.EndDate);
                Command.Parameters.AddWithValue("@name", NewCourse.CourseName);

                Command.ExecuteNonQuery();



            }


            return $"The course is added successfully";
        }

        /// <summary>
        /// This API controller receives an id for a course and delete that course.
        /// </summary>
        /// <param name="CourseId">An integer representing the id of the course.</param>
        /// <example>
        /// GET: api/Courses/DeleteCourse/5 -> "Deleted the course with id 5 successfully"
        /// GET: api/Courses/DeleteCourse/8 -> "Deleted the course with id 8 successfully"
        /// GET: api/Course/DeleteCourse/5 -> "The course with id 5 does not exist or already has been deleted"
        /// </example>
        /// <return>
        /// Returns a string indicating the information whether the course is deleted or not
        /// </return>
        /// 

        [HttpDelete]
        [Route(template:"DeleteCourse/{CourseId}")]

        public string DeleteCourse(int CourseId)
        {
            using(MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string query = "DELETE FROM courses WHERE courseid = @id";

                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = query;

                Command.Parameters.AddWithValue("@id", CourseId);

                int RowsAffected = Command.ExecuteNonQuery();

                if(RowsAffected == 0)
                {
                    return $"The course with id {CourseId} does not exist or already has been deleted";

                }
                return $"Deleted the course with id {CourseId} successfully";
            }

        }

    }
}
