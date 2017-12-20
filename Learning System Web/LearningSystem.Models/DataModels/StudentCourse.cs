namespace LearningSystem.Models.DataModels
{
    using Enums;

    public class StudentCourse
    {
        public string StudentId{ get; set; }

        public User Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public Grade? Grade { get; set; }
    }
}
