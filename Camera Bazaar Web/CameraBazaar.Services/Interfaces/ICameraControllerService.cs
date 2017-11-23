namespace CameraBazaar.Services.Interfaces
{
    using Models.BindingModels.Camera;
    using Models.ViewModels.Camera;
    using System.Collections.Generic;
    using System.Threading.Tasks;



    public interface ICameraControllerService : IService
    {
        IEnumerable<AllCamerasViewModel> GetAllCameras(string search);

        void Create(CreateCameraBindingModel model);

        Task<DetailCameraView> Details(int id);

        Task<UserDetailsViewModel> GetUserDetail(string userId, string guestuserId);

        EditCameraBindingModels Edit(int id);

        void Edit(int id, EditCameraBindingModels cameraModel);

        CameraDeleteViewModel Delete(int? id);

        void Destroy(int id);

        bool UserCanDrestoryCamera(int? cameraId, string userId);
    }
}
