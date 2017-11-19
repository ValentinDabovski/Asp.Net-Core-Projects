namespace CameraBazaar.Models.BindingModels.Camera
{
    using Enums.CameraMake;
    using Enums.Iso;
    using Enums.LightMetering;
    using Extensions.Attributes;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class CreateCameraBindingModel 
    {
        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z0-9-]+)$",ErrorMessage = "unknown model")]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Range(minimum: 0, maximum: 100)]
        public int Quantity { get; set; }

        [Display(Name = "Min Shutter Speed")]
        [Range(minimum: 1, maximum: 30)]
        public int MinShutterSpeed { get; set; }

        [Display(Name = "Max Shutter Speed")]
        [Range(minimum: 2000, maximum: 8000)]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Min Iso")]
        public MinIso MinIso { get; set; }

        [Range(minimum: 200, maximum: 409600)]
        [DividableMaxIso]
        public int MaxIso { get; set; }

        [Display(Name = "Full Frame")]
        [Required]
        public bool IsFullFrame { get; set; }

        [Display(Name = "Video Resolution")]
        [MaxLength(15)]
        public string VideoResolution { get; set; }

        [Display(Name = "Light Metering")]
        [Required]
        public IEnumerable<LightMetering> LightMeterings { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [Display(Name = "Image Url")]
        [MinLength(12), MaxLength(2090)]
        public string ImageUrl { get; set; }

        public string  UserId { get; set; }
    }
}
