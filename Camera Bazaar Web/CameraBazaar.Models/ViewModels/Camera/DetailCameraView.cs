namespace CameraBazaar.Models.ViewModels.Camera
{
    using Enums.CameraMake;
    using Enums.Iso;
    using Enums.LightMetering;
    using System.Collections.Generic;


    public class DetailCameraView
    {
        public string Model { get; set; }

        public CameraMake Make { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; }

        public bool IsInStock { get; set; }
        
        public string SellersName { get; set; }

        public string UserId { get; set; }

        public string ImageUrl { get; set; }

        public int MinShutterSpeed { get; set; }
        
        public int MaxShutterSpeed { get; set; }

        public MinIso MinIso { get; set; }
        
        public int MaxIso { get; set; }
        
        public bool IsFullFrame { get; set; }
        
        public string VideoResolution { get; set; }
        
        public IEnumerable<LightMetering> LightMeterings { get; set; }
        
        public string Description { get; set; }
        
    }
}
