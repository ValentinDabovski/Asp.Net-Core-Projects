using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSystem.Models.ViewModels.Admin.Users
{
    public class AdminUsersViewModel
    {
        public IEnumerable<AdminUserListingViewModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
