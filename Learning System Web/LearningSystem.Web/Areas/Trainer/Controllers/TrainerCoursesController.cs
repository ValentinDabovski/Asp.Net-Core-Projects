namespace LearningSystem.Web.Areas.Trainer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class TrainerCoursesController : Controller
    {
        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}