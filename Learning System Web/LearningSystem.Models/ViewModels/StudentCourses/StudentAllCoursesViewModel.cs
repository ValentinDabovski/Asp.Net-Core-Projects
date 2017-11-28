namespace LearningSystem.Models.ViewModels.StudentCourses
{
    using AutoMapper;
    using Common.Mapping;
    using DataModels;

    public class StudentAllCoursesViewModel : AllCoursesViewModel, IHaveCustomMapping
    {
        public string TrainerName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Course, StudentAllCoursesViewModel>()
                .ForMember(vm => vm.TrainerName, cfg => cfg.MapFrom(c => c.Trainer.Name));
        }
    }
}
