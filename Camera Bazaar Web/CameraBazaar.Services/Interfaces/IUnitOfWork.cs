namespace CameraBazaar.Services.Interfaces
{
    using System;
    using Models.DataModels.Camera;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Camera> Cameras { get; }
        
        int Commit();
    }
}
