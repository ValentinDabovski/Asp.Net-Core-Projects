namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.StudentCourses.Interfaces;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IStudentCourseServiceController service;

        public HomeController(IStudentCourseServiceController service)
        {
            this.service = service;
        }

        public async Task<IActionResult>  Index()
        {
            var allCourses = await this.service.AllCoursesAsync();

            return View(allCourses);
        }
    }
}
