using System.Linq;
using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Models.DataModels;
using LearningSystem.Models.Enums;

namespace LearningSystem.Models.ViewModels.Trainer
{
    public class ManageStudentsInCourseViewModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string StudentUserName { get; set; }

        public string StudentEmail { get; set; }

        public string StudentId { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            int courseId = default(int);

            profile.CreateMap<User, ManageStudentsInCourseViewModel>()
                .ForMember(s => s.Grade,
                    cfg => cfg.MapFrom(u =>
                        u.Courses.Where(c => c.CourseId == courseId).Select(c => c.Grade).FirstOrDefault()));
        }
    }
}
