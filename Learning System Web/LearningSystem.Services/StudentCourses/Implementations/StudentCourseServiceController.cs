using System.Net;

namespace LearningSystem.Services.StudentCourses.Implementations
{
    using System.Linq;
    using Models.DataModels;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Data;
    using Models.ViewModels.StudentCourses;
    using Interfaces;

    public class StudentCourseServiceController : IStudentCourseServiceController
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public StudentCourseServiceController(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AllCoursesViewModel>> AllCoursesAsync()
        {
            return await this.db
                .Courses
                .ProjectTo<AllCoursesViewModel>()
                .ToListAsync();
        }

        public async Task<bool> SignUpStudentForCourse(int courseId, string userId)
        {
            var courseExists = await this.CourseExistsAsync(courseId);
            var student = await this.GetStudentByIdAsync(userId);
            var studentExists = student != null;

            if (!courseExists || !studentExists)
            {
                return false;
            }


            await this
              .db
              .StudentCourses
              .AddAsync(new StudentCourse()
              {
                  Course = await this.GetCourseByIdAsync(courseId),
                  CourseId = courseId,
                  Student = student,
                  StudentId = userId
              });

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StudentIsInCourse(int courseId, string studentId)
        {
            return await this
                .db
                .StudentCourses
                .AnyAsync(c => c.StudentId == studentId
                && c.CourseId == courseId);
        }

        public async Task<IEnumerable<StudentAllCoursesViewModel>> StudentAllCoursesAsync(string studenId)
        {
            var student = await this.GetStudentByIdAsync(studenId);
            var studentExist = student != null;

            if (!studentExist)
            {
                return null;
            }

            var courses = await this
                .db.
                StudentCourses
                .Where(s => s.StudentId == studenId)
                .Select(c => c.Course)
                .ProjectTo<StudentAllCoursesViewModel>()
                .ToListAsync();

            return courses;
        }


        public async Task<bool> SignOutStudentFromCourse(int courseId, string studentId)
        {
            var courseExist = await this.CourseExistsAsync(courseId);
            var student = await this.GetStudentByIdAsync(studentId);
            var studentExists = studentId != null;

            if (!courseExist || !studentExists)
            {
                return false;
            }


            var studenCourse = await this
                .db
                .StudentCourses
                .Where(sc => sc.StudentId == studentId
                && sc.CourseId == courseId)
                .FirstOrDefaultAsync();

            this.db.StudentCourses.Remove(studenCourse);

            await this.db.SaveChangesAsync();

            return true;
        }



        public async Task<string> GetCourseNameAsync(int courseId)
        {
            var courseName = await this.GetCourseByIdAsync(courseId);
            return courseName.Name;
        }


        private async Task<User> GetStudentByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }

        private async Task<Course> GetCourseByIdAsync(int id)
        {
            if (!await this.CourseExistsAsync(id))
            {
                return null;
            }
            return await this.db.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
        }


        private async Task<bool> CourseExistsAsync(int id)
        {
            return await this.db.Courses.AnyAsync(c => c.Id == id);
        }
    }
}
