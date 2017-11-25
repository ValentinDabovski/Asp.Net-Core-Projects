using LearningSystem.Common.Mapping;
using LearningSystem.Models.DataModels;

namespace LearningSystem.Models.ViewModels.Admin.Users
{
    public class AdminUserListingViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
