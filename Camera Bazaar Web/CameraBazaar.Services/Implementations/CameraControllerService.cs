namespace CameraBazaar.Services.Implementations
{
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models.BindingModels.Camera;
    using Models.DataModels.Camera;
    using Models.DataModels.Identity;
    using Models.Enums.LightMetering;
    using Models.ViewModels.Camera;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Extensions;
    using System.Threading.Tasks;



    public class CameraControllerService : ICameraControllerService
    {
        private IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public CameraControllerService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public IEnumerable<AllCamerasViewModel> GetAllCameras(string search)
        {
            if (search != null)
            {
                var serchedCameras =
                    this.unitOfWork.Cameras.Filter(c => c.Make.ToString().ToLower() == search.ToLower());

                return serchedCameras.Select(c => new AllCamerasViewModel()
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    IsInStock = c.Quantity > 0,
                    Price = c.Price
                });
            }

            var allCameras = this.unitOfWork.Cameras.GetAll();

            return allCameras.Select(c => new AllCamerasViewModel()
            {
                Id = c.Id,
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
                Task.Run(async () =>
                    {
                        var user = await this.userManager.FindByIdAsync(model.UserId);

                        var camera = new Camera
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
                            Quantity = model.Quantity,
                            User = user,
                            UserId = model.UserId

                        };

                        this.unitOfWork.Cameras.AddAsync(camera);
                        this.unitOfWork.Commit();
                    })
                    .Wait();

            }

        }

        public async Task<DetailCameraView> Details(int id)
        {
            var camera = this.unitOfWork.Cameras.Filter(c => c.Id == id)?.FirstOrDefault();

            if (camera != null)
            {
                var user = await this.userManager.FindByIdAsync(camera.UserId);

                string sellersname = user.UserName;

                var lightmeterings = camera?.LightMetering.GetFlags().Cast<LightMetering>();

                return new DetailCameraView()
                {
                    Model = camera.Model,
                    IsInStock = camera.Quantity > 0,
                    IsFullFrame = camera.IsFullFrame,
                    LightMeterings = lightmeterings,
                    Price = camera.Price,
                    MinShutterSpeed = camera.MinShutterSpeed,
                    MaxShutterSpeed = camera.MaxShutterSpeed,
                    ImageUrl = camera.ImageUrl,
                    Description = camera.Description,
                    Make = camera.Make,
                    VideoResolution = camera.VideoResolution,
                    MaxIso = camera.MaxIso,
                    MinIso = camera.MinIso,
                    SellersName = sellersname,
                    UserId = camera.UserId
                };
            }
            return null;
        }

        public async Task<UserDetailsViewModel> GetUserDetail(string userId, string guestUserId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var userCamers = this.unitOfWork.Cameras.Filter(c => c.UserId == userId).ToList();

            int camersInStock = userCamers.Where(c => c.Quantity > 0).ToList().Sum(c => c.Quantity);

            int camersOutofSotck = userCamers.Where(c => c.Quantity < 0).ToList().Sum(c => c.Quantity);

            bool userIsOwenerOfCameras = this.unitOfWork.Cameras.Any(c => c.UserId == guestUserId && c.UserId == userId);

            UserDetailsViewModel viewModel = new UserDetailsViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CamerasInStock = camersInStock,
                CamerasOutOfStock = camersOutofSotck,
                UserIsOwnerOfCameras = userIsOwenerOfCameras,
                UserCameras = userCamers.Select(uc => new AllCamerasViewModel()
                {
                    Model = uc.Model,
                    IsInStock = uc.Quantity > 0,
                    ImageUrl = uc.ImageUrl,
                    Make = uc.Make,
                    Price = uc.Price,
                    Id = uc.Id

                })
                 .ToList()
            };
            return viewModel;
        }

        public EditCameraBindingModels Edit(int id)
        {
            if (id <= 0)
            {
                return null;

            }

            var cameraToEdit = this.unitOfWork.Cameras?.FindById(id);

            if (cameraToEdit != null)
            {
                var viewModel = new EditCameraBindingModels()
                {
                    Model = cameraToEdit.Model,
                    Description = cameraToEdit.Description,
                    ImageUrl = cameraToEdit.ImageUrl,
                    IsFullFrame = cameraToEdit.IsFullFrame,
                    LightMeterings = cameraToEdit.LightMetering.GetFlags().Cast<LightMetering>(),
                    Make = cameraToEdit.Make,
                    MaxIso = cameraToEdit.MaxIso,
                    MaxShutterSpeed = cameraToEdit.MaxShutterSpeed,
                    MinIso = cameraToEdit.MinIso,
                    MinShutterSpeed = cameraToEdit.MinShutterSpeed,
                    Price = cameraToEdit.Price,
                    Quantity = cameraToEdit.Quantity,
                    VideoResolution = cameraToEdit.VideoResolution,
                    CameraId = id
                };
                return viewModel;
            }
            return null;

        }

        public void Edit(int id, EditCameraBindingModels cameraModel)
        {
            var cameraToEdit = this.unitOfWork.Cameras.FindById(id);

            if (cameraToEdit != null)
            {
                cameraToEdit.Model = cameraModel.Model;
                cameraToEdit.Description = cameraModel.Description;
                cameraToEdit.ImageUrl = cameraModel.ImageUrl;
                cameraToEdit.IsFullFrame = cameraModel.IsFullFrame;
                cameraToEdit.LightMetering = (LightMetering)cameraModel.LightMeterings.Cast<int>().Sum();
                cameraToEdit.Make = cameraModel.Make;
                cameraToEdit.MaxIso = cameraModel.MaxIso;
                cameraToEdit.MaxShutterSpeed = cameraModel.MaxShutterSpeed;
                cameraToEdit.MinIso = cameraModel.MinIso;
                cameraToEdit.MinShutterSpeed = cameraModel.MinShutterSpeed;
                cameraToEdit.Price = cameraModel.Price;
                cameraToEdit.Quantity = cameraModel.Quantity;
                cameraToEdit.VideoResolution = cameraModel.VideoResolution;

                this.unitOfWork.Commit();
            }

        }

        public CameraDeleteViewModel Delete(int? id)
        {
            if (id > 0)
            {
                var cameraToDelete = this.unitOfWork.Cameras?.FindById(id);

                if (cameraToDelete != null)
                {
                    CameraDeleteViewModel viewModel = new CameraDeleteViewModel()
                    {
                        Model = cameraToDelete.Model,
                        Make = cameraToDelete.Make,
                        Price = cameraToDelete.Price,
                        Id = cameraToDelete.Id
                    };
                    return viewModel;
                }
            }
            return null;
        }



        public void Destroy(int id)
        {
            var cameraToDestroy = this.unitOfWork.Cameras.FindById(id);

            this.unitOfWork.Cameras.Remove(cameraToDestroy);

            this.unitOfWork.Commit();
        }

        public bool UserCanDrestoryCamera(int? cameraId, string userId)
        {
            if (cameraId != null)
            {
                var userCanDestorycameras = this.unitOfWork.Cameras.Filter(c => c.UserId == userId).Any();

                return userCanDestorycameras;

            }
            return false;
        }

    }
}


