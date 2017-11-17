namespace CameraBazaar.Models.Enums.LightMetering
{
    using System;


    [Flags]
    public enum LightMeteringType
    {
        Spot = 0,
        CenterWeighted = 1,
        Evaluative = 2
    }
}
