using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSystem.Models.ViewModels.Admin
{
    public class AdminUsersViewModel
    {
        public IEnumerable<AdminUserListingViewModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
