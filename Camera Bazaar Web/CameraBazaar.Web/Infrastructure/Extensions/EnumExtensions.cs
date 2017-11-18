namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using CameraBazaar.Models.Enums.LightMetering;

    public static class EnumExtensions
    {
        public static string ToDisplayName(this LightMeteringType lightMetering)
        {
            if (lightMetering == LightMeteringType.CenterWeighted)
            {
                return "Center-Weighted";
            }

            return lightMetering.ToString();
        }
    }
}
