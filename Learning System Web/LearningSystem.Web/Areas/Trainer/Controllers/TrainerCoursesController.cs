namespace LearningSystem.Web.Areas.Trainer.Controllers
{
    using Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Models.DataModels;
    using Services.Trainer.Interfaces;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static Common.Constants.WebConstants;

    [Area(TrainerArea)]
    [Authorize(Roles = TrainerRole)]
    [Route("trainer/courses")]
    public class TrainerCoursesController : Controller
    {
        private readonly ITrainerControllerService service;
        private readonly UserManager<User> userManager;

        public TrainerCoursesController(ITrainerControllerService service, UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route(nameof(All))]
        public async Task<IActionResult> All()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager.FindByIdAsync(userId);

            var userInTrainerRole = await this.userManager.IsInRoleAsync(user, TrainerRole);

            if (!userInTrainerRole)
            {
                TempData.AddErrorMessage("Access Denied.");
                RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            
            var trainersAllCourses = await this.service.TrainerAllCoursesAsync(userId);

            if (trainersAllCourses == null)
            {
                 TempData.AddErrorMessage("You have no courses yet.");
                return View();
            }

            return View(trainersAllCourses);
        }

        [HttpGet]
        [Route("manage/{id?}")]
        public async Task<IActionResult> ManageStudents(int id)
        {
            return View();
        }

    }
}