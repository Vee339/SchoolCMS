using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;

namespace Cumulative.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        // StudentPage/List -> A webpage that would display the list of the students from the database
        public IActionResult List()
        {

            List<Student> Students = _api.ListStudents();
            
            return View(Students);
        }

        // StudentPage/Show/{id} -> A webpage that shows all the information about a particular student on the basis of id
        [HttpGet]
        [Route(template:"StudentPage/Show/{StudentId}")]

        public IActionResult Show(int StudentId)
        {
            // Make a variable for the selected student
            Student SelectedStudent = _api.FindStudent(StudentId);


            return View(SelectedStudent);
        }
    }
}
