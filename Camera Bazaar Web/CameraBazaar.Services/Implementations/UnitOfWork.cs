namespace CameraBazaar.Services.Implementations
{
    using Models.DataModels.Camera;
    using Interfaces;
    using Data;


    public class UnitOfWork : IUnitOfWork
    {
        private readonly CameraBazaarDbContext dbContext;

        private IRepository<Camera> cameras;

        public UnitOfWork(CameraBazaarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public IRepository<Camera> Cameras => this.cameras ?? (this.cameras = new Repository<Camera>(this.dbContext.Cameras));

        
        public int Commit()
        {
            return this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
          this.dbContext.Dispose();
        }
    }
}
