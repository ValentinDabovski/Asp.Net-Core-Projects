namespace LearningSystem.Services.Trainer.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.DataModels;
    using Models.ViewModels.Trainer;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TrainerControllerService : ITrainerControllerService
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public TrainerControllerService(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<TrainerAllCourses>> TrainerAllCoursesAsync(string trainerId)
        {
            var trainer = await this.GetTrainerById(trainerId);
            var trainerExists = trainer != null;

            if (!trainerExists)
            {
                return null;
            }

            return await this
                  .db
                  .Courses
                  .Where(c => c.TrainerId == trainerId)
                  .ProjectTo<TrainerAllCourses>()
                  .ToListAsync();
        }

        private async Task<User> GetTrainerById(string trainerId)
        {
            return await this
                .userManager
                .FindByIdAsync(trainerId);
        }

    }
}
