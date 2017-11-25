using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using LearningSystem.Models.ViewModels.Admin;
using LearningSystem.Models.ViewModels.Admin.Courses;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearningSystem.Services.Admin.Implementations
{
    using Models.DataModels;
    using Data;
    using System.Threading.Tasks;
    using Models.BindingModels.Admin;
    using Interfaces;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public AdminCourseService(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task CreateCourseAsync(AddCourseBindingModel courseModel)
        {
            var course = new Course
            {
                EndtDate = courseModel.EndtDate,
                StartDate = courseModel.StartDate,
                Description = courseModel.Description,
                Name = courseModel.Name,
                TrainerId = courseModel.TrainerId
            };

            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdminAllCoursesViewModell>> AllCoursesAsync()
        {
            var allCoursesViewModel = await this.db.Courses
                .ProjectTo<AdminAllCoursesViewModell>()
                .ToListAsync();

            return allCoursesViewModel;
        }

        public async Task<EditCourseBinddingModel> EditAsync(int id)
        {
            var courseExists = await this.CourseExistsAsync(id);

            if (!courseExists)
            {
                return null;
            }

            var course = await this.db
                .Courses
                .Where(c => c.Id == id)
                .ProjectTo<EditCourseBinddingModel>()
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task EditAsync(EditCourseBinddingModel editModel)
        {
            var courseExists = await this.CourseExistsAsync(editModel.Id);
            var user = await this.GetUserByIdAsync(editModel.TrainerId);
            var userExists = user != null;


            if (courseExists && userExists)
            {
                var courseToEdit = await this.db.Courses.FindAsync(editModel.Id);

                courseToEdit.Description = editModel.Description;
                courseToEdit.EndtDate = editModel.EndtDate;
                courseToEdit.StartDate = editModel.StartDate;
                courseToEdit.Name = courseToEdit.Name;
                courseToEdit.TrainerId = courseToEdit.TrainerId;
                courseToEdit.Trainer = user;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task<DeleteViewModel> Delete(int id)
        {
            var courseExists = await this.CourseExistsAsync(id);

            if (!courseExists)
            {
                return null;
            }

            var deleteModel = await this.db.Courses
                .Where(c => c.Id == id)
                .ProjectTo<DeleteViewModel>()
                .FirstOrDefaultAsync();

            return deleteModel;
        }

        public async Task<bool> IsDestroyedAsync(DeleteViewModel courseDeleteModel)
        {
            var courseExists = await this.CourseExistsAsync(courseDeleteModel.Id);

            if (courseExists)
            {
                var courseToDelete = await this.db.Courses.FindAsync(courseDeleteModel.Id);

                this.db.Courses.Remove(courseToDelete);

                await this.db.SaveChangesAsync();

                return true;
            }

            return false;
        }


        private async Task<bool> CourseExistsAsync(int id)
        {
            return await this.db.Courses.AnyAsync(c => c.Id == id);
        }

        private async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            return user;
        }
    }
}
