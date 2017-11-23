namespace CameraBazaar.Models.ViewModels.Camera
{
    using Enums.CameraMake;

    public class CameraDeleteViewModel
    {
        public int Id { get; set; }

        public CameraMake Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

    }
}
