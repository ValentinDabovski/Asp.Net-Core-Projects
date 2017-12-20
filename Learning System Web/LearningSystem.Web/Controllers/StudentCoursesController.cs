using LearningSystem.Web.Areas.Trainer.Controllers;

namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.DataModels;
    using Services.StudentCourses.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Infrastructure.Extensions;
    using static Common.Constants.WebConstants;


    [Authorize]
    [Route("students/courses")]
    public class StudentCoursesController : Controller
    {
        private readonly IStudentCourseServiceController service;
        private readonly UserManager<User> userManager;

        public StudentCoursesController(IStudentCourseServiceController service, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> AllStudentCourses()
        {
            var studentId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.FindByIdAsync(studentId);
            var userIsInTrainerRole = await this.userManager.IsInRoleAsync(user, TrainerRole);

            if (userIsInTrainerRole)
            {
                return RedirectToAction(nameof(TrainerCoursesController.All), "TrainerCourses", new { area = "Trainer" });
            }

            var studentAllCourses = await this.service.StudentAllCoursesAsync(studentId);

            if (studentAllCourses == null)
            {
                TempData.AddErrorMessage("You are not sign up for any course.");
            }

            return View(studentAllCourses);
        }


        [HttpPost]
        [Route("signup/{id?}")]
        public async Task<IActionResult> SignUpStudent(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var studentAlreadyInCourse = await this.service.StudentIsInCourse(id, userId);

            if (studentAlreadyInCourse)
            {
                TempData.AddErrorMessage("You are already in this course");
                return RedirectToAction(nameof(ManageController.Index), "Manage", new { area = string.Empty });
            }

            var signedInSuccessfull = await this.service.SignUpStudentForCourse(id, userId);

            if (!signedInSuccessfull)
            {
                return BadRequest();
            }

            var courseName = await this.service.GetCourseNameAsync(id);

            TempData.AddSuccessMessage($"You have successfully signed up for course {courseName}");

            return RedirectToAction(nameof(ManageController.Index), "Manage", new { area = string.Empty });

        }

        [HttpPost]
        [Route("signout/{id?}")]
        public async Task<IActionResult> SignOutStudent(int id)
        {
            var studentId = this.userManager.GetUserId(this.User);
            var courseName = await this.service.GetCourseNameAsync(id);
            var studentIsInCourse = await this.service.StudentIsInCourse(id, studentId);

            if (!studentIsInCourse)
            {
                TempData.AddErrorMessage("You are not in this course");
                return RedirectToAction(nameof(ManageController.Index), "Manage", new { area = string.Empty });
            }


            var signOutFromCourseSuccessfull = await this.service.SignOutStudentFromCourse(id, studentId);

            if (!signOutFromCourseSuccessfull)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage($"Successfully signed out from course {courseName}");

            return RedirectToAction(nameof(ManageController.Index), "Manage", new { area = string.Empty });
        }
    }
}