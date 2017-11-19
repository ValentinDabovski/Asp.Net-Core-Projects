
namespace CameraBazaar.Services.Interfaces
{
    using Models.ViewModels.Camera;
    using System.Collections.Generic;
    using Models.BindingModels.Camera;


    public interface ICameraControllerService : IService
    {
        IEnumerable<AllCamerasVIewModel> GetAllCameras();

        void Create(CreateCameraBindingModel model);
    }
}
