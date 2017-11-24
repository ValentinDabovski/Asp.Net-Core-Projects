namespace LearningSystem.Services.Admin.Interfaces
{
    using Models.ViewModels.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingViewModel>> AllAsync();
    }
}
