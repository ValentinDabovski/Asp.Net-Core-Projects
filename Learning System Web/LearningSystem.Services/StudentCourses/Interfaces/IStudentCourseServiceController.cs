namespace LearningSystem.Services.StudentCourses.Interfaces
{
    using System.Threading.Tasks;
    using Models.ViewModels.StudentCourses;
    using System.Collections.Generic;

    public interface IStudentCourseServiceController
    {
        Task<IEnumerable<AllCoursesViewModel>> AllCoursesAsync();

        Task<bool> SignUpStudentForCourse(int courseId,string studentId);

        Task<string> GetCourseNameAsync(int courseId);

        Task<bool> StudentIsInCourse(int courseId, string studentId);

        Task<IEnumerable<StudentAllCoursesViewModel>> StudentAllCoursesAsync(string studenId);

        Task<bool> SignOutStudentFromCourse(int courseId, string studentId);

    }
}
