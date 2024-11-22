using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;

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
    }
}
