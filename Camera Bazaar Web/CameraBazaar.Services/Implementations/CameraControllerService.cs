namespace CameraBazaar.Services.Implementations
{
    using Interfaces;
    using Models.BindingModels.Camera;
    using Models.DataModels.Camera;
    using Models.Enums.LightMetering;
    using Models.Intefaces.Camera;
    using Models.ViewModels.Camera;
    using System.Collections.Generic;
    using System.Linq;


    public class CameraControllerService : ICameraControllerService
    {
        private IUnitOfWork unitOfWork;
     

        public CameraControllerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<AllCamerasVIewModel> GetAllCameras()
        {
            IEnumerable<ICamera> allCameras = this.unitOfWork.Cameras.GetAll();

           return allCameras.Select(c => new AllCamerasVIewModel()
            {
                Make = c.Make,
                Model = c.Model,
                ImageUrl = c.ImageUrl,
                IsInStock = c.Quantity > 0,
                Price = c.Price
            });
        }

        public void Create(CreateCameraBindingModel model)
        {
            if (model != null)
            {
                this.unitOfWork.Cameras.AddAsync(new Camera()
                {
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    VideoResolution = model.VideoResolution,
                    IsFullFrame = model.IsFullFrame,
                    LightMetering = (LightMetering)model.LightMeterings.Cast<int>().Sum(),
                    Make = model.Make,
                    MaxIso = model.MaxIso,
                    MaxShutterSpeed = model.MaxShutterSpeed,
                    MinIso = model.MinIso,
                    MinShutterSpeed = model.MinShutterSpeed,
                    Model = model.Model,
                    Price = model.Price,
                    UserId = model.UserId,
                    Quantity = model.Quantity,
                });
                this.unitOfWork.Commit();
            }

        }
    }
}
