namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using CameraBazaar.Models.Enums.LightMetering;

    public static class EnumExtensions
    {
        public static string ToDisplayName(this LightMetering lightMetering)
        {
            if (lightMetering == LightMetering.CenterWeighted)
            {
                return "Center-Weighted";
            }

            return lightMetering.ToString();
        }
    }
}
