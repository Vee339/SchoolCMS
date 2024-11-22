using Microsoft.AspNetCore.Mvc;
using Cumulative.Models;

namespace Cumulative.Controllers
{
    public class CoursePageController : Controller
    {

        private readonly CourseAPIController _api;

        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        // GET -> A webpage that returns the list of all the courses from the database

        [HttpGet]
        [Route(template:"CoursePage/List")]
        public IActionResult List()

        {
            List<Course> Courses = _api.ListCourses();

            return View(Courses);
        }

        // GET -> A webpage that returns all the information about a particular course 

        [HttpGet]
        [Route(template:"CoursePage/Show/{CourseId}")]

        public IActionResult Show(int CourseId)
        {
            Course SelectedCourse = _api.FindCourse(CourseId);
            return View(SelectedCourse);
        }
    }
}
