namespace LearningSystem.Models.ViewModels.Admin.Courses
{
    using Common.Mapping;
    using DataModels;

    public class DeleteViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
