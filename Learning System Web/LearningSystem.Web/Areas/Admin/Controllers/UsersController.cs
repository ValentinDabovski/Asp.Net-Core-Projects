using LearningSystem.Web.Infrastructure.Extensions;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.BindingModels;
    using Models.DataModels;
    using Models.ViewModels.Admin;
    using Services.Admin.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;
    using static Infrastructure.WebConstants;

    [Area("Admin")]
    [Authorize(Roles = AdministratorRole)]

    public class UsersController : Controller
    {
        private readonly IAdminUserService service;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService service, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var users = await service.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new AdminUsersViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(UserAddToRoleBindingModel roleModel)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleModel.Role);
            var user = await this.userManager.FindByIdAsync(roleModel.UserId);
            var userExists = user != null;


            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details");
            }

            if (!ModelState.IsValid)
            {
                TempData.AddErrorMessage("Error.");
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, roleModel.Role);

            TempData.AddSuccessMessage($"Successfully added user {user.Name} to the role {roleModel.Role}");

            return RedirectToAction(nameof(Index));
        }
    }
}