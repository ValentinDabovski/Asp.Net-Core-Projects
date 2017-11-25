using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Models.ViewModels.Admin.Users;


namespace LearningSystem.Services.Admin.Implementations
{
    using System;
    using System.Collections.Generic;
    using Models.ViewModels.Admin;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingViewModel>> AllAsync()
        {
            var allUsers = await this.db.Users
                     .ProjectTo<AdminUserListingViewModel>()
                     .ToListAsync();

            return allUsers;
        }
    }
}
