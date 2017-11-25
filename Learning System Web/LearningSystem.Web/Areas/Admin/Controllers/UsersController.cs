using LearningSystem.Models.BindingModels.Admin;
using LearningSystem.Models.ViewModels.Admin.Users;
using Microsoft.AspNetCore.Html;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Models.DataModels;
    using Models.ViewModels.Admin;
    using Services.Admin.Interfaces;
    using System.Linq;
    using System.Threading.Tasks;


    public class UsersController : AdminBaseController
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
            var roleExists = await this.RoleExistsAsync(roleModel.Role);
            var user = await this.GetUserByIdAsync(roleModel.UserId);
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



            var result = await this.userManager.AddToRoleAsync(user, roleModel.Role);

            if (!result.Succeeded)
            {

                TempData.AddErrorMessage($"User {user.UserName} already in role '{roleModel.Role}'");

                return RedirectToAction(nameof(Index));
            }

            TempData.AddSuccessMessage($"Successfully added user {user.UserName} to the role {roleModel.Role}");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(UserAddToRoleBindingModel roleModel)
        {
            var roleExists = await this.RoleExistsAsync(roleModel.Role);
            var user = await this.GetUserByIdAsync(roleModel.UserId);
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

            var result = await this.userManager.RemoveFromRoleAsync(user, roleModel.Role);

            if (!result.Succeeded)
            {
                TempData.AddErrorMessage($"User {user.UserName} is not in role '{roleModel.Role}'");
                return RedirectToAction(nameof(Index));
            }

            TempData.AddSuccessMessage($"Successfully removed user {user.UserName} from role {roleModel.Role}");

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoleExistsAsync(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            return roleExists;
        }

        private async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            return user;
        }

    }
}