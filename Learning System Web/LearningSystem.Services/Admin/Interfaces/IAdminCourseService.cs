namespace LearningSystem.Services.Admin.Interfaces
{
    using System.Threading.Tasks;
    using Models.BindingModels.Admin;
    using System.Collections.Generic;
    using Models.ViewModels.Admin.Courses;

    public interface IAdminCourseService
    {
        Task CreateCourseAsync(AddCourseBindingModel courseModel);

        Task<IEnumerable<AdminAllCoursesViewModell>> AllCoursesAsync();

        Task<EditCourseBinddingModel> EditAsync(int id);

        Task EditAsync(EditCourseBinddingModel editModel);

        Task<DeleteViewModel> Delete(int id);

        Task<bool> IsDestroyedAsync(DeleteViewModel courseDeleteModel);
    }
}
