namespace LearningSystem.Models.ViewModels.Trainer
{
    using System;
    using DataModels;
    using Common.Mapping;
    using AutoMapper;


    public class TrainerAllCourses : IMapFrom<Course>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public int StudentsCount { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Course, TrainerAllCourses>()
                .ForMember(c => c.StudentsCount, cfg => cfg.MapFrom(c => c.Students.Count));
        }
    }
}
