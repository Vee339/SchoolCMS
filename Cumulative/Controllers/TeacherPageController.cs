using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;
using System.Diagnostics;

namespace Cumulative.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }




       // TeacherPage/List -> A webpage that shows all teachers in the database
        public IActionResult List()
        {
            // creating a variable for the list of teachers
            List<Teacher> Teachers = _api.ListTeachers();

            // direct us to the /Views/TeacherPage/List.cshtml
            return View(Teachers);
        }

        // GET : TeacherPage/Show/{id} -> A webpage that shows a particular teacher in the database matching the given id
        [HttpGet]
        [Route("TeacherPage/Show/{TeacherId}")]
        public IActionResult Show(int TeacherId)
        {
            
            // Creating a Teacher instance for the teacher information received upon inserting the id
            Teacher SelectedTeacher = _api.GiveTeacherInfo(TeacherId);


            return View(SelectedTeacher);
           
        }

        // GET: TeacherPage/New -> A webpage that allows the user to add a new teacher into the database

        [HttpGet]
        [Route("TeacherPage/New")]
        public IActionResult New()
        {
            // Direct the user to /Views/TeacherPage/New.cshtml
            return View();
        }

        // POST: ArticlePage/Create -> 
        // Request Header: Content-type: application/x-www-url-formencoded
        // Request Body:
        // TeacherFName = {first_name}, TeacherLName = {last_name}, EmployeeNumber={employee_number}, HireDate={hire_date}, Salary={salary}

        [HttpPost]
        [Route("TeacherPage/Create")]
        public IActionResult Create(string first_name, string last_name, string employee_number, DateOnly hire_date, decimal salary)
        {
            //Debug.WriteLine($"First Name: {first_name}");
            //Debug.WriteLine($"Last Name: {last_name}");
            //Debug.WriteLine($"Employee Number: {employee_number}");
            //Debug.WriteLine($"Hire Date: {hire_date}");
            //Debug.WriteLine($"Salary: {salary}");
            Teacher AddedTeacher = new Teacher();

            AddedTeacher.TeacherFName = first_name;
            AddedTeacher.TeacherLName = last_name;
            AddedTeacher.EmployeeNumber = employee_number;
            AddedTeacher.Salary = salary;

            _api.AddTeacher(AddedTeacher);

            // direct the user to the create view
            return View();
        }

        [HttpGet]
        [Route("TeacherPage/ConfirmDelete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            Teacher SelectedTeacher = _api.GiveTeacherInfo(id);

            return View(SelectedTeacher);
        }

        [HttpGet]
        [Route("TeacherPage/Delete/{id}")]

        public IActionResult Delete(int id)
        {
            _api.DeleteTeacher(id);

            return View();
        }
    }
}
