namespace LearningSystem.Models.ViewModels.Admin
{
    using Common.Mapping;
    using DataModels;


    public class AdminUserListingViewModel : IMapFrom<User>

    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
