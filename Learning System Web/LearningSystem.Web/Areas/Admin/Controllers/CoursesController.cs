namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.BindingModels.Admin;
    using Models.DataModels;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using Services.Admin.Interfaces;
    using Infrastructure.Extensions;
    using Models.ViewModels.Admin.Courses;


    using static Common.Constants.WebConstants;

    public class CoursesController : AdminBaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminCourseService service;

        public CoursesController(UserManager<User> userManager, IAdminCourseService service)
        {
            this.userManager = userManager;
            this.service = service;

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new AddCourseBindingModel
            {
                StartDate = DateTime.UtcNow,
                EndtDate = DateTime.UtcNow.AddDays(30),
                Trainers = await this.GetUsersInTrainerRole()
            });
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var viewModel = await this.service.AllCoursesAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseBindingModel courseModels)
        {
            if (!ModelState.IsValid)
            {
                var trainers = await this.GetUsersInTrainerRole();
                courseModels.Trainers = trainers;

                return View(courseModels);
            }

            await this.service.CreateCourseAsync(courseModels);

            TempData.AddSuccessMessage($"Successfully added course {courseModels.Name}");

            return RedirectToAction(nameof(All));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var courseToEdit = await this.service.EditAsync(id);
            courseToEdit.Trainers = await this.GetUsersInTrainerRole();

            return View(courseToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseBinddingModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                courseModel.Trainers = await this.GetUsersInTrainerRole();
                return View(courseModel);
            }

            await this.service.EditAsync(courseModel);

            TempData.AddSuccessMessage($"Successfully edited of course {courseModel.Name}.");

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = await this.service.Delete(id);

            if (deleteModel == null)
            {
                TempData.AddErrorMessage("No such course.");

                return RedirectToAction(nameof(All));
            }
            return View(deleteModel);
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(DeleteViewModel deleteModel)
        {
            var isDestroyed = await this.service.IsDestroyedAsync(deleteModel);

            if (!isDestroyed)
            {
                TempData.AddErrorMessage("Error ocured when trying to delete course.");

                return RedirectToAction(nameof(All));
            }

            TempData.AddSuccessMessage($"Course {deleteModel.Name} deleted.");

            return RedirectToAction(nameof(All));
        }


        private async Task<IEnumerable<SelectListItem>> GetUsersInTrainerRole()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(TrainerRole);


            var trainerListItems = trainers.Select(t => new SelectListItem
            {
                Text = t.UserName,
                Value = t.Id
            }).ToList();

            return trainerListItems;
        }
    }
}