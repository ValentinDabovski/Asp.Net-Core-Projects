using System;
using System.Collections.Generic;
using AutoMapper;
using LearningSystem.Common.Mapping;
using LearningSystem.Models.DataModels;

namespace LearningSystem.Models.ViewModels.Admin.Courses
{
    public class AdminAllCoursesViewModell : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TrainerName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public ICollection<StudentCourse> Students { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Course, AdminAllCoursesViewModell>()
                .ForMember(viewModel => viewModel.TrainerName, cfg => cfg.MapFrom(course => course.Trainer.Name));
        }
    }
}
