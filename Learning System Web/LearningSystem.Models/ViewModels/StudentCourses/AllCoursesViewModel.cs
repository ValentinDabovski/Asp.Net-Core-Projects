namespace LearningSystem.Models.ViewModels.StudentCourses
{
    using System;
    using Common.Mapping;
    using DataModels;

    public class AllCoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

    }
}
