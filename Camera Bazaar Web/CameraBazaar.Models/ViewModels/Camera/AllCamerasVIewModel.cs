﻿namespace CameraBazaar.Models.ViewModels.Camera
{
    using Enums.CameraMake;

    public class AllCamerasViewModel 
    {
        public int Id { get; set; } 

        public string ImageUrl { get; set; }

        public string Model { get; set; }

        public CameraMake Make { get; set; }

        public decimal Price { get; set; }

        public bool IsInStock { get; set; }


    }
}
