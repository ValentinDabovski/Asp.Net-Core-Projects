namespace CameraBazaar.Models.DataModels.Camera
{
    using Enums.CameraMake;
    using Enums.Iso;
    using Enums.LightMetering;
    using Extensions.Attributes;
    using Identity;
    using System.ComponentModel.DataAnnotations;
    using Intefaces.Camera;
    
    public class Camera
    {
        public int Id { get; set; }

        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z0-9-]+)$")]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Range(minimum: 0, maximum: 100)]
        public int Quantity { get; set; }

        [Range(minimum: 1, maximum: 30)]
        public int MinShutterSpeed { get; set; }

        [Range(minimum: 2000, maximum: 8000)]
        public int MaxShutterSpeed { get; set; }

        public MinIso MinIso { get; set; }

        [Range(minimum: 200, maximum: 409600)]
        [DividableMaxIso]
        public int MaxIso { get; set; }

        [Required]
        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        public string VideoResolution { get; set; }

        [Required]
        public LightMetering LightMetering { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }

        [MinLength(12), MaxLength(2090)]
        public string ImageUrl { get; set; }
 
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
