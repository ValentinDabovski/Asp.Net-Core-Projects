namespace CameraBazaar.Models.Intefaces.Camera
{
    using DataModels.Identity;
    using Enums.CameraMake;
    using Enums.Iso;
    using Enums.LightMetering;

    public interface ICamera
    {
        int Id { get;}

        CameraMake Make { get;}

        string Model { get;}

        decimal Price { get;}

        int Quantity { get;}

        int MinShutterSpeed { get;}

        int MaxShutterSpeed { get;}

        MinIso MinIso { get;}
        
        int MaxIso { get;}

        bool IsFullFrame { get;}

        string VideoResolution { get;}

        LightMetering LightMetering { get;}

        string Description { get;}
        
        string ImageUrl { get;}

        string UserId { get;}

        User User { get;}
    }
}
