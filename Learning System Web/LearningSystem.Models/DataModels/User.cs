
namespace LearningSystem.Models.DataModels
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstats;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<Course> Trainigs { get; set; } = new List<Course>();

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
