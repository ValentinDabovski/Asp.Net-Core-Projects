using LearningSystem.Common.Mapping;
using LearningSystem.Models.DataModels;

namespace LearningSystem.Models.BindingModels.Admin
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstats;

    public class AddCourseBindingModel : IValidatableObject, IMapFrom<Course>
    {
        [Required]
        [MaxLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndtDate { get; set; }

        [Display(Name = "Trainer")]
        [Required]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start date should be in the future.");
            }

            if (this.StartDate > this.EndtDate)
            {
                yield return new ValidationResult("Start date should be before end date.");
            }
        }
    }
}
