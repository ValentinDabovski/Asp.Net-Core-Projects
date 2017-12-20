using System.Collections.Generic;
using System.Threading.Tasks;
using LearningSystem.Models.ViewModels.Trainer;

namespace LearningSystem.Services.Trainer.Interfaces
{
    public interface ITrainerControllerService
    {
        Task<IEnumerable<TrainerAllCourses>> TrainerAllCoursesAsync(string trainerId);
    }
}
