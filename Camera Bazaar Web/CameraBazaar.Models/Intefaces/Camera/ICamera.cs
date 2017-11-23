namespace CameraBazaar.Models.Intefaces.Camera
{
    using Enums.CameraMake;
    using Enums.Iso;
    using Enums.LightMetering;

    public interface ICamera
    {
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

    }
}
