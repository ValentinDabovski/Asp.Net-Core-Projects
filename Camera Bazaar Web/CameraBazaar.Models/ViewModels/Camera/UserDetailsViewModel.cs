namespace CameraBazaar.Models.ViewModels.Camera
{
    using System.Collections.Generic;

    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CamerasInStock { get; set; }

        public int CamerasOutOfStock { get; set; }

        public ICollection<AllCamerasViewModel> UserCameras { get; set; }

        public bool UserIsOwnerOfCameras { get; set; }

    }
}
